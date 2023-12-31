using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    public string curSceneName;
    string scene1 = "HubStart";
    string scene2 = "PrototypeLevel";
    string scene3 = "TitleScene";
    string scene4 = "MVPLevel";
    string scene5 = "HubEnd";
    string scene6 = "ScoreCards";

    [SerializeField]
    Camera mainCam;
    [SerializeField]
    private Transform cameraPosition;
    [SerializeField]
    public Transform closeShoot;
    [SerializeField]
    public Animator animTitle;
    [SerializeField]
    public GameObject text;
    [SerializeField]
    private GameObject instructionText;



    public string layerNameToFind1 = "P1Collider";
    public string layerNameToFind2 = "P2Collider";
    GameObject player;
    [SerializeField]
    public TestCube p1;
    [SerializeField]
    public TestCube p2;
    [SerializeField]
    public GameObject player1;
    [SerializeField]
    public GameObject player2;
    [SerializeField]
    private Transform startPoint1;
    [SerializeField]
    private Transform startPoint2;


    [SerializeField]
    private GameObject character1;
    [SerializeField]
    private GameObject character2;
    [SerializeField]
    private bool readyToStart;
    [SerializeField]
    public Animator p1Ani;
    [SerializeField]
    public Animator p2Ani;
    [SerializeField]
    private Animator titleAni;
    [SerializeField]
    private float destroyTime;

    [SerializeField]
    public bool sceneChanged;
    [SerializeField]
    private GameObject[] popUps;
    [SerializeField]
    public int popUpIndex;

    [SerializeField]
    public Transform p1Anim;
    [SerializeField]
    public Transform p2Anim;
    [SerializeField]
    public GameObject p1Character;
    [SerializeField]
    public GameObject p2Character;
    [SerializeField]
    private bool p1AnimFound;
    [SerializeField]
    private bool p2AnimFound;
    [SerializeField]
    private bool isDestroyed;
    [SerializeField]
    public GameObject p1UI;
    [SerializeField]
    public GameObject p2UI;
    [SerializeField]
    private bool p1UIFound;
    [SerializeField]
    private bool p2UIFound;
    [SerializeField]
    public GameObject p1UIMinus;
    [SerializeField]
    public GameObject p2UIMinus;
    [SerializeField]
    private bool p1UIFound1;
    [SerializeField]
    private bool p2UIFound2;

    Scene currentScene;

    [SerializeField]
    private Material skyboxScene1;
    [SerializeField]
    private Material skyboxScene2;
    [SerializeField]
    private float waitingTime;
    [SerializeField]
    private GameObject TVinstruction;

    [SerializeField]
    public bool showTVInstruction;
    [SerializeField]
    public bool isBegin;
    [SerializeField]
    public bool showWertherInstruction;

    [SerializeField]
    public GameObject noisy1;
    [SerializeField]
    public GameObject noisy2;

    [SerializeField]
    private bool isNoisy1;
    [SerializeField]
    private bool isNoisy2;
    [SerializeField]
    private Transform p1StartPoint;
    [SerializeField]
    private Transform p2StartPoint;
   
    [Header("Indicator")]
    [SerializeField]
    private GameObject p1Indicator;
    [SerializeField]
    private GameObject p2Indicator;
    [SerializeField]
    private float indicatorDistance = 5;
    [SerializeField]
    private float appearanceDistance = 50f;
    [SerializeField]
    public bool GMconversationStart;
    [SerializeField]
    public Trigger trigger;
    [SerializeField]
    public bool foundTrigger;
    [SerializeField]
    public bool camChanged1;
    [SerializeField]
    public bool camChanged2;
    [SerializeField]
    public bool enterOffice;


    private Dictionary<GameObject, GameObject> projectileToIndicator = new Dictionary<GameObject, GameObject>();



    private void Awake()
    {
        instructionText.SetActive(false);
        instance = this;
    }
    private void Start()
    {

        sceneChanged = false;
        currentScene = SceneManager.GetActiveScene();
        curSceneName = currentScene.name;

    }

    private void Update()
    {
        FindPlayer();
        
        //FindCamera();
        DetectScene();
        PushCheck();
        P1Indicator();

    }

    private void FixedUpdate()
    {

        SkyboxControl();

        ShowTVInstruction();



    }



    void SkyboxControl()
    {
        // Change skybox based on the scene name
        if (sceneChanged)
        {
            if(curSceneName == scene3)
            {
                RenderSettings.skybox = skyboxScene1;
            }
            else
            {
                RenderSettings.skybox = skyboxScene2;
            }
            
        }

    }

    void DetectScene()
    {
        if (p1 != null && p2 != null)
        {

            currentScene = SceneManager.GetActiveScene();
            curSceneName = currentScene.name;
            if (curSceneName != scene3)
            {
                animTitle = null;
                text = null;
            }
            else
            {
                if (!enterOffice)
                {
                    MoveCamera(cameraPosition, 5f);
                    animTitle.SetBool("isEnded", true);
                    text.SetActive(false);
                }

            }


        }

    }


    public void Reposition(Transform P1position, Transform P2position)
    {
        player1.transform.position = P1position.position;
        player2.transform.position = P2position.position;
    }





    void FindPlayer()
    {
        int layerToFind1 = LayerMask.NameToLayer(layerNameToFind1);
        int layerToFind2 = LayerMask.NameToLayer(layerNameToFind2);
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objectsInScene)
        {
            if (obj.layer == layerToFind1 && p1 == null && !p1AnimFound && !p1UIFound && !p1UIFound1 &&!isNoisy1)
            {
                player1 = obj;
                p1 = obj.GetComponent<TestCube>();
                p1Ani = p1.playerAnimator;


                Transform parentTransform = player1.transform;

                foreach (Transform child in parentTransform)
                {
                    if (p1Ani != null)
                    {
                        p1AnimFound = true;
                    }

                    if (child.CompareTag("InGameUI"))
                    {
                        p1UI = child.gameObject;
                        p1UIFound = true;
                    }
                    
                    if (child.CompareTag("Minus"))
                    {
                        p1UIMinus = child.gameObject;
                        p1UIFound1 = true;
                    }

                    if (child.CompareTag("Noisy"))
                    {
                        noisy1 = child.gameObject;
                        isNoisy1 = true;
                        noisy1.SetActive(false);
                    }

                }


            }

            if (obj.layer == layerToFind2 && p2 == null && !p2AnimFound && !p2UIFound && !p2UIFound2 && !isNoisy2)
            {
                player2 = obj;
                p2 = obj.GetComponent<TestCube>();
                p2Ani = p2.playerAnimator2;


                Transform parentTransform = player2.transform;

                foreach (Transform child in parentTransform)
                {
                    if (p2Ani != null)
                    {
                        p2AnimFound = true; 
                    }

                    if (child.CompareTag("InGameUI"))
                    {
                        p2UI = child.gameObject;
                        p2UIFound = true;
                    }
                    if (child.CompareTag("Minus"))
                    {
                        p2UIMinus = child.gameObject;
                        p2UIFound2 = true;
                    }

                    if (child.CompareTag("Noisy"))
                    {
                        noisy2 = child.gameObject;
                        isNoisy2 = true;
                        noisy2.SetActive(false);

                    }
                }
            }
        }

        //two players join the game, it loads to the Title Scene

    }

 
    public void FreezePlayer()
    {
        if (p1 != null && p2 != null)
        {
            p1.isFreeze = true;
            p2.isFreeze = true;
        }

        if (p1 != null && p2 == null)
        {
            p1.isFreeze = true;
        }

        if (p1 == null && p2 != null)
        {
            p2.isFreeze = true;
        }

        if (enterOffice)
        {
            p1.isFreeze = true;
            p2.isFreeze = true;
        }
        if (sceneChanged)
        {
            p1.isFreeze = false;
            p2.isFreeze = false;
        }


    }

    public void UnfreezePlayer()
    {
        if (p1 != null && p2 != null)
        {
            p1.isFreeze = false;
            p2.isFreeze = false;

        }

        if (p1 != null && p2 == null)
        {
            p1.isFreeze = false;
        }

        if (p1 == null && p2 != null)
        {
            p2.isFreeze = false;
        }
    }

    public void DestroyObject()
    {
        if (player != null && player2 != null)
        {
            Destroy(player1);
            Destroy(player2);
        }

        if (player1 != null && player2 == null)
        {
            Destroy(player1);
        }

        if (player1 == null && player2 != null)
        {
            Destroy(player2);
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(destroyTime);

        // Destroy the GameObject this script is attached to
        Destroy(character1);
        Destroy(character2);
        isDestroyed = true;
    }




    //public void FindCamera()
    //{
    //    if(curSceneName == scene3)
    //    {
    //        mainCam = Camera.main;
    //        if (p1)
    //        {
    //            if (!enterOffice)
    //            {
    //                MoveCamera(cameraPosition);
    //            }
    //            else
    //            {

    //                StartCoroutine(MovingCamera());

    //                if(camChanged == true)
    //                {
    //                    Loader.Load(Loader.Scene.HubStart);
    //                }
    //            }


    //        }

    
    //    }
    //    else
    //    {
    //        mainCam = null;
    //        cameraPosition = null;
    //    }
    //}

    public IEnumerator MovingCamera1()
    {
        MoveCamera(closeShoot,5);
        yield return new WaitForSecondsRealtime(2f);
        camChanged1 = true;

    }

    //public IEnumerator MovingCamera2()
    //{
    //    MoveCamera(closeShoot);
    //    yield return new WaitForSecondsRealtime(2f);
    //    camChanged2 = true;

    //}






    public void MoveCamera(Transform newPos, float lerpSpeed)
    {
        
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newPos.position, Time.deltaTime * lerpSpeed);
        //mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, newPos.rotation, Time.deltaTime * lerpSpeed);

    }



    public void ShowTVInstruction()
    {
        if(curSceneName == scene1 || curSceneName == scene5)
        {
            if (p1.withinTVRange || p2.withinTVRange)
            {
                showTVInstruction = true;
            }
            else if (!p1.withinTVRange && !p2.withinTVRange)
            {
                showTVInstruction = false;
            }

            if(p1.withinNPCsRange || p2.withinNPCsRange)
            {
                showWertherInstruction = true;
            }
            else if (!p1.withinTVRange && !p2.withinTVRange)
            {
                showWertherInstruction = false;
            }
        }

    }
    public void ShowDirection()
    {
        // Wait for the specified time
        instructionText.SetActive(true);
        // Destroy the GameObject this script is attached to
    }

    public void StopShowDirection()
    {
        // Wait for the specified time
        instructionText.SetActive(false);
        // Destroy the GameObject this script is attached to
    }

    IEnumerable FindCharacter()
    {
        yield return new WaitForSeconds(1);
        FindPlayer();
        isBegin = false;
    }



    void PushCheck()
    {
        if(p1 != null && p2 != null)
        {
            if (p1.p1pushed)
            {
                noisy2.SetActive(true);
                StartCoroutine(StopNoisyP2());
                if (!p1.withinPushingRange)
                {
                    p2Ani.SetBool("beingPush", false);
                    p1.p1pushed = false;

                }
                
            }

            if (p2.p2pushed)
            {
                noisy1.SetActive(true);
                StartCoroutine(StopNoisyP1());

                if (!p2.withinPushingRange)
                {
                    p1Ani.SetBool("beingPush", false);
                    p2.p2pushed = false;
                }
            }
        }

    }

    IEnumerator StopNoisyP2()
    {
        yield return new WaitForSeconds(waitingTime);
        noisy2.SetActive(false);


    }
    IEnumerator StopNoisyP1()
    {
        yield return new WaitForSeconds(waitingTime);
        noisy1.SetActive(false);



    }
    void TutorialControl()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if(i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }
        }

        if(popUpIndex == 0)
        {
            if(p1.transform != startPoint1 && p1.transform != startPoint2)
            {
                popUpIndex++;

            } else if(popUpIndex == 1)
            {
                //enter office
            }
            else if (popUpIndex == 2)
            {
                //pick packahge
            }
        }
    }

    void P1Indicator()
    {


    }



}






