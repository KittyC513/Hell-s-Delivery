using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHubStart : MonoBehaviour
{

    [SerializeField]
    private bool isEnterred;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEnterred)
        {
            //GameManager.instance.timesEnterHub += 1;
            GameManager.instance.changeSceneTimes += 1;
            if (GameManager.instance.curSceneName == "MVPLevel")
            {
                GameManager.instance.timesEnterHub += 1;
            }
            isEnterred = true;
            Loader.Load(Loader.Scene.HubStart);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !isEnterred)
        {
            //GameManager.instance.timesEnterHub += 1;
            GameManager.instance.changeSceneTimes += 1;
            if (GameManager.instance.curSceneName == "MVPLevel")
            {
                GameManager.instance.timesEnterHub += 1;
            }
            isEnterred = true;
            Loader.Load(Loader.Scene.HubStart);
        }
    }

}
