using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using Yarn.Unity;

public class RespawnControl : MonoBehaviour
{

    [Header("Respawn Points")]
    [SerializeField]
    public Vector3 respawnPoint;
    [SerializeField]
    private bool resetRespawnP;
    [SerializeField]
    private Transform P1RespawnRotation;
    [SerializeField]
    private Transform P2RespawnRotation;

    [SerializeField]
    private List<GameObject> cps = new List<GameObject>();
    [SerializeField]
    private List<CheckpointControl> cpc = new List<CheckpointControl>();

    public GameObject cpParent;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TestCube testCube;

    [SerializeField]
    public bool isDead;

    [SerializeField]
    private ObjectGrabbable objectGrabbable;
    [SerializeField]
    private GameObject package;

    [SerializeField]
    bool isPlayer1;
    [SerializeField]
    bool isPlayer2;
    [SerializeField]
    public bool Player1Die;
    [SerializeField]
    public bool Player2Die;
    [SerializeField]
    string curSceneName;

    string scene1 = "HubStart";
    string scene2 = "PrototypeLevel";
    string scene3 = "TitleScene";
    string scene4 = "MVPLevel";
    string scene5 = "Tutorial";
    string scene9 = "Level1";

    [SerializeField]
    public bool Player1isCarrying;
    [SerializeField]
    public bool Player2isCarrying;
    public GameObject[] Partner;

    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    private bool isAtDoor;
    [SerializeField]
    public DialogueRunner dRP1, dRP2;
    [SerializeField]
    private GameObject dRGameobject, dRGameobject2;

    public GameObject currentActive;
    [SerializeField]
    private GameObject p1DeadScreen;
    [SerializeField]
    private GameObject p2DeadScreen;

    bool p1Pass;
    bool p2Pass;
    
    bool p1Pass1;
    bool p2Pass1;
    
    bool p1Pass2;
    bool p2Pass2;
    
    bool p1Pass3;
    bool p2Pass3;

    bool p1Pass4;
    bool p2Pass4;

    bool p1Pass5;
    bool p2Pass5;

    bool p1Pass6;
    bool p2Pass6;

    bool p1Pass7;
    bool p2Pass7;

    bool p1Pass8;
    bool p2Pass8;

    [SerializeField]
    private GameObject t1, t2, t3, t4, t5, t6, t7,t8;

    [SerializeField]
    private GameObject t1c, t2c, t3c, t4c, t5c, t6c, t7c,t8c;

    bool entry;

    [SerializeField]
    private GameObject p1Model, p2Model;
    [SerializeField]
    private bool switchPuzzleCam;

    [SerializeField]
    Vector3 respawnPos;
    //CheckpointControl activateFCP;


    List<string> nodeNames = new List<string>
    {
        "MeantToDoThat",
        "GottaHurt",
        "Yikes"
    };

    private void Start()
    {

        //dRP1 = Object.FindAnyObjectByType<DialogueRunner>();



        //testCube = player.GetComponent<TestCube>();


    }

    void SceneCheck()
    {
        if(gameManager == null)
        {
            gameManager = Object.FindAnyObjectByType<GameManager>();
        }
        if (gameManager.sceneChanged)
        {
            curSceneName = GameManager.instance.curSceneName;
            if (curSceneName ==scene5 ) 
            {
                if (dRP1 == null)
                {
                    dRGameobject = GameObject.FindWithTag("DRP1");
                    dRP1 = dRGameobject.GetComponent<DialogueRunner>();
                }

                if(dRP2 == null)
                {
                    dRGameobject2 = GameObject.FindWithTag("DRP2");
                    dRP2 = dRGameobject2.GetComponent<DialogueRunner>();
                }


            }

            if(curSceneName == scene4 || curSceneName == scene5 || curSceneName == scene9)
            {
                if (objectGrabbable == null)
                {
                    cpParent = GameObject.FindWithTag("cpParent");

                    foreach (Transform child in cpParent.transform)
                    {
                        cps.Add(child.gameObject);

                        CheckpointControl checkpc = child.gameObject.GetComponent<CheckpointControl>();
                        cpc.Add(checkpc);
                    }

                    package = GameObject.FindGameObjectWithTag("Package");
                    //package = GameObject.FindGameObjectWithTag("HeavyPackage");

                    objectGrabbable = package.GetComponent<ObjectGrabbable>();
                }
            }


            if (curSceneName != scene5)
            {
                t1.SetActive(false);
                t2.SetActive(false);
                t3.SetActive(false);
                t4.SetActive(false);
                t5.SetActive(false);
        
                t7.SetActive(false);
                t8.SetActive(false);
                t1c.SetActive(false);
                t2c.SetActive(false);
                t3c.SetActive(false);
                t4c.SetActive(false);
                t5c.SetActive(false);
                t6c.SetActive(false);
                t7c.SetActive(false);
               
            }

        }
    }

