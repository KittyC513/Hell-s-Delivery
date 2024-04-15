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
    public GameObject waypointp3;
    public GameObject waypointp4;
    public GameObject waypointExit;
    public Vector3 spawnpointp1;
    public Vector3 spawnpointp2;
    public Vector3 spawnpoint1;
    public Vector3 spawnpoint2;
    public Vector3 spawnpointp3;
    public Vector3 spawnpointp4;
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
    public GameObject boxingCanvas1;
    public Animator anim;
    public Animator anim1;
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
    public bool packageIsShowed;

    [Header("HubStart")]
    [SerializeField]
    private GameObject package1;
    [SerializeField]
    private GameObject package2;
    [SerializeField]
    public GameObject boxingCam;
    [SerializeField]
    public Camera boxingCamObject;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    public GameObject boxingCam1;
    [SerializeField]
    public Camera boxingCamObject1;
    [SerializeField]
    public Transform boxingSwitchCam;
    private bool minigameStart;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if(boxingCam != null)
        {
            if(boxingCamObject == null)
            {
                boxingCamObject = boxingCam.GetComponent<Camera>();
                boxingCam.SetActive(false);
            }

        }

        if(boxingCam1 != null)
        {
            boxingCamObject1 = boxingCam1.GetComponent<Camera>();
            boxingCam1.SetActive(false);
        }

        if(spawnpointp1 != null)
        {
            spawnpointp1 = waypointp1.transform.position;
        }

        if(spawnpointp3 != null)
        {
            spawnpointp3 = waypointp3.transform.position;
        }

        if(boxingCanvas!= null)
        {
            boxingCanvas.SetActive(false);
        }

        if (boxingCanvas1 != null)
        {
            boxingCanvas1.SetActive(false);
        }


        if (spawnpointp2 != null)
        {
            spawnpointp2 = waypointp2.transform.position;
        }

        if (spawnpointp4 != null)
        {
            spawnpointp4 = waypointp4.transform.position;
        }

        if (spawnpointExit != null)
        {
            spawnpointExit = waypointExit.transform.position;
        }


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

                    GameManager.instance.p2.rC.endminigamep1();
                    endMinigame();
                }
                if (p2pushedcount >= maxDamage)
                {
                    GameManager.instance.p1.rC.endminigamep2();
                    endMinigame();
                }
            }

            if (sceneString == "HubStart")
            {
                package1.SetActive(false);
                package2.SetActive(false);

                if (SelectMinigame.instance.chooseOne)
                {
                    if (p1pushedcount >= maxDamage)
                    {
                        GameManager.instance.p2.rC.endminigamep1();
                        EndGameInHub();
                    }
                    if (p2pushedcount >= maxDamage)
                    {
                        GameManager.instance.p1.rC.endminigamep2();
                        EndGameInHub();
                    }
                }

                if (SelectMinigame.instance.chooseTwo && endswitch == true)
                {
                    if (GameManager.instance.p1.rC.p1dead)
                    {
                        GameManager.instance.p1.rC.p1dead = false;
                        GameManager.instance.p1.rC.p2dead = false;
                        GameManager.instance.p1.rC.endminigamep2();
                        EndGameInHub();
                        endswitch = false;
                    }

                    if (GameManager.instance.p2.rC.p2dead)
                    {
                        GameManager.instance.p2.rC.p1dead = false;
                        GameManager.instance.p2.rC.p2dead = false;
                        GameManager.instance.p2.rC.endminigamep1();
                        EndGameInHub();
                        endswitch = false;
                    }
                }

            }
        

            if (sceneString == "MVPLevel" && endswitch == true)
            {
                packagePiece1.SetActive(false);
                packagePiece2.SetActive(false);


                //if (GameManager.instance.p1.rC.p1dead)
                //{
                //    GameManager.instance.p1.rC.p1dead = false;
                //    GameManager.instance.p1.rC.p2dead = false;
                //    GameManager.instance.p1.rC.endminigamep1();
                //    //GameManager.instance.p2.rC.endminigamep2();
                //    endswitch = false;
                //}

                //if (GameManager.instance.p2.rC.p1dead)
                //{
                //    GameManager.instance.p2.rC.p1dead = false;
                //    GameManager.instance.p2.rC.p2dead = false;
                //    //GameManager.instance.p1.rC.endminigamep1();
                //    GameManager.instance.p2.rC.endminigamep2();
                //    endswitch = false;
                //}


                Debug.Log("successful");
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
                        if (respawnControl.p1dead)
                        {
                            respawnControl.p1dead = false;
                            respawnControl.p2dead = false;
                            respawnControl.endminigamep1();
                            endMinigame();
                            endswitch = false;

                        }
                        if (respawnControl.p2dead)
                        {
                            respawnControl.p1dead = false;
                            respawnControl.p2dead = false;
                            respawnControl.endminigamep2();
                            endMinigame();

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

        if(GameManager.instance.curSceneName == "Level1")
        {
            cm.minigameCam();
            p1pushedcount = 0;
            p2pushedcount = 0;
            healthP1.fillAmount = (maxDamage - p1pushedcount) / maxDamage;
            healthP2.fillAmount = (maxDamage - p2pushedcount) / maxDamage;
        } 
        else if(GameManager.instance.curSceneName == "MVPLevel")
        {
            cm.minigameCam();
        }

        if (crowd != null)
        {
            crowd.SetActive(true);
        }
        //crowd.SetActive(true);

        GameManager.instance.p1.rC.p1dead = false;
        GameManager.instance.p2.rC.p2dead = false;
        GameManager.instance.p1.rC.startminigame();
        GameManager.instance.p2.rC.startminigame();
        endswitch = true;
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
        //        respawnControl.p1dead = false;
        //        respawnControl.p2dead = false;
        //        respawnControl.startminigame();
        //        endswitch = true;
        //    }
        //}

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

    public void StartGameHub()
    {

        isboxing = true;
        if (!minigameStart)
        {
            StartCoroutine(minigameCutscene());
        }

        if (SelectMinigame.instance.chooseOne)
        {
            boxingCanvas1.SetActive(false);
            boxingCanvas.SetActive(true);
            anim1.SetTrigger("boxingStart");
            boxingCam.SetActive(true);
            mainCam.SetActive(false);
            p1pushedcount = 0;
            p2pushedcount = 0;
            healthP1.fillAmount = (maxDamage - p1pushedcount) / maxDamage;
            healthP2.fillAmount = (maxDamage - p2pushedcount) / maxDamage;
        } 
        else if (SelectMinigame.instance.chooseTwo)
        {
            boxingCanvas.SetActive(false);
            boxingCanvas1.SetActive(true);
            anim.SetTrigger("boxingStart");
            boxingCam1.SetActive(true);
            mainCam.SetActive(false);
        }

        GameManager.instance.p1.rC.p1dead = false;
        GameManager.instance.p1.rC.p2dead = false;
        GameManager.instance.p2.rC.p1dead = false;
        GameManager.instance.p2.rC.p2dead = false;
        GameManager.instance.p1.rC.startminigameHub();
        GameManager.instance.p2.rC.startminigameHub();
        endswitch = true;

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
        //        respawnControl.p1dead = false;
        //        respawnControl.p2dead = false;
        //        respawnControl.startminigameHub();
        //        endswitch = true;
        //    }
        //}


    }

    IEnumerator minigameCutscene()
    {
        GameManager.instance.p1.isFreeze = true;
        GameManager.instance.p2.isFreeze = true;

        if (GameManager.instance.curSceneName == "MVPLevel")
        {
            boxingCam1.gameObject.SetActive(true);
            StartCoroutine(movingCam());
        }
        else
        {
            boxingCam.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        minigameStart = true;
    }

    IEnumerator movingCam()
    {
        print("MovingCam");
        yield return new WaitForSeconds(1f);
        MoveCamera(boxingSwitchCam);
    }

    public void MoveCamera(Transform newPos)
    {
        float lerpSpeed = 1f;
        boxingCam1.transform.position = Vector3.Lerp(boxingCam1.transform.position, newPos.position, Time.deltaTime * lerpSpeed);
        boxingCam1.transform.rotation = Quaternion.Lerp(boxingCam1.transform.rotation, newPos.rotation, Time.deltaTime * lerpSpeed);

    }

    public void EndGameInHub()
    {
        boxingCanvas.SetActive(false);
        boxingCanvas1.SetActive(false);
        StartCoroutine(SwitchCam());
        p1pushedcount = 0;
        p2pushedcount = 0;
        if (!packageIsShowed)
        {
            StartCoroutine(ShowPackage());
        }
        minigameStart = false;
    }

    IEnumerator SwitchCam()
    {
        yield return new WaitForSeconds(2f);
        boxingCam.SetActive(false);
        boxingCam1.SetActive(false);
        mainCam.SetActive(true);
    }

    IEnumerator ShowPackage()
    {
        yield return new WaitForSeconds(2f);
        if(GameManager.instance.curSceneName == "HubStart")
        {
            GameManager.instance.p1.transform.position = SceneControl.instance.originalPos1.position;
            GameManager.instance.p2.transform.position = SceneControl.instance.originalPos2.position;
        }

        if (packagePiece1 != null)
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

        if(package1 != null)
        {
            package1.SetActive(true);
        }

        if (package2 != null)
        {
            package2.SetActive(true);
        }

        packageIsShowed = true;

        if(GameManager.instance.curSceneName == "HubStart")
        {
            isboxing = false;
        }

    }

}
