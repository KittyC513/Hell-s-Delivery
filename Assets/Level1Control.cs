using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Control : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            GameManager.instance.p1.transform.position = boxingMinigame.instance.waypointExit.transform.position;
            GameManager.instance.p2.transform.position = boxingMinigame.instance.waypointExit.transform.position;
        }
    }
}
