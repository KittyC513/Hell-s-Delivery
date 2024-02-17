using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerIndicator : MonoBehaviour
{
    [SerializeField]
    private Image IndicatorImage;
    [SerializeField]
    private Image p1IndicatorImageOffScreen;
    [SerializeField]
    private Image p2IndicatorImageOffScreen;
    [SerializeField]
    private float outOfSigntOffSet;

    private float outOffSignOffset { get { return outOfSigntOffSet; } }

    [SerializeField]
    private GameObject package;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    [SerializeField]
    private Camera p1Cam;
    [SerializeField]
    private Camera p2Cam;

    [SerializeField]
    private RectTransform canvasRect;
    [SerializeField]
    private RectTransform canvasRect2;

    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        p1Cam = GameManager.instance.cam1.GetComponent<Camera>();
        p2Cam = GameManager.instance.cam2.GetComponent<Camera>();
    }

    public void InitializeIndicator(GameObject package, GameObject player1, GameObject player2, Camera cam, Camera cam2, Canvas canvas, Canvas canvas2)
    {
        this.package = package;
        this.player1 = player1;
        this.player2 = player2;
        this.p1Cam = cam;
        this.p2Cam = cam2;

        canvasRect = canvas.GetComponent<RectTransform>();
    }

    public void UpdateIndicator()
    {
        SetIndicatorPos();
    }

    protected void SetIndicatorPos()
    {
        Vector3 indicatorPosition = p2Cam.WorldToScreenPoint(package.transform.position);
        //print("indicatorPosition" + indicatorPosition);

        if (indicatorPosition.z >= 0f & indicatorPosition.x >= canvasRect.rect.width / 2 * canvasRect.localScale.x
        & indicatorPosition.y <= canvasRect.rect.height * canvasRect.localScale.x & indicatorPosition.x <= canvasRect.rect.width & indicatorPosition.y >= 0f)
        {
            indicatorPosition.z = 0f;
            targetOutOfSight(false, indicatorPosition);
            print("InSight");
        }
        else if (indicatorPosition.z >= 0f)
        {
            indicatorPosition = OutOfRangeIndicatorPosition(indicatorPosition);
            targetOutOfSight(true, indicatorPosition);
            print("OutOfSight");
        }
        else
        {
            indicatorPosition *= -1;
            indicatorPosition = OutOfRangeIndicatorPosition(indicatorPosition);
            targetOutOfSight(true, indicatorPosition);
            print("OutOfSight");
        }


        rectTransform.position = indicatorPosition;

    }

    private Vector3 OutOfRangeIndicatorPosition(Vector3 indicatorPosition)
    {
        indicatorPosition.z = 0f;

        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 1.33f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;
        indicatorPosition -= canvasCenter;

        float divX = (canvasRect.rect.width / 4f - outOffSignOffset) / Mathf.Abs(indicatorPosition.x);
        float divY = (canvasRect.rect.height / 2f - outOffSignOffset) / Mathf.Abs(indicatorPosition.y);

        if (divX < divY)
        {
            float angle = Vector3.SignedAngle(Vector3.right, indicatorPosition, Vector3.forward);
            indicatorPosition.x = Mathf.Sign(indicatorPosition.x) * (canvasRect.rect.width * 0.25f - outOffSignOffset) * canvasRect.localScale.x;
            indicatorPosition.y = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.x;
        }
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition, Vector3.forward);
            indicatorPosition.y = Mathf.Sign(indicatorPosition.y) * (canvasRect.rect.height / 2f - outOffSignOffset) * canvasRect.localScale.y;
            indicatorPosition.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.y;
        }

        indicatorPosition += canvasCenter;

        return indicatorPosition;
    }

    private void targetOutOfSight(bool outOfSight, Vector3 indicatorPosition)
    {
        if (IndicatorImage.isActiveAndEnabled == true)
        {
            IndicatorImage.enabled = false;
        }

        if (outOfSight)
        {
            if (p1IndicatorImageOffScreen.gameObject.activeSelf == false)
            {
                p1IndicatorImageOffScreen.gameObject.SetActive(true);
            }

            p1IndicatorImageOffScreen.rectTransform.rotation = Quaternion.Euler(rotationOutOfSigntIndicator(indicatorPosition));
        }
        else
        {
            if (p1IndicatorImageOffScreen.gameObject.activeSelf == true)
            {
                p1IndicatorImageOffScreen.gameObject.SetActive(false);
            }

        }
    }

    private Vector3 rotationOutOfSigntIndicator(Vector3 indicatorPosition)
    {
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 1.33f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;

        float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition - canvasCenter, Vector3.forward);

        return new Vector3(0f, 0f, angle);
    }

}




