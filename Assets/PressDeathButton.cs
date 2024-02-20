using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressDeathButton : MonoBehaviour
{
    [SerializeField]
    private float timer1;
    [SerializeField]
    private float timer2;
    [SerializeField]
    private float pressingTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sabotage();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("P1Collider"))
            {
                if (GameManager.instance.p1.ReadActionButton())
                {
                    timer1 += Time.deltaTime;
                }
                else
                {
                    timer1 = 0;
                }
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("P2Collider"))
            {
                if (GameManager.instance.p2.ReadActionButton())
                {
                    timer2 += Time.deltaTime;
                }
                else
                {
                    timer2 = 0;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("P1Collider"))
            {
                timer1 = 0;
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("P2Collider"))
            {
                timer2 = 0;
            }

        }

    }

    void Sabotage()
    {
        if(timer1 >= pressingTimer)
        {
            SceneControl.instance.p1isKilling = true;
            timer1 = 0;

        }

        if (timer2 >= pressingTimer)
        {
            SceneControl.instance.p2isKilling = true;
            timer2 = 0;
        }
    }
}
