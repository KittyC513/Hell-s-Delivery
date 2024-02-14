using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Canvas canvas;
    // List<TargetIndicator> targetIndicators = new List<TargetIndicator>();
    public Camera TestCamera;
    public GameObject PackageIndicatorObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PackageIndicator.instance.UpdatePackageIndicator();
    }


    public void AddPackageIndicator(GameObject package)
    {
       PackageIndicator indicator = GameObject.Instantiate(PackageIndicatorObject, canvas.transform).GetComponent<PackageIndicator>();
       indicator.InitializePackageIndicator(package, TestCamera, canvas);
       //targetIndicators.Add(indicator);

    }

}
