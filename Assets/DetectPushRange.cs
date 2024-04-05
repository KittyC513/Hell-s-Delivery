using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPushRange : MonoBehaviour
{
    [SerializeField]
    private TestCube testCube;
    [SerializeField]
    private bool isPlayer1;
    [SerializeField]
    private bool isPlayer2;
    [SerializeField]
    private GameObject toy;
    [SerializeField]
    private bool toyIsPicked;
    [SerializeField]
    private bool dropToy;
    [SerializeField]
    private BoxCollider toyCollider;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (testCube.isPlayer1)
        {
            isPlayer1 = true;
        }

        if (testCube.isPlayer2)
        {
            isPlayer2 = true;
        }

        TakeToy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer1 && other.CompareTag("Player"))
        {
            testCube.withinPushingRange = true;
        }

        if (isPlayer2 && other.CompareTag("Player"))
        {
            testCube.withinPushingRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayer1 && other.CompareTag("Player"))
        {
            testCube.withinPushingRange = false;
        }

        if (isPlayer2 && other.CompareTag("Player"))
        {
            testCube.withinPushingRange = false;
        }

        if (isPlayer1 && other.CompareTag("Toy"))
        {
            testCube.withinToyRange = false;
            if (!testCube.leftHandisFull)
            {
                toy = null;
            }
        }

        if (isPlayer2 && other.CompareTag("Toy"))
        {
            testCube.withinToyRange = false;
            if (!testCube.leftHandisFull)
            {
                toy = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPlayer1 && other.CompareTag("Toy"))
        {
            testCube.withinToyRange = true;
            if(toy == null && !dropToy)
            {
                toy = other.gameObject;
                toyCollider = toy.GetComponent<BoxCollider>();
                rb = toy.GetComponent<Rigidbody>();
            }

        }

        if (isPlayer2 && other.CompareTag("Toy"))
        {
            testCube.withinToyRange = true;

            if (toy == null && !dropToy)
            {
                toy = other.gameObject;
                toyCollider = toy.GetComponent<BoxCollider>();
                rb = toy.GetComponent<Rigidbody>();
            }
        }
    }

    private void TakeToy()
    {
        if(testCube.withinToyRange && testCube.ReadActionButton() && !dropToy)
        {
            toyIsPicked = true;

        }

        if (toyIsPicked)
        {
            if (timer < 1)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 1;
            }
            if (isPlayer1)
            {

                //toy.transform.position = testCube.leftHand1.position;
                testCube.leftHandisFull = true;
                //if(testCube.leftHandisFull && !testCube.rightHandisFull)
                //{
                //    toy.transform.position = testCube.rightHand1.position;
                //    testCube.rightHandisFull = true;
                //}

            }

            if (isPlayer2)
            {

                //toy.transform.position = testCube.leftHand2.position;
                testCube.leftHandisFull = true;
                //if (testCube.leftHandisFull && !testCube.rightHandisFull)
                //{
                //    toy.transform.position = testCube.rightHand2.position;
                //    testCube.rightHandisFull = true;
                //}
            }

        }

        if (testCube.leftHandisFull && isPlayer1 && !dropToy)
        {
            toy.transform.position = testCube.leftHand1.position;
            toyCollider.enabled = false;
            rb.isKinematic = true;

            if (testCube.ReadActionButton() && timer >= 0.3f)
            {
                dropToy = true;
            }
        }


        if (testCube.leftHandisFull && isPlayer2 && !dropToy)
        {
            toy.transform.position = testCube.leftHand2.position;
            toyCollider.enabled = false;
            rb.isKinematic = true;
            

            if (testCube.ReadActionButton() && timer >= 0.3f)
            {
                dropToy = true;
            }
        }

        if (dropToy)
        {
            
            StartCoroutine(DropToy());
        }

        IEnumerator DropToy()
        {
            toyIsPicked = false;
            testCube.leftHandisFull = false;
            toyCollider.enabled = true;
            toyCollider.isTrigger = false;
            rb.isKinematic = false;
              
            yield return new WaitForSeconds(0.3f);
            toy = null;
            dropToy = false;
            timer = 0;
        }

        //if (testCube.leftHandisFull && testCube.rightHandisFull)
        //{
        //    testCube.handIsFull = true;
        //}
        //else if (!testCube.leftHandisFull || !testCube.rightHandisFull)
        //{
        //    testCube.handIsFull = false;
        //}
    }
}
