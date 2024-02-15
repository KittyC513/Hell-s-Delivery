using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargerObject : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {

        UIController ui = GetComponentInParent<UIController>();
        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIController>();
        }

        if (ui == null) Debug.LogError("No UIController component found");

        ui.AddTargetIndicator(this.gameObject);

        UIController ui1 = GetComponentInParent<UIController>();
        if (ui1 == null)
        {
            ui1 = GameObject.Find("New Char Updated").GetComponent<UIController>();
        }

        if (ui1 == null) Debug.LogError("No UIController component found");

        ui1.AddPlayerIndicator(this.gameObject);


    }

    private void Start()
    {

    }

}


