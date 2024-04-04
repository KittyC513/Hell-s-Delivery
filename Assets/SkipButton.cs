using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    private bool backToHub;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) &&!backToHub)
        {
            GameManager.instance.timesEnterHub += 1;
            Loader.Load(Loader.Scene.HubStart);
            backToHub = true;
        }

    }
}
