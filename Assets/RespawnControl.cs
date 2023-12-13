using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using Yarn.Unity;

public class RespawnControl : MonoBehaviour
{

    [SerializeField]
    public Vector3 respawnPoint;

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

    //CheckpointControl activateFCP;


    List<string> nodeNames = new List<string>
    {
        "MeantToDoThat",
        "GottaHurt",
        "Yikes"
    };

    private void Start()
    {
        cpParent = GameObject.FindWithTag("cpParent");

        foreach (Transform child in cpParent.transform)
        {
            cps.Add(child.gameObject);

            CheckpointControl checkpc = child.gameObject.GetComponent<CheckpointControl>();
            cpc.Add(checkpc);
        }


        //dRP1 = Object.FindAnyObjectByType<DialogueRunner>();

        gameManager = Object.FindAnyObjectByType<GameManager>();

        testCube = player.GetComponent<TestCube>();

        
    }

    void SceneCheck()
    {
        if (GameManager.instance.sceneChanged)
        {
            curSceneName = GameManager.instance.curSceneName;
            if (curSceneName == scene4 || curSceneName ==scene5)
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

                if (objectGrabbable == null)
                {
                    package = GameObject.FindGameObjectWithTag("Package");
                    objectGrabbable = package.GetComponent<ObjectGrabbable>();
                }
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
        if(respawnPoint == null && curSceneName == scene4)
        {
            if (isPlayer1)
            {
                respawnPoint = SceneControl.instance.P1StartPoint.position;
            }
            if (isPlayer2)
            {
                respawnPoint = SceneControl.instance.P2StartPoint.position;
            }
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
            Player1isCarrying = objectGrabbable.P1TakePackage;
            Player2isCarrying = objectGrabbable.P2TakePackage;
        }
        ResetInitialRespawnPoint();
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
        player.transform.position = respawnPos;
        player.transform.eulerAngles = new Vector3(0, 90, 0);
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Hazard"))
        {
            //Debug.Log("Hazard name =" + other.gameObject);
            Respawn(respawnPoint);

            if (isPlayer1)
            {
                ScoreCount.instance.AddDeathsToP1(5);
                StartCoroutine(ActivateP1UIForDuration(3f));
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
                ScoreCount.instance.AddDeathsToP2(5);
                StartCoroutine(ActivateP2UIForDuration(3f));
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

        if (other.gameObject.tag == ("TriggerStart"))
        {
            //LevelDialogue.ShowDevilAll();
            dRP1.StartDialogue("StartLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("TriggerFirstLevel"))
        {
            //LevelDialogue.ShowDevilAll();
            dRP1.StartDialogue("FirstLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("TriggerSecondLevel"))
        {
            //LevelDialogue.ShowDevilAll();
            dRP1.StartDialogue("SecondLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("TriggerThirdLevel"))
        {
            //LevelDialogue.ShowDevilAll();
            dRP1.StartDialogue("ThirdLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("TriggerEnd"))
        {
            //LevelDialogue.ShowDevilAll();
            dRP1.StartDialogue("EndLevel");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == ("Start_Tutorial"))
        {


            if (isPlayer1 && !p1Pass)
            {
                p1Pass = true;
                LevelDialogue.ShowDevilPlayer1();
                //dR.Stop();
                dRP1.StartDialogue("LookAround");

            }

            if (isPlayer2 && !p2Pass)
            {
                p2Pass = true;
                LevelDialogue.ShowDevilPlayer2();
                //dR.Stop();
                dRP2.StartDialogue("LookAround2");

            }

            if (p2Pass && p1Pass)
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
                Debug.Log("loopworking");
                RespawnControl partnerScript = obj.GetComponent<RespawnControl>();

                if (partnerScript != null)
                {
                    partnerScript.respawnPoint = respawnPoint;
                    Debug.Log("Partner Respawn Point" + partnerScript.respawnPoint);
                }
                //Debug.Log("Partner Respawn Point" + partnerScript.respawnPoint);
            }
            
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("PostOfficeDoor") && gameManager.player1!= null && gameManager.player2 != null)
        {
            // when scene changed, both players reset start point
            if (testCube.ReadActionButton())
            {
                gameManager.sceneChanged = true;
                print("sceneChanged: " + gameManager.sceneChanged);

                //GameManager.instance.LoadScene(scene1);
                Loader.Load(Loader.Scene.HubStart);
                

            }            
        }
        //if(other.gameObject.tag == ("TV"))
        //{
        //    if (testCube.ReadActionButton())
        //    {
        //        print("Enter Tutorial Level");
        //        //gameManager.sceneChanged = true;
        //        //SceneManager.LoadScene("TutorialLevel);
        //    }

        //}
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
