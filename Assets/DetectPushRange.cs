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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer1 && other.CompareTag("Player"))
        {
            print("InRange");
            testCube.withinPushingRange = true;
        }

        if (isPlayer2 && other.CompareTag("Player"))
        {
            testCube.withinPushingRange = true;
            print("InRange");
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
    }
}
