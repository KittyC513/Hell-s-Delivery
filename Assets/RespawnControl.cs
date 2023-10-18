using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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



    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        curSceneName = currentScene.name;

        respawnPoint = this.transform.position;


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
        Player1isCarrying = objectGrabbable.P1TakePackage;
        Player2isCarrying = objectGrabbable.P2TakePackage;
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
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Hazard"))
        {
           Respawn(respawnPoint);
           if(Player1isCarrying)
           {
                objectGrabbable.Grab(objectGrabbable.p2ItemC.transform);
                objectGrabbable.P2TakePackage = true;
                objectGrabbable.P1TakePackage = false;
                Player1Die = true;
           }

           if (Player2isCarrying)
           {
                objectGrabbable.Grab(objectGrabbable.p1ItemC.transform);
                objectGrabbable.P2TakePackage = false;
                objectGrabbable.P1TakePackage = true;
                Player2Die = true;

            }

        } else if(other.tag == "CheckPoint")
        {
            respawnPoint = other.transform.position;
        }
    }

}
