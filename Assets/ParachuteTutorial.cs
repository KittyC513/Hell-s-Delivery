using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<TestCube>().isPlayer1)
            {
                GameManager.instance.p1.inParachuteArea = true;
            }
            if (other.GetComponent<TestCube>().isPlayer2)
            {
                GameManager.instance.p2.inParachuteArea = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<TestCube>().isPlayer1)
            {
                GameManager.instance.p1.inParachuteArea = false;
            }
            if (other.GetComponent<TestCube>().isPlayer2)
            {
                GameManager.instance.p2.inParachuteArea = false;
            }
        }
    }
}
