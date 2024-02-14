using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageIndicator : MonoBehaviour
{
    public static PackageIndicator instance;
    
    public Image PackageIndicatorImage;
    public Image OffScreenPackageIndicator;
    
    public float OutOfSightOffset = 45f;
    public float outOfSightOffset { get { return OutOfSightOffset; } }

    public GameObject package;

    private Camera mainCamera;

    private RectTransform canvasRect;

    private RectTransform rectTransform;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        //if(GameManager.instance.cam2 != null)
        //{
        //    mainCamera = GameManager.instance.cam2.GetComponent<Camera>();
        //} 

        // = Camera.main;


    }


    public void InitializePackageIndicator(GameObject package, Camera mainCamera, Canvas canvas)
    {
        mainCamera = Camera.main;
        this.package = package;
        this.mainCamera = mainCamera;
        canvasRect = canvas.GetComponent<RectTransform>();
    }


    public void UpdatePackageIndicator()
    {
        SetIndicatorPosition();

        // adjust distance display
        // turn on or off when in range/out of range
    }


    protected void SetIndicatorPosition()
    {
        // get pos of target in relation to screenspace
        Vector3 indicatorPos = mainCamera.WorldToScreenPoint(package.transform.position);

        if (indicatorPos.z >= 0f & indicatorPos.x <= canvasRect.rect.width * canvasRect.localScale.x & indicatorPos.y <= canvasRect.rect.height * canvasRect.localScale.x & indicatorPos.x >= 0f & indicatorPos.y >= 0f)
        {
            indicatorPos.z = 0f;

            targetOutOfSight(false, indicatorPos);
        }

        else if (indicatorPos.z >= 0f)
        {
            indicatorPos = OutOfRangeindicatorPositionB(indicatorPos);
            targetOutOfSight(true, indicatorPos);
        }

        else
        {
            indicatorPos *= -1f;

            indicatorPos = OutOfRangeindicatorPositionB(indicatorPos);
            targetOutOfSight(true, indicatorPos);
        }

        rectTransform.position = indicatorPos;

    }

    private Vector3 OutOfRangeindicatorPositionB(Vector3 indicatorPos)
    {
        indicatorPos.z = 0f;

        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;
        indicatorPos -= canvasCenter;

        float divX = (canvasRect.rect.width / 2f - outOfSightOffset / Mathf.Abs(indicatorPos.x));
        float divY = (canvasRect.rect.width / 2f - outOfSightOffset / Mathf.Abs(indicatorPos.y));

        if (divX < divY)
        {
            float angle = Vector3.SignedAngle(Vector3.right, indicatorPos, Vector3.forward);
            indicatorPos.x = Mathf.Sign(indicatorPos.x) * (canvasRect.rect.width * 0.5f - outOfSightOffset) * canvasRect.localScale.x;
            indicatorPos.y = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPos.x;
        }
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, indicatorPos, Vector3.forward);

            indicatorPos.y = Mathf.Sign(indicatorPos.y) * (canvasRect.rect.height / 2f - outOfSightOffset) * canvasRect.localScale.y;
            indicatorPos.x = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPos.y;
        }


        indicatorPos += canvasCenter;
        return indicatorPos;


    }

    private void targetOutOfSight(bool oos, Vector3 indicatorPosition)
    {
        if (oos)
        {
            if (OffScreenPackageIndicator.gameObject.activeSelf == false) OffScreenPackageIndicator.gameObject.SetActive(true);
            if (PackageIndicatorImage.isActiveAndEnabled == true) PackageIndicatorImage.enabled = false;

            //maybe add rotation
            OffScreenPackageIndicator.rectTransform.rotation = Quaternion.Euler(rotationOutOfSightTargetindicator(indicatorPosition));

        }
        else
        {
            if (OffScreenPackageIndicator.gameObject.activeSelf == true) OffScreenPackageIndicator.gameObject.SetActive(false);
            if (PackageIndicatorImage.isActiveAndEnabled == false) PackageIndicatorImage.enabled = true;


        }
    }


    private Vector3 rotationOutOfSightTargetindicator(Vector3 indicatorPosition)
    {
        //Calculate the canvasCenter
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;

        //Calculate the signedAngle between the position of the indicator and the Direction up.
        float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition - canvasCenter, Vector3.forward);

        //return the angle as a rotation Vector
        return new Vector3(0f, 0f, angle);
    }


}