    //void FindDR()
    //{
    //    curSceneName = GameManager.instance.curSceneName;
    //    if(curSceneName == scene5 && dR == null)
    //    {

    //        dR = Object.FindAnyObjectByType<DialogueRunner>();
    //        print("DR FOUND");

    //    }

    //}



    void ResetInitialRespawnPoint()
    {

        if (isPlayer1)
        {
            respawnPoint = SceneControl.instance.P1StartPoint.position;
            //P1RespawnRotation = SceneControl.instance.P1Rotation.rotation;

        }
        if (isPlayer2)
        {
            respawnPoint = SceneControl.instance.P2StartPoint.position;
            //P2RespawnRotation = SceneControl.instance.P2Rotation.rotation;
        }




    }

    private void Update()
    {
        Partner = GameObject.FindGameObjectsWithTag("FindScript");
        //FindDR();
        SceneCheck();
        PlayerDetector();
        if(objectGrabbable != null)
        {

            //Debug.Log("check");

            Player1isCarrying = objectGrabbable.P1TakePackage;
            Player2isCarrying = objectGrabbable.P2TakePackage;
        }

        if (!resetRespawnP && curSceneName == scene4)
        {
            ResetInitialRespawnPoint();
            resetRespawnP = true;
        }
        else if (!resetRespawnP && curSceneName == scene9)
        {
            ResetInitialRespawnPoint();
            resetRespawnP = true;
        }

        Debug.Log(curSceneName);

            ////if (Partner == null)
            //{

            //}


        }
    void PlayerDetector()
    {

        if (this.gameObject.layer == LayerMask.NameToLayer("P1") && !isPlayer1)
        {
            isPlayer1 = true;
        }
        if (this.gameObject.layer == LayerMask.NameToLayer("P2") && !isPlayer2)
        {
            isPlayer2 = true;
        }

    }

    public void Respawn(Vector3 respawnPos)
    {
        if(curSceneName == scene5)
        {
            player.transform.position = respawnPos;
            P1RespawnRotation = SceneControl.instance.P1Rotation;
            P2RespawnRotation = P1RespawnRotation;
            player.transform.rotation = P1RespawnRotation.rotation;
        } 
        else if(curSceneName == scene4)
        {
            player.transform.position = respawnPos;
            if (isPlayer1)
            {
                P1RespawnRotation = SceneControl.instance.RespawnRotation;
                player.transform.rotation = P1RespawnRotation.rotation;
            }

            if (isPlayer2)
            {
                P2RespawnRotation = SceneControl.instance.RespawnRotation;
                player.transform.rotation = P2RespawnRotation.rotation;
            }

        }
        else if(curSceneName != scene4 && curSceneName != scene5)
        {
            player.transform.position = respawnPos;
        }

        //player.transform.rotation = respawnRotation.rotation;
        //Debug.Log("RespawnPoint =" + respawnPos);
    }
    //IEnumerator RespawnTimer(Vector3 respawnPos)
    //{
    //    yield return new WaitForSeconds(3);
    //    if (Player1Die)
    //    {
    //        player.transform.position = respawnPos;
    //        player.transform.eulerAngles = new Vector3(0, 90, 0);
    //        Player1Die = false;
    //    }
    //    if (Player2Die)
    //    {
    //        player.transform.position = respawnPos;
    //        player.transform.eulerAngles = new Vector3(0, 90, 0);
    //        Player2Die = false;
    //    }

    //}
    IEnumerator P1RespawnTimer()
    {
        p1Model.SetActive(false);
        p1DeadScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        Respawn(respawnPoint);
        p1Model.SetActive(true);
        p1DeadScreen.SetActive(false);
    }

