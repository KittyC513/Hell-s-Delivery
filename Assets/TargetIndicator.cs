using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    List<TargetIndicator> targetIndicators = new List<TargetIndicator>();

    [SerializeField]
    Camera PlayerCamera;

    [SerializeField]
    GameObject TargetIndicatorPrefab;
    [SerializeField]
    GameObject PackageIndicatorPrefab;


    private void Update()
    {
        if(targetIndicators.Count > 0)
        {
            for(int i = 0; i < targetIndicators.Count; i++)
            {
                //targetIndicators[i].UpdateTargetIndicator();
            }
        }
    }

    private void AddTargetIndicator(GameObject target)
    {
        TargetIndicator indicator = GameObject.Instantiate(TargetIndicatorPrefab, canvas.transform).GetComponent<TargetIndicator>();
        //indicator.InitialiseTargetIndicator(target, PlayerCamera, canvas);
        targetIndicators.Add(indicator);
    }
}
