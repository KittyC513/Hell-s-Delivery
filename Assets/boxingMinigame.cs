using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class boxingMinigame : MonoBehaviour
{

    public GameObject waypointp1;
    public GameObject waypointp2;
    public GameObject waypointExit;
    public Vector3 spawnpointp1;
    public Vector3 spawnpointp2;
    public Vector3 spawnpointExit;
    public GameObject[] players;
    public Level1CamControl cm;
    public bool isboxing = false;
    public float p1pushedcount = 0;
    public float p2pushedcount = 0;
    public float maxDamage;
    public Image healthP1;
    public Image healthP2;
    public GameObject boxingCanvas;
    public Animator anim;
    string sceneString;




    // Start is called before the first frame update
    void Start()
    {
        boxingCanvas.SetActive(false);
        spawnpointp1 = waypointp1.transform.position;
        spawnpointp2 = waypointp2.transform.position;
        spawnpointExit = waypointExit.transform.position;
        Scene scene = SceneManager.GetActiveScene();


        sceneString = scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (isboxing)
        {
            if(sceneString == "Level1")
            {
                if (p1pushedcount >= maxDamage)
                {
                    // Find all game objects with the tag "Findscript"
                    GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("FindScript");

                    // Loop through each object found
                    foreach (GameObject obj in objectsWithTag)
                    {
                        // Check if the object has a component of type RespawnControl
                        RespawnControl respawnControl = obj.GetComponent<RespawnControl>();

                        // If the RespawnControl component is found, do something with it
                        if (respawnControl != null)
                        {
                            // You can access methods and properties of the RespawnControl script here
                            // For example:
                            respawnControl.endminigamep1();
                        }
                    }
                    endMinigame();
                }
                if (p2pushedcount >= maxDamage)
                {
                    if (p2pushedcount >= maxDamage)
                    {
                        // Find all game objects with the tag "Findscript"
                        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("FindScript");

                        // Loop through each object found
                        foreach (GameObject obj in objectsWithTag)
                        {
                            // Check if the object has a component of type RespawnControl
                            RespawnControl respawnControl = obj.GetComponent<RespawnControl>();

                            // If the RespawnControl component is found, do something with it
                            if (respawnControl != null)
                            {
                                // You can access methods and properties of the RespawnControl script here
                                // For example:
                                respawnControl.endminigamep2();
                            }
                        }
                        endMinigame();
                    }
                }
            }
            




            if (sceneString == "MVPLevel")
            {
                Debug.Log("sccessful");
                // Find all game objects with the tag "Findscript"
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("FindScript");

                // Loop through each object found
                foreach (GameObject obj in objectsWithTag)
                {
                    // Check if the object has a component of type RespawnControl
                    RespawnControl respawnControl = obj.GetComponent<RespawnControl>();

                    // If the RespawnControl component is found, do something with it
                    if (respawnControl != null)
                    {
                        // You can access methods and properties of the RespawnControl script here
                        // For example:
                        if (respawnControl.Player1Die)
                        {
                            respawnControl.endminigamep1();
                            endMinigame();
                            

                        }
                        if (respawnControl.Player2Die)
                        {
                            respawnControl.endminigamep2();
                            endMinigame();

                        }
                    }
                }

            }
        }

        healthP1.fillAmount = (maxDamage - p1pushedcount) / maxDamage;
        healthP2.fillAmount = (maxDamage - p2pushedcount) / maxDamage;

        


    }

    public void StartMinigame()
    {

        

        boxingCanvas.SetActive(true);
        anim.SetTrigger("boxingStart");
        isboxing = true;
        cm.minigameCam();
        // Find all game objects with the tag "Findscript"
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("FindScript");

        // Loop through each object found
        foreach (GameObject obj in objectsWithTag)
        {
            // Check if the object has a component of type RespawnControl
            RespawnControl respawnControl = obj.GetComponent<RespawnControl>();

            // If the RespawnControl component is found, do something with it
            if (respawnControl != null)
            {
                // You can access methods and properties of the RespawnControl script here
                // For example:
                respawnControl.startminigame();
            }
        }
    }

    public void endMinigame()
    {
        boxingCanvas.SetActive(false);
        isboxing = false;
        cm.endminigameCam();
        p1pushedcount = 0;
        p2pushedcount = 0;

    }
}
