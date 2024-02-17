using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargerObject : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {




    }

    private void Start()
    {

        UIController ui = GetComponentInParent<UIController>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIController>();
        }

        if (ui == null) Debug.LogError("No UIController component found");

        ui.AddTargetIndicator(this.gameObject, null);
        
    }

}


