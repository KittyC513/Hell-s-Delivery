using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        UIController ui = FindAnyObjectByType<UIController>();

        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIController>();
        }

        if (ui == null) Debug.LogError("No UIController component found");

        ui.AddPlayerIndicator(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
