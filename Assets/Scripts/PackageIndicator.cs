using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageIndicator : MonoBehaviour
{
    public static PackageIndicator instance;
    
    public Image PackageIndicatorImage;
    public Image OffScreenPackageIndicator;
    
    public float OutOfSightOffset = 20f;
    public float outOfSightOffset { get { return OutOfSightOffset; } }

    private GameObject package;
    public Camera testCamera;
    private RectTransform canvasRect;
    private RectTransform rectTransform;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
    }


    public void InitializePackageIndicator(GameObject package, Camera testCamera, Canvas canvas)
    {
        this.package = package;
        this.testCamera = testCamera;
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
        Vector3 indicatorPos = testCamera.WorldToScreenPoint(package.transform.position);

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

    private Vector3 OutOfRangeindicatorPositionB(Vector3 indicatorPosition)
    {
        indicatorPosition.z = 0f;

        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;
        indicatorPosition -= canvasCenter;

        float divX = (canvasRect.rect.width / 2f - outOfSightOffset / Mathf.Abs(indicatorPosition.x));
        float divY = (canvasRect.rect.width / 2f - outOfSightOffset / Mathf.Abs(indicatorPosition.y));

        if (divX < divY)
        {
            float angle = Vector3.SignedAngle(Vector3.right, indicatorPosition, Vector3.forward);
            indicatorPosition.x = Mathf.Sign(indicatorPosition.x) * (canvasRect.rect.width * 0.5f - outOfSightOffset) * canvasRect.localScale.x;
            indicatorPosition.y = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.x;
        }
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition, Vector3.forward);

            indicatorPosition.y = Mathf.Sign(indicatorPosition.y) * (canvasRect.rect.height / 2f - outOfSightOffset) * canvasRect.localScale.y;
            indicatorPosition.x = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.y;
        }


        indicatorPosition += canvasCenter;
        return indicatorPosition;


    }

    private void targetOutOfSight(bool oos, Vector3 indicatorPosition)
    {
        if (oos)
        {
            if (OffScreenPackageIndicator.gameObject.activeSelf == false) OffScreenPackageIndicator.gameObject.SetActive(true);
            if (PackageIndicatorImage.isActiveAndEnabled == true) PackageIndicatorImage.enabled = false;

            //maybe add rotation
        }
        else
        {
            if (OffScreenPackageIndicator.gameObject.activeSelf == true) OffScreenPackageIndicator.gameObject.SetActive(false);
            if (PackageIndicatorImage.isActiveAndEnabled == false) PackageIndicatorImage.enabled = true;

        }
    }



}
