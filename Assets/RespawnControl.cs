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
    string scene5 = "TutorialLevel";
    [SerializeField]
    public bool Player1isCarrying;
    [SerializeField]
    public bool Player2isCarrying;
    public GameObject[] Partner;

    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    private bool isAtDoor;

    public DialogueRunner dR;

    public GameObject currentActive;
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

        respawnPoint = this.transform.position;
        dR = Object.FindAnyObjectByType<DialogueRunner>();

        gameManager = Object.FindAnyObjectByType<GameManager>();

        testCube = player.GetComponent<TestCube>();

        foreach (Transform child in cpParent.transform)
        {
            cps.Add(child.gameObject);

            CheckpointControl checkpc = child.gameObject.GetComponent<CheckpointControl>();
            cpc.Add(checkpc);
        }
    }

    void SceneCheck()
    {
        if (gameManager.sceneChanged)
        {
            if (curSceneName == scene2 || curSceneName == scene3 || curSceneName == scene4)
            {
                if (objectGrabbable == null)
                {
                    package = GameObject.FindGameObjectWithTag("Package");
                    objectGrabbable = package.GetComponent<ObjectGrabbable>();
                }
            }
            else
            {
                objectGrabbable = null;
            }
        }

    }

    private void Update()
    {
        SceneCheck();
        PlayerDetector();
        if(curSceneName == scene2 || curSceneName == scene3 || curSceneName == scene4)
        {
            Player1isCarrying = objectGrabbable.P1TakePackage;
            Player2isCarrying = objectGrabbable.P2TakePackage;
        }

        ////if (Partner == null)
        //{
         Partner = GameObject.FindGameObjectsWithTag("FindScript");
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
        Debug.Log("RespawnPoint =" + respawnPos);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Hazard"))
        {
            Debug.Log("Hazard name =" + other.gameObject);
            Respawn(respawnPoint);

            if (isPlayer1)
            {
                ScoreCount.instance.AddDeathsToP1(3);
                StartCoroutine(ActivateP1UIForDuration(3f));
                if (curSceneName == scene2)
                {
                    LevelDialogue.ShowDevilPlayer2();
                    dR.Stop();
                    PlayRandomDialogue();
                }

            }
            else
            {
                //dR.Stop();
            }

            if (isPlayer2)
            {
                ScoreCount.instance.AddDeathsToP2(3);
                StartCoroutine(ActivateP2UIForDuration(3f));
                if (curSceneName == scene2)
                {
                    LevelDialogue.ShowDevilPlayer1();
                    dR.Stop();
                    PlayRandomDialogue();
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
                Debug.Log("Player1Die");
            }
            else if (Player2isCarrying && isPlayer2)
            {
                objectGrabbable.Grab(objectGrabbable.p1ItemC.transform);
                objectGrabbable.P2TakePackage = false;
                objectGrabbable.P1TakePackage = true;
                Player2Die = true;
                Debug.Log("Player2Die");

            }

        }
        else if (other.tag == "CheckPoint")
        {
            respawnPoint = other.transform.position;
            Debug.Log("RespawnPoint =" + respawnPoint);
        }

        if (other.gameObject.tag == ("TriggerStart"))
        {
            LevelDialogue.ShowDevilAll();
            dR.StartDialogue("StartLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("TriggerFirstLevel"))
        {
            LevelDialogue.ShowDevilAll();
            dR.StartDialogue("FirstLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("TriggerSecondLevel"))
        {
            LevelDialogue.ShowDevilAll();
            dR.StartDialogue("SecondLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("TriggerThirdLevel"))
        {
            LevelDialogue.ShowDevilAll();
            dR.StartDialogue("ThirdLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("TriggerEnd"))
        {
            LevelDialogue.ShowDevilAll();
            dR.StartDialogue("EndLevel");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == ("fCheckpoint"))
        {

            respawnPoint = other.transform.position;

            foreach (CheckpointControl checkpc in cpc)
            {

                checkpc.deActivate = true;

            }

            Debug.Log("RespawnPoint =" + respawnPoint);

            foreach (GameObject obj in Partner)
            {
                //Debug.Log(obj);
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
        dR.StartDialogue(nodeNames[index]);
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

}
