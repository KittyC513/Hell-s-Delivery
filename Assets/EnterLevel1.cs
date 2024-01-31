using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            GameManager.instance.sceneChanged = true;
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;

            Loader.Load(Loader.Scene.Level1);

        }
    }
}
