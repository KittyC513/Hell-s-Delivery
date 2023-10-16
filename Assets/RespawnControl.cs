using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControl : MonoBehaviour
{

    [SerializeField]
    Vector3 respawnPoint;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool isDead;

    [SerializeField]
    private ObjectGrabbable objectGrabbable;
    [SerializeField]
    private GameObject package;

    private void Start()
    {

        respawnPoint = this.transform.position;
        package = GameObject.FindGameObjectWithTag("Package");
        objectGrabbable = package.GetComponent<ObjectGrabbable>();

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
           isDead = true;
           if(this.gameObject.layer == LayerMask.NameToLayer("P1") && objectGrabbable.P1TakePackage)
           {
                objectGrabbable.Grab(objectGrabbable.p2ItemC.transform);
 
                objectGrabbable.P2TakePackage = true;
                objectGrabbable.P1TakePackage = false;
           }

            if (this.gameObject.layer == LayerMask.NameToLayer("P2") && objectGrabbable.P2TakePackage)
            {
                objectGrabbable.Grab(objectGrabbable.p1ItemC.transform);
                objectGrabbable.P2TakePackage = false;
                objectGrabbable.P1TakePackage = true;
            }

        } else if(other.tag == "CheckPoint")
        {
            respawnPoint = other.transform.position;
        }
    }

}
