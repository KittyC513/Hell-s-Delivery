using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    [SerializeField]
    Camera mainCam;
    [SerializeField]
    private Transform cameraPosition;
    [SerializeField]
    private Animator animTitle;
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private GameObject instructionText;



    public string layerNameToFind1 = "P1Collider";
    public string layerNameToFind2 = "P2Collider";
    GameObject player;
    [SerializeField]
    private TestCube p1;
    [SerializeField]
    private TestCube p2;
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
    private Animator p1Ani;
    [SerializeField]
    private Animator p2Ani;
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
    Canvas canvas;

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

    private Dictionary<GameObject, GameObject> projectileToIndicator = new Dictionary<GameObject, GameObject>();

    [Header("Loading Screen")]
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider loadingSlider;


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
        FindCamera();
        DetectScene();
        PushCheck();

    }

    private void FixedUpdate()
    {

        SkyboxControl();

        ShowTVInstruction();

    }

    public void LoadScene(string sceneToLoad)
    {
        loadingScreen.SetActive(true);
        //Run the A sync
        StartCoroutine(LoadSceneAsync(sceneToLoad));

    }

    IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }


    }

    void SkyboxControl()
    {
        // Change skybox based on the scene name
        if (curSceneName == scene3)
        {
            RenderSettings.skybox = skyboxScene1;
        }
        else
        {
            RenderSettings.skybox = skyboxScene2;
        }
        print(RenderSettings.skybox);
    }

    void DetectScene()
    {
        if (sceneChanged)
        {
            StartCoroutine(CheckScene());
        }

            //if(curSceneName == scene4)
            //{
            //    p1StartPoint = GameObject.FindWithTag("P1StartPoint").transform;
            //    p2StartPoint = GameObject.FindWithTag("P2StartPoint").transform;

            //    player1.transform.position = p1StartPoint.position;
            //    player2.transform.position = p2StartPoint.position;
            //    print("Reset MVP Level");

            //    sceneChanged = false;
            //}
      

    }
    IEnumerator CheckScene()
    {
        yield return new WaitForSeconds(1);

        if (curSceneName == scene1)
        {
            player1.transform.position = new Vector3(4.97f, 1f, 3f);
            player2.transform.position = new Vector3(3f, 1f, 3f);
            print("Reset");
            print("gameManager " + curSceneName);
            sceneChanged = false;

        }
        else if (curSceneName == scene4)
        {
            p1StartPoint = GameObject.FindWithTag("P1StartPoint").transform;
            p2StartPoint = GameObject.FindWithTag("P2StartPoint").transform;

            player1.transform.position = p1StartPoint.position;
            player2.transform.position = p2StartPoint.position;
            print("Reset MVP Level");
            print("gameManager " + curSceneName);
            sceneChanged = false;
        }
        if (curSceneName == scene1 || curSceneName == scene5)
        {
            if (canvas == null)
            {
                canvas = GameObject.Find("TVCanvas").GetComponent<Canvas>();
                TVinstruction = canvas.gameObject;
                TVinstruction.SetActive(false);

            }

        }

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


                Transform parentTransform = player1.transform;

                foreach (Transform child in parentTransform)
                {
                    if (child.CompareTag("Character"))
                    {
                        p1Anim = child;
                        p1Character = p1Anim.gameObject;
                        p1AnimFound = true;
                        p1Ani = p1Character.GetComponent<Animator>();
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



                Transform parentTransform = player2.transform;

                foreach (Transform child in parentTransform)
                {
                    if (child.CompareTag("Character"))
                    {

                        p2Anim = child;
                        p2Character = p2Anim.gameObject;
                        p2AnimFound = true;
                        p2Ani = p2Character.GetComponent<Animator>();

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
        if(p1 != null && p2 != null)
        {
            currentScene = SceneManager.GetActiveScene();
            curSceneName = currentScene.name;
            if (curSceneName == scene3)
            {
                animTitle.SetBool("isEnded", true);
                text.SetActive(false);
                StartCoroutine(ShowDirection());
            }
            else
            {
                animTitle = null;
                text = null;
                StopCoroutine(ShowDirection());
            }
        }

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




    void FindCamera()
    {
        if(curSceneName == scene3)
        {
            mainCam = Camera.main;
            if (p1)
            {
                MoveCamera();
            }
        }
        else
        {
            mainCam = null;
            cameraPosition = null;
        }
    }



    void MoveCamera()
    {
        float lerpSpeed = 5f;
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, cameraPosition.position, Time.deltaTime * lerpSpeed);
        print("Camera");
    }



    void ShowTVInstruction()
    {
        if(curSceneName == scene1 || curSceneName == scene5)
        {
            if(canvas != null)
            {
                if (p1.withinTVRange || p2.withinTVRange)
                {
                    TVinstruction.SetActive(true);
                }
                else if (!p1.withinTVRange && !p2.withinTVRange)
                {
                    TVinstruction.SetActive(false);
                }
            }
        }

    }
    IEnumerator ShowDirection()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitingTime);
        instructionText.SetActive(true);
        // Destroy the GameObject this script is attached to
    }


    //IEnumerator ShowIntruction()
    //{

    //    // Wait for the specified time
    //    yield return new WaitForSeconds(waitingTime);
    //    TVinstruction = canvas.gameObject;
    //    TVinstruction.SetActive(true);
       
    //}



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
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);
        if(distance < appearanceDistance)
        {
            if (!projectileToIndicator.ContainsKey(player2))
            {
                //GameObject newIndicator = Instantiate(p2UIIndicator, Vector3.zero, Quaternion.identity);
               // projectileToIndicator.Add(player2, newIndicator);
            }
        }
    }

}






