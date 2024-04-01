using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamControl : MonoBehaviour
{
    public static TutorialCamControl instance;
    [SerializeField]
    private float waitingTime;

    [Header("At Start")]
    [SerializeField]
    public bool atStart;
    [SerializeField]
    private Transform[] cameraPositions;
    [SerializeField]
    private int currentPositionIndex = 0;
    [SerializeField]
    public float transitionSpeed = 1f;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    public bool cutsceneIsCompleted;
    [SerializeField]
    public bool endTutorial;
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private GameObject indicatorCanvas;
    [SerializeField]
    private GameObject startBlockingWall;

    [Header("Package")]
    [SerializeField]
    public bool inPackageArea;
    [SerializeField]
    private int currentPositionIndex1 = 0;
    [SerializeField]
    private Transform[] cameraPositions1;
    [SerializeField]
    private Camera cam1;
    [SerializeField]
    private bool isActivated;
    [SerializeField]
    public bool endTutorial1;
    [SerializeField]
    private GameObject packageBlockingWall;

    [Header("Push")]
    [SerializeField]
    public bool inPushArea;
    [SerializeField]
    private Camera cam2;
    private int currentPositionIndex2 = 0;
    [SerializeField]
    private Transform[] cameraPositions2;
    [SerializeField]
    private bool isActivated2;
    [SerializeField]
    public bool endTutorial2;
    [SerializeField]
    private GameObject pushBlockingWall;

    [Header("PressurePlate")]
    [SerializeField]
    public bool inPressurePlateArea;
    [SerializeField]
    private Camera cam3;
    private int currentPositionIndex3 = 0;
    [SerializeField]
    private Transform[] cameraPositions3;
    [SerializeField]
    private bool isActivated3;
    [SerializeField]
    public bool endTutorial3;
    [SerializeField]
    private GameObject pressurePlateBlockingWall;

    [Header("GoldSummoning")]
    [SerializeField]
    public bool inGoldSummningArea;
    [SerializeField]
    private Camera cam4;
    private int currentPositionIndex4 = 0;
    [SerializeField]
    private Transform[] cameraPositions4;
    [SerializeField]
    private bool isActivated4;
    [SerializeField]
    public bool endTutorial4;
    [SerializeField]
    private GameObject invisibleWall;
    [SerializeField]
    private bool wallIsGone;


    [Header("Checkpoint")]
    [SerializeField]
    public bool inCheckpointArea;
    [SerializeField]
    private Camera cam5;
    private int currentPositionIndex5 = 0;
    [SerializeField]
    private Transform[] cameraPositions5;
    [SerializeField]
    private bool isActivated5;
    [SerializeField]
    public bool endTutorial5;
    [SerializeField]
    private GameObject checkPointBlockingWall;

    [Header("SummoningCircle")]
    [SerializeField]
    public bool inSummoningCircleArea;
    [SerializeField]
    private Camera cam6;
    private int currentPositionIndex6 = 0;
    [SerializeField]
    private Transform[] cameraPositions6;
    [SerializeField]
    private bool isActivated6;
    [SerializeField]
    public bool endTutorial6;
    [SerializeField]
    private GameObject summoningCircleBlockingWall;

    [Header("Sabatage")]
    [SerializeField]
    public bool inSabptageArea;
    [SerializeField]
    private Camera cam7;
    private int currentPositionIndex7 = 0;
    [SerializeField]
    private Transform[] cameraPositions7;
    [SerializeField]
    private bool isActivated7;
    [SerializeField]
    public bool endTutorial7;
    [SerializeField]
    private GameObject sabatageingWall;

    [Header("DualSummoning")]
    [SerializeField]
    public bool inDualSummoningArea;
    [SerializeField]
    private Camera cam8;
    private int currentPositionIndex8 = 0;
    [SerializeField]
    private Transform[] cameraPositions8;
    [SerializeField]
    private bool isActivated8;
    [SerializeField]
    public bool endTutorial8;
    [SerializeField]
    private GameObject dualSummoningBlockingWall;

    [Header("PlayerSpecific")]
    [SerializeField]
    public bool inPlayerSpecificArea;
    [SerializeField]
    private Camera cam9;
    private int currentPositionIndex9 = 0;
    [SerializeField]
    private Transform[] cameraPositions9;
    [SerializeField]
    private bool isActivated9;
    [SerializeField]
    public bool endTutorial9;
    [SerializeField]
    private GameObject playerSpecificBlockingWall;

    [Header("Times of triggered the dialogue")]
    [SerializeField]
    private int startTimes;
    [SerializeField]
    private int packageTimes;
    [SerializeField]
    private int pushTimes;
    [SerializeField]
    private int pressurePlateTimes;
    [SerializeField]
    private int checkpointTimes;
    [SerializeField]
    private int summoningcircleTimes;
    [SerializeField]
    private int sabotageTimes;
    [SerializeField]
    private int dualSummoningTimes;
    [SerializeField]
    private int playerSpecificTimes;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        atStart = true;
        indicator.SetActive(false);
        indicatorCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AtStartCam();
        PackageArea();
        //PushArea();
        PressurePlateArea();
        //GoldSummoningArea();
        InvisibleWall();
        CheckpointArea();
        SummoningCircleArea();
        SabptageArea();
        DualSummoningArea();
        //PlayerSpecificArea();
    }

    #region At Start
    void AtStartCam()
    {
        if (atStart)
        {
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            mainCam.gameObject.SetActive(true);
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(false);
            cam4.gameObject.SetActive(false);
            cam5.gameObject.SetActive(false);
            cam6.gameObject.SetActive(false);
            cam7.gameObject.SetActive(false);
            cam8.gameObject.SetActive(false);
            cam9.gameObject.SetActive(false);

            startBlockingWall.SetActive(true);
            packageBlockingWall.SetActive(true);
            pushBlockingWall.SetActive(true);
            //summoningCircleBlockingWall.SetActive(true);
            //dualSummoningBlockingWall.SetActive(true);
            //playerSpecificBlockingWall.SetActive(true);
            //pressurePlateBlockingWall.SetActive(true);
            checkPointBlockingWall.SetActive(true);

            StartCoroutine(MoveToNextCamera());
            atStart = false;
        }

        if (currentPositionIndex >= cameraPositions.Length - 1 && !endTutorial)
        {
            StartCoroutine(StopMoveCam());
            endTutorial = true;
        }


    }

    IEnumerator MoveToNextCamera()
    {

        while (currentPositionIndex < cameraPositions.Length)
        {
            Transform targetPosition = cameraPositions[currentPositionIndex];
            yield return StartCoroutine(MoveCamera(targetPosition, transitionSpeed + 1, mainCam));
            currentPositionIndex++;
        }
    }

    public IEnumerator StopMoveCam()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        mainCam.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        cutsceneIsCompleted = true;
        SceneControl.instance.radialUI2.SetActive(false);
        indicator.SetActive(true);
        indicatorCanvas.SetActive(true);

    }

    public IEnumerator StopMoveCamStart1()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        mainCam.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        cutsceneIsCompleted = true;
        SceneControl.instance.radialUI2.SetActive(false);
        indicator.SetActive(true);
        indicatorCanvas.SetActive(true);

    }

    IEnumerator MoveCamera(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(mainCam.transform.position, targetPos.position) > 0.1f)
        {
            if (currentPositionIndex == 1)
            {
                mainCam.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
                mainCam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            }
            else
            {
                mainCam.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed);
                mainCam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed);
            }

            yield return null;
        }
    }

    #endregion

    #region Package Area

    private void PackageArea()
    {
        if (inPackageArea && !isActivated)
        {
            //indicator.SetActive(false);
            indicatorCanvas.SetActive(false);
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam1.gameObject.SetActive(true);


            StartCoroutine(MoveToNextCamera1());
            isActivated = true;
        }

        if (currentPositionIndex1 >= cameraPositions1.Length - 1 && !endTutorial1)
        {
            StartCoroutine(StopMoveCam1());
            endTutorial1 = true;
        }
    }
    IEnumerator MoveToNextCamera1()
    {
        //yield return new WaitForSeconds(1f);
        while (currentPositionIndex1 < cameraPositions1.Length)
        {
            Transform targetPosition = cameraPositions1[currentPositionIndex1];
            yield return StartCoroutine(MoveCamera1(targetPosition, transitionSpeed, cam1));
            currentPositionIndex1++;
        }
    }


    IEnumerator MoveCamera1(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam1.transform.position, targetPos.position) > 0.1f)
        {
            cam1.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam1.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    IEnumerator StopMoveCam1()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam1.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        //indicator.SetActive(true);
        indicatorCanvas.SetActive(true);

    }
    #endregion

    #region Push Area

    private void PushArea()
    {
        if (inPushArea && !isActivated2)
        {
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam2.gameObject.SetActive(true);

            StartCoroutine(MoveToNextCamera2());
            isActivated2 = true;
            print("1");
        }

        if (currentPositionIndex2 >= cameraPositions2.Length - 1 && !endTutorial2)
        {
            StartCoroutine(StopMoveCam2());
            endTutorial2 = true;
        }
    }
    IEnumerator MoveToNextCamera2()
    {
        yield return new WaitForSeconds(1f);
        while (currentPositionIndex2 < cameraPositions2.Length)
        {
            Transform targetPosition = cameraPositions2[currentPositionIndex2];
            yield return StartCoroutine(MoveCamera2(targetPosition, transitionSpeed, cam2));
            currentPositionIndex2++;
            print("2");
        }
    }


    IEnumerator MoveCamera2(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam2.transform.position, targetPos.position) > 0.1f)
        {
            cam2.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam2.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
            print("3");
        }
    }

    IEnumerator StopMoveCam2()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam2.gameObject.SetActive(false);
        print("4");
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;

    }
    #endregion

    #region PressurePlate Area
    private void PressurePlateArea()
    {
        if (inPressurePlateArea && !isActivated3)
        {
            indicator.SetActive(false);
            indicatorCanvas.SetActive(false);
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam3.gameObject.SetActive(true);


            StartCoroutine(MoveToNextCamera3());
            isActivated3 = true;
        }

        if (currentPositionIndex3 >= cameraPositions3.Length - 1 && !endTutorial3)
        {
            StartCoroutine(StopMoveCam3());
            endTutorial3 = true;
        }
    }
    IEnumerator MoveToNextCamera3()
    {
        while (currentPositionIndex3 < cameraPositions3.Length)
        {
            Transform targetPosition = cameraPositions3[currentPositionIndex3];
            yield return StartCoroutine(MoveCamera3(targetPosition, transitionSpeed, cam3));
            currentPositionIndex3++;
        }
    }


    IEnumerator MoveCamera3(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam3.transform.position, targetPos.position) > 0.1f)
        {
            cam3.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam3.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    IEnumerator StopMoveCam3()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam3.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        indicator.SetActive(true);
        indicatorCanvas.SetActive(true);

    }
    #endregion

    #region GoldSummoning Area

    private void InvisibleWall()
    {
        if (inGoldSummningArea && !wallIsGone)
        {
            StartCoroutine(SetInvisibleWall());
        }
    }

    IEnumerator SetInvisibleWall()
    {
        yield return new WaitForSeconds(1f);
        invisibleWall.SetActive(false);
        wallIsGone = true;
    }
    private void GoldSummoningArea()
    {
        if (inGoldSummningArea && !isActivated4)
        {
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam4.gameObject.SetActive(true);


            StartCoroutine(MoveToNextCamera4());
            isActivated4 = true;
        }

        if (currentPositionIndex4 >= cameraPositions4.Length - 1 && !endTutorial4)
        {
            StartCoroutine(StopMoveCam4());
            endTutorial4 = true;
            invisibleWall.SetActive(false);
        }
    }
    IEnumerator MoveToNextCamera4()
    {
        while (currentPositionIndex4 < cameraPositions4.Length)
        {
            Transform targetPosition = cameraPositions4[currentPositionIndex4];
            yield return StartCoroutine(MoveCamera4(targetPosition, transitionSpeed, cam4));
            currentPositionIndex4++;
        }
    }


    IEnumerator MoveCamera4(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam4.transform.position, targetPos.position) > 0.1f)
        {
            cam4.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam4.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    IEnumerator StopMoveCam4()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam4.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;

    }
    #endregion

    #region Checkpoint Area
    private void CheckpointArea()
    {
        if (inCheckpointArea && !isActivated5)
        {
            indicator.SetActive(false);
            indicatorCanvas.SetActive(false);
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam5.gameObject.SetActive(true);


            StartCoroutine(MoveToNextCamera5());
            isActivated5 = true;
        }

        if (currentPositionIndex5 >= cameraPositions5.Length - 1 && !endTutorial5)
        {
            StartCoroutine(StopMoveCam5());
            endTutorial5 = true;
        }
    }
    IEnumerator MoveToNextCamera5()
    {
        while (currentPositionIndex5 < cameraPositions5.Length)
        {
            Transform targetPosition = cameraPositions5[currentPositionIndex5];
            yield return StartCoroutine(MoveCamera5(targetPosition, transitionSpeed, cam5));
            currentPositionIndex5++;
        }
    }


    IEnumerator MoveCamera5(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam5.transform.position, targetPos.position) > 0.1f)
        {
            cam5.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam5.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    IEnumerator StopMoveCam5()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam5.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        indicator.SetActive(true);
        indicatorCanvas.SetActive(true);

    }
    #endregion

    #region inSummoningCircle Area
    private void SummoningCircleArea()
    {
        if (inSummoningCircleArea && !isActivated6)
        {
            indicator.SetActive(false);
            indicatorCanvas.SetActive(false);
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam6.gameObject.SetActive(true);


            StartCoroutine(MoveToNextCamera6());
            isActivated6 = true;
        }

        if (currentPositionIndex6 >= cameraPositions6.Length - 1 && !endTutorial6)
        {
            StartCoroutine(StopMoveCam6());
            endTutorial6 = true;
        }
    }
    IEnumerator MoveToNextCamera6()
    {
        while (currentPositionIndex6 < cameraPositions6.Length)
        {
            Transform targetPosition = cameraPositions6[currentPositionIndex6];
            yield return StartCoroutine(MoveCamera6(targetPosition, transitionSpeed, cam6));
            currentPositionIndex6++;
        }
    }


    IEnumerator MoveCamera6(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam6.transform.position, targetPos.position) > 0.1f)
        {
            cam6.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam6.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    IEnumerator StopMoveCam6()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam6.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        indicator.SetActive(true);
        indicatorCanvas.SetActive(true);

    }
    #endregion

    #region Sabotage Area
    private void SabptageArea()
    {
        if (inSabptageArea && !isActivated7)
        {
            indicator.SetActive(false);
            indicatorCanvas.SetActive(false);
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam7.gameObject.SetActive(true);


            StartCoroutine(MoveToNextCamera7());
            isActivated7 = true;
        }

        if (currentPositionIndex7 >= cameraPositions7.Length - 1 && !endTutorial7)
        {
            StartCoroutine(StopMoveCam7());
            endTutorial7 = true;
        }
    }
    IEnumerator MoveToNextCamera7()
    {
        while (currentPositionIndex7 < cameraPositions7.Length)
        {
            Transform targetPosition = cameraPositions7[currentPositionIndex7];
            yield return StartCoroutine(MoveCamera7(targetPosition, transitionSpeed, cam7));
            currentPositionIndex7++;
        }
    }


    IEnumerator MoveCamera7(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam7.transform.position, targetPos.position) > 0.1f)
        {
            cam7.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam7.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    IEnumerator StopMoveCam7()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam7.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        indicator.SetActive(true);
        indicatorCanvas.SetActive(true);

    }
    #endregion

    #region inDualSummoning Area
    private void DualSummoningArea()
    {
        if (inDualSummoningArea && !isActivated8)
        {
            indicator.SetActive(false);
            indicatorCanvas.SetActive(false);
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam8.gameObject.SetActive(true);


            StartCoroutine(MoveToNextCamera8());
            isActivated8 = true;
        }

        if (currentPositionIndex8 >= cameraPositions8.Length - 1 && !endTutorial8)
        {
            StartCoroutine(StopMoveCam8());
            endTutorial8 = true;
        }
    }
    IEnumerator MoveToNextCamera8()
    {
        while (currentPositionIndex8 < cameraPositions8.Length)
        {
            Transform targetPosition = cameraPositions8[currentPositionIndex8];
            yield return StartCoroutine(MoveCamera8(targetPosition, transitionSpeed, cam8));
            currentPositionIndex8++;
        }
    }


    IEnumerator MoveCamera8(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam8.transform.position, targetPos.position) > 0.1f)
        {
            cam8.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam8.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    IEnumerator StopMoveCam8()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam8.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        indicator.SetActive(true);
        indicatorCanvas.SetActive(true);

    }
    #endregion

    #region inPlayerSpecific Area
    private void PlayerSpecificArea()
    {
        if (inPlayerSpecificArea && !isActivated9)
        {
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
            cam9.gameObject.SetActive(true);


            StartCoroutine(MoveToNextCamera9());
            isActivated9 = true;
        }

        if (currentPositionIndex9 >= cameraPositions9.Length - 1 && !endTutorial9)
        {
            StartCoroutine(StopMoveCam9());
            endTutorial9 = true;
        }
    }
    IEnumerator MoveToNextCamera9()
    {
        while (currentPositionIndex9 < cameraPositions9.Length)
        {
            Transform targetPosition = cameraPositions9[currentPositionIndex9];
            yield return StartCoroutine(MoveCamera9(targetPosition, transitionSpeed, cam9));
            currentPositionIndex9++;
        }
    }


    IEnumerator MoveCamera9(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(cam9.transform.position, targetPos.position) > 0.1f)
        {
            cam9.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            cam9.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    IEnumerator StopMoveCam9()
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        cam9.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;

    }
    #endregion

    #region Destroy

    public void DestroyWall1()
    {
        startTimes += 1;
        if(startTimes >= 2)
        {
            Destroy(startBlockingWall.gameObject);
        }

    }

    public void DestroyWall2()
    {
        Destroy(packageBlockingWall.gameObject);
    }

    public void DestroyWall3()
    {
        pushTimes += 1;
        if(pushTimes >= 2)
        {
            Destroy(pushBlockingWall.gameObject);
        }

    }

    public void DestroyWall4()
    {
        pressurePlateTimes += 1;
        if (pressurePlateTimes >= 2)
        {
            Destroy(pressurePlateBlockingWall.gameObject);
        }

    }

    public void DestroyWall5()
    {
        checkpointTimes += 1;
        if (checkpointTimes >= 2)
        {
            Destroy(checkPointBlockingWall.gameObject);
        }

    }

    public void DestroyWall6()
    {
        summoningcircleTimes += 1;
        if (summoningcircleTimes >= 2)
        {
            Destroy(summoningCircleBlockingWall.gameObject);
        }

    }

    public void DestroyWall7()
    {
        sabotageTimes += 1;
        if (sabotageTimes >= 2)
        {
            Destroy(sabatageingWall.gameObject);
        }

    }

    public void DestroyWall8()
    {
        dualSummoningTimes += 1;
        if (dualSummoningTimes >= 2)
        {
            Destroy(dualSummoningBlockingWall.gameObject);
        }
    }

    public void DestroyWall9()
    {
        playerSpecificTimes += 1;
        if (playerSpecificTimes >= 2)
        {
            Destroy(pressurePlateBlockingWall.gameObject);
        }
    }

    #endregion
}





