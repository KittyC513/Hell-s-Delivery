using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{

    public Transform platformTransform;
    public Transform playerTransform;
    public Transform originalTransform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "MovingPlat")
        {

            Debug.Log("success");

            platformTransform = collision.transform;

            playerTransform.parent = platformTransform;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MovingPlat")
        {

            Debug.Log("successtrigger");

            platformTransform = other.transform;

            playerTransform.parent = platformTransform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "MovingPlat")
        {
            Debug.Log("success2Strigger");

            playerTransform.parent = originalTransform;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "MovingPlat")
        {
            Debug.Log("success2");

            playerTransform.parent = originalTransform;

        }
    }
}
