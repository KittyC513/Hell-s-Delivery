using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamControl : MonoBehaviour
{
    public TutorialCamControl instance;


    [Header("At Start")]
    [SerializeField]
    private bool atStart;
    [SerializeField]
    private Transform[] cameraPositions;
    [SerializeField]
    private int currentPositionIndex = 0;
    [SerializeField]
    public float transitionSpeed = 1f;
    [SerializeField]
    private Camera mainCam;

    [Header("Package")]
    [SerializeField]
    private bool inPackageArea;
    [SerializeField]
    private int currentPositionIndex1 = 0;
    [SerializeField]
    private Transform[] cameraPositions1;
    [SerializeField]
    private Camera cam1;
    [SerializeField]
    private bool isActivated;

    [Header("Push")]
    [SerializeField]
    private bool inPushArea;
    [SerializeField]
    private Camera cam2;

    [Header("PressurePlate")]
    [SerializeField]
    private bool inPressurePlateArea;
    [SerializeField]
    private Camera cam3;

    [Header("GoldSummoning")]
    [SerializeField]
    private bool inGoldSumoningArea;
    [SerializeField]
    private Camera cam4;

    [Header("Checkpoint")]
    [SerializeField]
    private bool inCheckpointArea;
    [SerializeField]
    private Camera cam5;

    [Header("SummoningCircle")]
    [SerializeField]
    private bool inSummoningCircleArea;
    [SerializeField]
    private Camera cam6;

    [Header("Sabatage")]
    [SerializeField]
    private bool inSabptageArea;
    [SerializeField]
    private Camera cam7;

    [Header("DualSummoning")]
    [SerializeField]
    private bool inDualSummoningArea;
    [SerializeField]
    private Camera cam8;

    [Header("PlayerSpecific")]
    [SerializeField]
    private bool inPlayerSpecificArea;
    [SerializeField]
    private Camera cam9;




    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        atStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        AtStartCam();
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

            StartCoroutine(MoveToNextCamera());
            atStart = false;
        }

        if(currentPositionIndex >= cameraPositions.Length - 1)
        {
            StartCoroutine(StopMoveCam());
        }

        if (inPackageArea && !isActivated)
        {
            GameManager.instance.cam1.SetActive(false);
            GameManager.instance.cam2.SetActive(false);
            mainCam.gameObject.SetActive(false);
            cam1.gameObject.SetActive(true);
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(false);
            cam4.gameObject.SetActive(false);
            cam5.gameObject.SetActive(false);
            cam6.gameObject.SetActive(false);

            StartCoroutine(MoveToNextCamera1());
            isActivated = true;
        }
    }

    IEnumerator MoveToNextCamera()
    {
        while (currentPositionIndex < cameraPositions.Length)
        {
            Transform targetPosition = cameraPositions[currentPositionIndex];
            yield return StartCoroutine(MoveCamera(targetPosition, transitionSpeed, mainCam));
            currentPositionIndex++;
        }
    }

    IEnumerator StopMoveCam()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.cam1.SetActive(true);
        GameManager.instance.cam2.SetActive(true);
        mainCam.gameObject.SetActive(false);

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;

    }

    IEnumerator MoveCamera(Transform targetPos, float lerpSpeed, Camera cam)
    {
        while (Vector3.Distance(mainCam.transform.position, targetPos.position) > 0.1f)
        {
            if(currentPositionIndex == 1)
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
    IEnumerator MoveToNextCamera1()
    {
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
            mainCam.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, Time.deltaTime * lerpSpeed * 0.5f);
            mainCam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, Time.deltaTime * lerpSpeed * 0.5f);
            yield return null;
        }
    }

    #endregion
}
