using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHubStart : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameManager.instance.timesEnterHub += 1;
            GameManager.instance.changeSceneTimes += 1;
            if (GameManager.instance.curSceneName == "MVPLevel")
            {
                GameManager.instance.timesEnterHub += 1;
            }
            Loader.Load(Loader.Scene.HubStart);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            //GameManager.instance.timesEnterHub += 1;
            GameManager.instance.changeSceneTimes += 1;
            if (GameManager.instance.curSceneName == "MVPLevel")
            {
                GameManager.instance.timesEnterHub += 1;
            }
            Loader.Load(Loader.Scene.HubStart);
        }
    }

}
