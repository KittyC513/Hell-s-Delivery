using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVInstructionControl : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.showTVInstruction)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }

    }
}