    IEnumerator P2RespawnTimer()
    {
        p2Model.SetActive(false);
        p2DeadScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        Respawn(respawnPoint);
        p2Model.SetActive(true);
        p2DeadScreen.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Hazard"))
        {
            //Debug.Log("Hazard name =" + other.gameObject);

            //Respawn(respawnPoint);


            if (isPlayer1)
            {

                StartCoroutine(P1RespawnTimer());
                if (curSceneName != scene5)
                {
                    ScoreCount.instance.AddDeathsToP1(5);
                    StartCoroutine(ActivateP1UIForDuration(3f));
                }

                if (curSceneName == scene2)
                {
                    //LevelDialogue.ShowDevilPlayer2();
                    dRP1.Stop();
                    PlayRandomDialogue();
                    Player1Die = true;
                }

            }
            else
            {
                //dR.Stop();
            }

            if (isPlayer2)
            {
                StartCoroutine(P2RespawnTimer());
                if (curSceneName != scene5)
                {
                    ScoreCount.instance.AddDeathsToP2(5);
                    StartCoroutine(ActivateP2UIForDuration(3f));
                }

                if (curSceneName == scene2)
                {
                    //LevelDialogue.ShowDevilPlayer1();
                    dRP1.Stop();
                    PlayRandomDialogue();
                    Player2Die = true;
                }


            }
            else
            {
                //dR.Stop();
            }

            if (Player1isCarrying && isPlayer1)
            {
                objectGrabbable.Grab(objectGrabbable.p2ItemC.transform);
                objectGrabbable.P2TakePackage = true;
                objectGrabbable.P1TakePackage = false;
                Player1Die = true;
                //Debug.Log("Player1Die");
            }
            else if (Player2isCarrying && isPlayer2)
            {
                objectGrabbable.Grab(objectGrabbable.p1ItemC.transform);
                objectGrabbable.P2TakePackage = false;
                objectGrabbable.P1TakePackage = true;
                Player2Die = true;
                //Debug.Log("Player2Die");

            }

        }
        else if (other.tag == "CheckPoint")
        {
            
            respawnPoint = other.transform.position;
            objectGrabbable.respawnPoint = respawnPoint;
            //Debug.Log("RespawnPoint =" + respawnPoint);
        }

        if(other.tag == "EndingPoint")
        {
            if(isPlayer1 && Player1isCarrying)
            {
                Loader.Load(Loader.Scene.ScoreCards);
            }
            if (isPlayer2 && Player2isCarrying)
            {
                Loader.Load(Loader.Scene.ScoreCards);
            }

        }

        //if (other.gameObject.tag == ("TriggerStart"))
        //{
        //    //LevelDialogue.ShowDevilAll();
        //    dRP1.StartDialogue("StartLevel");
        //    Destroy(other.gameObject);
        //}

        //if (other.gameObject.tag == ("TriggerFirstLevel"))
        //{
        //    //LevelDialogue.ShowDevilAll();
        //    dRP1.StartDialogue("FirstLevel");
        //    Destroy(other.gameObject);
        //}

        //if (other.gameObject.tag == ("TriggerSecondLevel"))
        //{
        //    //LevelDialogue.ShowDevilAll();
        //    dRP1.StartDialogue("SecondLevel");
        //    Destroy(other.gameObject);
        //}

        //if (other.gameObject.tag == ("TriggerThirdLevel"))
        //{
        //    //LevelDialogue.ShowDevilAll();
        //    dRP1.StartDialogue("ThirdLevel");
        //    Destroy(other.gameObject);
        //}

        if (other.gameObject.tag == ("Start_Tutorial"))
        {
        

            if (isPlayer1 && !p1Pass)
            {
                p1Pass = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("LookAround");
    
            
            }


            if (isPlayer2 && !p2Pass)
            {
                p2Pass = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("LookAround2");
          

            }
 
            if (p1Pass && p2Pass)
            {
                Destroy(other.gameObject);
                t5.SetActive(false);
            }

        }

        if (other.gameObject.tag == ("Package_Tutorial"))
        {
            if (isPlayer1 && !p1Pass1)
            {
                p1Pass1 = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("Package");
         
                Debug.Log("Print");
   

            }
  

            if (isPlayer2 && !p2Pass1)
            {
                p2Pass1 = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("Packages2");
    

            }


            if (p1Pass1 && p2Pass1)
            {
                Destroy(other.gameObject);
                t3.SetActive(false);
            }
        }

        if (other.gameObject.tag == ("Jump_Tutorial"))
        {
            if (isPlayer1 && !p1Pass2)
            {
                p1Pass2 = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("P1Jump");
      
            

            }


            if (isPlayer2 && !p2Pass2)
            {
                p2Pass2 = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("P2Jump");
       

            }


            if  (p1Pass2 && p2Pass2)
            {
                Destroy(other.gameObject);
                t1.SetActive(false);
            }
        }

        if (other.gameObject.tag == ("Pressure_Tutorial"))
        {
            if (isPlayer1 && !p1Pass3)
            {
                p1Pass3 = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("PressurePlate");
           

            }
    
            if (isPlayer2 && !p2Pass3)
            {
                p2Pass3 = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("PressurePlate2");

            }

            if (p1Pass3 && p2Pass3)
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == ("Checkpoint_Tutorial"))
        {
            if (isPlayer1 && !p1Pass4)
            {
                p1Pass4 = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("Checkpoints");

            }

            if (isPlayer2 && !p2Pass4)
            {
                p2Pass4 = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("Checkpoints2");

            }


            if (p1Pass4 && p2Pass4)
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == ("SummoningCircle_Tutorial"))
        {
            if (isPlayer1 && !p1Pass5)
            {
                p1Pass5 = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("P1SummoningCircle");
            

            }


            if (isPlayer2 && !p2Pass5)
            {
                p2Pass5 = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("P2SummoningCircle");
            
            }

            if (p1Pass5 && p2Pass5)
            {
                Destroy(other.gameObject);
                t2.SetActive(false);
            }
       
            
        }

        if (other.gameObject.tag == ("Cooperation_Tutorial"))
        {
            if (isPlayer1 && !p1Pass6)
            {
                p1Pass6 = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("Cooperation");

            }

            if (isPlayer2 && !p2Pass6)
            {
                p2Pass6 = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("Cooperation2");
            }

            if (p1Pass6 && p2Pass6)
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == ("Sabotage_Tutorial"))
        {
            if (isPlayer1 && !p1Pass7)
            {
                p1Pass7 = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("Sabotage");

            }

            if (isPlayer2 && !p2Pass7)
            {
                p2Pass7 = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("Sabotage2");
            }

            if (p1Pass7 && p2Pass7)
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == ("End_Tutorial"))
        {

            if (isPlayer1 && !p1Pass8)
            {
                p1Pass8 = true;
                LevelDialogue.ShowDevilPlayer1();
                dRP1.Stop();
                dRP1.StartDialogue("End");
            }

            if (isPlayer2 && !p2Pass8)
            {
                p2Pass8 = true;
                LevelDialogue.ShowDevilPlayer2();
                dRP2.Stop();
                dRP2.StartDialogue("End2");
               
            }


            if (p1Pass8 && p2Pass8)
            {
                Destroy(other.gameObject);
            }
        }
        //Debug.Log("newcheckpoint");
        if (other.gameObject.tag == ("fCheckpoint"))
        {
            
            respawnPoint = other.transform.position;
            if (objectGrabbable != null)
            {
                objectGrabbable.respawnPoint = respawnPoint;
            }
            

            foreach (CheckpointControl checkpc in cpc)
            {
                Debug.Log("deactivatetrue");
                checkpc.deActivate = true;
                

            }

            //Debug.Log("RespawnPoint =" + respawnPoint);

            foreach (GameObject obj in Partner)
            {
                //Debug.Log("loopworking");
                RespawnControl partnerScript = obj.GetComponent<RespawnControl>();

                if (partnerScript != null)
                {
                    partnerScript.respawnPoint = respawnPoint;
                    //Debug.Log("Partner Respawn Point" + partnerScript.respawnPoint);
                }
                //Debug.Log("Partner Respawn Point" + partnerScript.respawnPoint);
            }
            
        }


    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag == ("PostOfficeDoor") && gameManager.player1!= null && gameManager.player2 != null)
        //{
        //    // when scene changed, both players reset start point
        //    if (testCube.ReadActionButton())
        //    {
        //        gameManager.enterOffice = true;
        //        gameManager.sceneChanged = true;
                
