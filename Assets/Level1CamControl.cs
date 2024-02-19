using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1CamControl : MonoBehaviour
{
    public static Level1CamControl instance;

    [SerializeField]
    private float waitingTime;

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
    public bool cutsceneIsCompleted;
    [SerializeField]
    public bool endTutorial;
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
            //GameManager.instance.cam1.SetActive(false);
            //GameManager.instance.cam2.SetActive(false);
            //GameManager.instance.p1.isFreeze = true;
            //GameManager.instance.p2.isFreeze = true;
            mainCam.gameObject.SetActive(true);

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
            yield return StartCoroutine(MoveCamera(targetPosition, transitionSpeed, mainCam));
            currentPositionIndex++;
        }
    }

    IEnumerator StopMoveCam()
    {
        yield return new WaitForSeconds(3f);
        //GameManager.instance.cam1.SetActive(true);
        //GameManager.instance.cam2.SetActive(true);
        mainCam.gameObject.SetActive(false);

        //GameManager.instance.p1.isFreeze = false;
        //GameManager.instance.p2.isFreeze = false;
        cutsceneIsCompleted = true;

    }

    IEnumerator MoveCamera(Transform targetPos, float lerpSpeed, Camera cam)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(cam.transform.position, targetPos.position);

        while (Vector3.Distance(cam.transform.position, targetPos.position) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * lerpSpeed;
            float fractionOfJourney = distCovered / journeyLength;

            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos.position, fractionOfJourney);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetPos.rotation, fractionOfJourney);

            yield return null;
        }
    }

    #endregion
}
