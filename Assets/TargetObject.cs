using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private void Awake()
    {
        UIController ui = GetComponentInParent<UIController>();

        if (ui == null)
        {
            ui = GameObject.Find("PackageControllerHolder").GetComponent<UIController>();
        }

        if (ui == null) Debug.LogError("No UIController component found");

        ui.AddPackageIndicator(this.gameObject);
    }
}