        //        //GameManager.instance.LoadScene(scene1);               

        //    }            
        //}
        if (other.gameObject.tag == ("Start_Tutorial"))
        {


            if (isPlayer1)
            {

                t1.SetActive(true);

            }


            if (isPlayer2)
            {

                t1c.SetActive(true);

            }



        }

        if (other.gameObject.tag == ("Package_Tutorial"))
        {
            if (isPlayer1)
            {

                t3.SetActive(true);

            }


            if (isPlayer2)
            {

                t3c.SetActive(true);

            }


        }

        if (other.gameObject.tag == ("Jump_Tutorial"))
        {
            if (isPlayer1)
            {

                t4.SetActive(true);

            }


            if (isPlayer2)
            {

                t4c.SetActive(true);

            }



        }

        if (other.gameObject.tag == ("Pressure_Tutorial"))
        {
            if (isPlayer1)
            {

                t5.SetActive(true);

            }

            if (isPlayer2)
            {

                t5c.SetActive(true);
            }


        }

        if (other.gameObject.tag == ("Checkpoint_Tutorial"))
        {
            if (isPlayer1)
            {
                t7.SetActive(true);

            }

            if (isPlayer2)
            {
                t6c.SetActive(true);

            }

        }

        if (other.gameObject.tag == ("SummoningCircle_Tutorial"))
        {
            if (isPlayer1)
            {

                t8.SetActive(true);
            }


            if (isPlayer2)
            {
                t7c.SetActive(true);

            }




        }

