using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class boxingMinigame : MonoBehaviour
{
    public static boxingMinigame instance;

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
    bool endswitch = false;

    [Header("Boxing")]
    [SerializeField]
    private GameObject packagePiece1;
    [SerializeField]
    private GameObject packagePiece2;
    [SerializeField]
    private GameObject packagePiece3;
    [SerializeField]
    private GameObject packagePiece4;
    [SerializeField]
    private bool packageIsShowed;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        boxingCanvas.SetActive(false);
        spawnpointp1 = waypointp1.transform.position;
        spawnpointp2 = waypointp2.transform.position;
        spawnpointExit = waypointExit.transform.position;
        Scene scene = SceneManager.GetActiveScene();
        if (crowd != null)
        {
            crowd.SetActive(false);
        }


        sceneString = scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (isboxing)
        {
            packageIsShowed = false;

            if (sceneString == "Level1")
            {
                packagePiece1.SetActive(false);
                packagePiece2.SetActive(false);
                packagePiece3.SetActive(false);
                packagePiece4.SetActive(false);

                if (p1pushedcount >= maxDamage)
                {
                    //// Find all game objects with the tag "Findscript"
                    //GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("FindScript");

                    //// Loop through each object found
                    //foreach (GameObject obj in objectsWithTag)
                    //{
                    //    // Check if the object has a component of type RespawnControl
                    //    RespawnControl respawnControl = obj.GetComponent<RespawnControl>();

                    //    // If the RespawnControl component is found, do something with it
                    //    if (respawnControl != null)
                    //    {
                    //        // You can access methods and properties of the RespawnControl script here
                    //        // For example:
                    //        respawnControl.endminigamep1();
                    //    }
                    //}

                    GameManager.instance.p2.rC.endminigamep1();
                    endMinigame();
                }
                if (p2pushedcount >= maxDamage)
                {

                    // Find all game objects with the tag "Findscript"
                    //GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("FindScript");

                    //// Loop through each object found
                    //foreach (GameObject obj in objectsWithTag)
                    //{
                    //    // Check if the object has a component of type RespawnControl
                    //    RespawnControl respawnControl = obj.GetComponent<RespawnControl>();

                    //    // If the RespawnControl component is found, do something with it
                    //    if (respawnControl != null)
                    //    {
                    //        // You can access methods and properties of the RespawnControl script here
                    //        // For example:
                    //        respawnControl.endminigamep2();
                    //    }
                    //}

                    GameManager.instance.p1.rC.endminigamep2();
                    endMinigame();      
                }

                //if (GameManager.instance.p1.damageApplied)
                //{
                //    healthP1.fillAmount = (maxDamage - p1pushedcount) / maxDamage;
                //}

                //if (GameManager.instance.p2.damageApplied)
                //{
                //    healthP2.fillAmount = (maxDamage - p2pushedcount) / maxDamage;
                //}


                //print("HP");

            }

            




            if (sceneString == "MVPLevel" && endswitch == true)
            {
                packagePiece1.SetActive(false);
                packagePiece2.SetActive(false);

                Debug.Log("successful");
                // Find all game objects with the tag "Findscript"
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("FindScript");

                // Loop through each object found
                foreach (GameObject obj in objectsWithTag)
                {
                    // Check if the object has a component of type RespawnControl
                    RespawnControl respawnControl = obj.GetComponent<RespawnControl>();
                    Debug.Log(respawnControl.p1dead);
                    Debug.Log(respawnControl.p2dead);
                    // If the RespawnControl component is found, do something with it
                    if (respawnControl != null)
                    {
                        // You can access methods and properties of the RespawnControl script here
                        // For example:
                        if (respawnControl.p1dead)
                        {
                            respawnControl.p1dead = false;
                            respawnControl.p2dead = false;
                            respawnControl.endminigamep1();
                            endMinigame();
                            Debug.Log("endminigame p1 die");
                            endswitch = false;
                            
                        }
                        if (respawnControl.p2dead)
                        {
                            respawnControl.p1dead = false;
                            respawnControl.p2dead = false;
                            respawnControl.endminigamep2();
                            endMinigame();
                            Debug.Log("endminigame p2 die");
                            endswitch = false;
                            
                        }
                    }
                }

            }
        }
    }

    public GameObject crowd;

    public void StartMinigame()
    {
        boxingCanvas.SetActive(true);
        anim.SetTrigger("boxingStart");
        isboxing = true;
        cm.minigameCam();
        if (crowd != null)
        {
            crowd.SetActive(true);
        }
        //crowd.SetActive(true);

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
                respawnControl.p1dead = false;
                respawnControl.p2dead = false;
                respawnControl.startminigame();
                endswitch = true;
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

        if (!packageIsShowed)
        {
            StartCoroutine(ShowPackage());
        }

        if (crowd != null)
        {
            crowd.SetActive(false);
        }
    }

    IEnumerator ShowPackage()
    {
        yield return new WaitForSeconds(2f);
        if(packagePiece1 != null)
        {
            packagePiece1.SetActive(true);
        }

        if (packagePiece2 != null)
        {
            packagePiece2.SetActive(true);
        }

        if (packagePiece3 != null)
        {
            packagePiece3.SetActive(true);
        }

        if (packagePiece4 != null)
        {
            packagePiece4.SetActive(true);
        }

        packageIsShowed = true;
    }

}
