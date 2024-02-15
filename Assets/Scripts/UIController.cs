using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Canvas canvas;
    public List<PackageIndicator> targetIndicators = new List<PackageIndicator>();
    public Camera TestCamera;
    public GameObject PackageIndicatorPrefab;
 

    // Update is called once per frame
    void Update()
    {
        if(targetIndicators.Count > 0)
        {
            for (int i = 0; i < targetIndicators.Count; i++)
            {
                targetIndicators[i].UpdatePackageIndicator();
            }
        }
        //TargetIndicator.instance.UpdatePackageIndicator();
    }


    public void AddPackageIndicator(GameObject package)
    {
        PackageIndicator indicator = GameObject.Instantiate(PackageIndicatorPrefab, canvas.transform).GetComponent<PackageIndicator>();
       indicator.InitializePackageIndicator(package, TestCamera, canvas);
       targetIndicators.Add(indicator);

    }
   
    }