        if (other.gameObject.tag == ("Cooperation_Tutorial"))
        {
            if (isPlayer1)
            {

                t7.SetActive(true);

            }

            if (isPlayer2)
            {
                t6c.SetActive(true);
            }


        }


        if (other.gameObject.tag == ("End_Tutorial"))
        {

            if (isPlayer1)
            {

            }

            if (isPlayer2)
            {


            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Start_Tutorial"))
        {


            if (isPlayer1)
            {

                t1.SetActive(false);

            }


            if (isPlayer2)
            {

                t1c.SetActive(false);

            }



        }

        if (other.gameObject.tag == ("Package_Tutorial"))
        {
            if (isPlayer1)
            {

                t3.SetActive(false);

            }


            if (isPlayer2)
            {

                t3c.SetActive(false);

            }


        }

        if (other.gameObject.tag == ("Jump_Tutorial"))
        {
            if (isPlayer1)
            {

                t4.SetActive(false);

            }


            if (isPlayer2)
            {

                t4c.SetActive(false);

            }



        }

        if (other.gameObject.tag == ("Pressure_Tutorial"))
        {
            if (isPlayer1)
            {

                t5.SetActive(false);

            }

            if (isPlayer2)
            {

                t5c.SetActive(false);
            }


        }

        if (other.gameObject.tag == ("Checkpoint_Tutorial"))
        {
            if (isPlayer1)
            {
                t7.SetActive(false);

            }

            if (isPlayer2)
            {
                t6c.SetActive(false);

            }

        }

        if (other.gameObject.tag == ("SummoningCircle_Tutorial"))
        {
            if (isPlayer1)
            {

                t8.SetActive(false);
            }


            if (isPlayer2)
            {
                t7c.SetActive(false);

            }




        }

        if (other.gameObject.tag == ("Cooperation_Tutorial"))
        {
            if (isPlayer1)
            {

                t7.SetActive(false);

            }

            if (isPlayer2)
            {
                t6c.SetActive(false);
            }


        }


        if (other.gameObject.tag == ("End_Tutorial"))
        {

            if (isPlayer1)
            {

            }

            if (isPlayer2)
            {


            }

        }
    }




    public void PlayRandomDialogue()
    {
        System.Random rnd = new System.Random();
        int index = rnd.Next(nodeNames.Count);
        dRP1.StartDialogue(nodeNames[index]);
    }


    IEnumerator ActivateP1UIForDuration(float duration)
    {
        gameManager.p1UIMinus.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the UI after the specified duration
        gameManager.p1UIMinus.SetActive(false);
    }

    IEnumerator ActivateP2UIForDuration(float duration)
    {
        gameManager.p2UIMinus.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the UI after the specified duration
        gameManager.p2UIMinus.SetActive(false);
    }



    IEnumerator DestroyGameObject(GameObject gameObject)
    {
        Destroy(gameObject);
        return null;

    }

}
