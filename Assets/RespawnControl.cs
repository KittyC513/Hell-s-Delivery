using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControl : MonoBehaviour
{

    [SerializeField]
    Vector3 respawnPoint;

    [SerializeField]
    private GameObject player;




    private void Start()
    {

        respawnPoint = this.transform.position;
    }

    public void Respawn(Vector3 respawnPos)
    {
        player.transform.position = respawnPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Hazard"))
        {
           Respawn(respawnPoint);
        } else if(other.tag == "CheckPoint")
        {
            respawnPoint = other.transform.position;
        }
    }

}
