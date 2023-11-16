using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class RespawnControl : MonoBehaviour
{

    [SerializeField]
    Vector3 respawnPoint;

    [SerializeField]
    private GameObject player;

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
    [SerializeField]
    public bool Player1isCarrying;
    [SerializeField]
    public bool Player2isCarrying;

    public DialogueRunner dR;

    List<string> nodeNames = new List<string>
    {
        "MeantToDoThat",
        "GottaHurt",
        "Yikes"
    };

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        curSceneName = currentScene.name;

        respawnPoint = this.transform.position;
        dR = Object.FindAnyObjectByType<DialogueRunner>();

    }

    void SceneCheck()
    {
        if (curSceneName == scene2 && objectGrabbable == null)
        {
            package = GameObject.FindGameObjectWithTag("Package");
            objectGrabbable = package.GetComponent<ObjectGrabbable>();
        }
    }

    private void Update()
    {
        SceneCheck();
        PlayerDetector();
        if(curSceneName == scene2)
        {
            Player1isCarrying = objectGrabbable.P1TakePackage;
            Player2isCarrying = objectGrabbable.P2TakePackage;
        }


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
                ScoreCount.instance.AddDeathsToP1(1);
                LevelDialogue.ShowDevilPlayer2();
                dR.Stop();
                PlayRandomDialogue();
            }
            else
            {
                dR.Stop();
            }

            if (isPlayer2)
            {
                ScoreCount.instance.AddDeathsToP2(1);
                LevelDialogue.ShowDevilPlayer1();
                dR.Stop();
                PlayRandomDialogue();
            }
            else
            {
                dR.Stop();
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


    }

    public void PlayRandomDialogue()
    {
        System.Random rnd = new System.Random();
        int index = rnd.Next(nodeNames.Count);
        dR.StartDialogue(nodeNames[index]);
    }

}
