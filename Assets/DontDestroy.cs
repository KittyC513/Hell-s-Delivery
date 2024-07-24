using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.instance.gameIsReset)
        {
            Destroy(this.gameObject);
            GameManager.instance.gameIsReset = false;
        }

    }
}
