using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTV : MonoBehaviour
{
    [SerializeField]
    private float waitingTime;
    [SerializeField]
    private GameObject instruction;


    private void Start()
    {
        print("Hello TV");
        instruction.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            print("TV");

            StartCoroutine(ShowIntruction());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            instruction.SetActive(false);
            print("NO TV");
        }
    }

    IEnumerator ShowIntruction()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitingTime);
        instruction.SetActive(true);
        print("Interact with TV");
        // Destroy the GameObject this script is attached to
    }

}
