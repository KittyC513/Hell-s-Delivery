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
    private GameObject toy, box;
    [SerializeField]
    private bool toyIsPicked, boxIsPicked;
    [SerializeField]
    private bool dropToy, dropBox;
    [SerializeField]
    private BoxCollider toyCollider, boxCollider;
    [SerializeField]
    private Rigidbody rb, rbBox;
    [SerializeField]
    private float timer, timerBox;
    [SerializeField]
    private bool withinBoxRange;
    [SerializeField]
    private bool takeBox;

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
        //TakeBox();
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

        //if (isPlayer1 && other.CompareTag("Box"))
        //{
        //    withinBoxRange = false;
        //    if (!testCube.itemIsFull)
        //    {
        //        box = null;
        //    }
        //}

        //if (isPlayer2 && other.CompareTag("Box"))
        //{
        //    withinBoxRange = false;
        //    if (!testCube.itemIsFull)
        //    {
        //        box = null;
        //    }
        //}
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

        //if (isPlayer1 && other.CompareTag("Box") && testCube.objectGrabbable == null)
        //{
        //    withinBoxRange = true;
        //    if (box == null && !dropBox)
        //    {
        //        box = other.gameObject;
        //        boxCollider = box.GetComponent<BoxCollider>();
        //        rbBox = box.GetComponent<Rigidbody>();
        //    }
        //}

        //if (isPlayer2 && other.CompareTag("Box") && testCube.objectGrabbable == null)
        //{
        //    withinBoxRange = true;
        //    if (box == null && !dropBox)
        //    {
        //        box = other.gameObject;
        //        boxCollider = box.GetComponent<BoxCollider>();
        //        rbBox = box.GetComponent<Rigidbody>();
        //    }
        //}
    }

    private void TakeToy()
    {
        if(testCube.withinToyRange && testCube.ReadActionButtonArcade() && !dropToy)
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

            if (testCube.ReadActionButtonArcade() && timer >= 0.3f)
            {
                dropToy = true;
            }
        }


        if (testCube.leftHandisFull && isPlayer2 && !dropToy)
        {
            toy.transform.position = testCube.leftHand2.position;
            toyCollider.enabled = false;
            rb.isKinematic = true;
            

            if (testCube.ReadActionButtonArcade() && timer >= 0.3f)
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
    }


    private void TakeBox()
    {
        if (withinBoxRange && testCube.ReadActionButtonArcade() && !dropBox)
        {
            boxIsPicked = true;

        }

        if (boxIsPicked)
        {
            if (timerBox < 1)
            {
                timerBox += Time.deltaTime;
            }
            else
            {
                timerBox = 1;
            }
            if (isPlayer1)
            {

                //toy.transform.position = testCube.leftHand1.position;
                testCube.itemIsFull = true;
                //if(testCube.leftHandisFull && !testCube.rightHandisFull)
                //{
                //    toy.transform.position = testCube.rightHand1.position;
                //    testCube.rightHandisFull = true;
                //}

            }

            if (isPlayer2)
            {

                //toy.transform.position = testCube.leftHand2.position;
                testCube.itemIsFull = true;
                //if (testCube.leftHandisFull && !testCube.rightHandisFull)
                //{
                //    toy.transform.position = testCube.rightHand2.position;
                //    testCube.rightHandisFull = true;
                //}
            }

        }

        if (testCube.itemIsFull && isPlayer1 && !dropBox)
        {
            box.transform.position = testCube.itemContainer.position;
            boxCollider.enabled = false;
            rbBox.isKinematic = true;

            if (testCube.ReadActionButtonArcade() && timerBox >= 0.3f)
            {
                dropBox = true;
            }
        }


        if (testCube.itemIsFull && isPlayer2 && !dropBox)
        {
            box.transform.position = testCube.itemContainer.position;
            boxCollider.enabled = false;
            rbBox.isKinematic = true;


            if (testCube.ReadActionButtonArcade() && timerBox >= 0.3f)
            {
                dropBox = true;
            }
        }

        if (dropBox)
        {

            StartCoroutine(DropBox());
        }

        IEnumerator DropBox()
        {
            boxIsPicked = false;
            testCube.itemIsFull = false;
            boxCollider.enabled = true;
            boxCollider.isTrigger = false;
            rbBox.isKinematic = false;

            yield return new WaitForSeconds(0.3f);
            box = null;
            dropBox = false;
            timerBox = 0;
        }
    }
}
