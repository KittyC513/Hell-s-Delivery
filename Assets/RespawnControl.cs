using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControl : MonoBehaviour
{
    TestCube testCube;
    Vector3 respawnPoint;


    private void Start()
    {
        testCube = GetComponent<TestCube>();
        respawnPoint = this.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Hazard"))
        {
            testCube.Respawn(respawnPoint);
        } else if(other.tag == "Checkpoint")
        {
            respawnPoint = this.transform.position;
        }
    }

}
