using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHubStart : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Loader.Load(Loader.Scene.HubStart);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Loader.Load(Loader.Scene.HubStart);
        }
    }

}
