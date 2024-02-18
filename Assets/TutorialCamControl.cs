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



    [SerializeField]
    private bool inPackageArea;
    [SerializeField]
    private bool inPressurePlateArea;
    [SerializeField]
    private bool inCheckpointArea;
    [SerializeField]
    private bool inSummoningCircleArea;
    [SerializeField]
    private bool inDualSummoningArea;
    [SerializeField]
    private bool inSabptageArea;


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
            StartCoroutine(MoveToNextCamera());
            atStart = false;
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
}
