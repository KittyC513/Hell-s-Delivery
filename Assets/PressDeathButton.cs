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
    [SerializeField]
    private bool hasTriggered1;
    [SerializeField]
    private bool hasTriggered2;
    [SerializeField]
    private bool isSaving;
    [SerializeField]
    private bool isSaving1;
    [SerializeField]
    private bool isKilling;
    [SerializeField]
    private bool isKilling1;

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
            if (other.gameObject.layer == LayerMask.NameToLayer("P1Collider") && this.gameObject.tag == "OnDeathButton")
            {
                if (GameManager.instance.p1.ReadActionButton())
                {
                    timer1 += Time.deltaTime;
                    isKilling = true;
                }
                else
                {
                    isKilling = false;
                    timer1 = 0;
                    SceneControl.instance.p1isKilling = false;
                }
            } else if(other.gameObject.layer == LayerMask.NameToLayer("P1Collider") && this.gameObject.tag == "OnSavingButton")
            {
                if (GameManager.instance.p1.ReadActionButton())
                {
                    timer1 += Time.deltaTime;
                    isSaving = true;
                }
                else
                {
                    isSaving = false;
                    timer1 = 0;
                    SceneControl.instance.p1isKilling = false;             
                }
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("P2Collider") && this.gameObject.tag == "OnDeathButton")
            {
                if (GameManager.instance.p2.ReadActionButton())
                {
                    isKilling1 = true;
                    timer2 += Time.deltaTime;
                }
                else
                {
                    isKilling1 = false;
                    timer2 = 0;
                    SceneControl.instance.p2isKilling = false;
                }
            }else if(other.gameObject.layer == LayerMask.NameToLayer("P2Collider") && this.gameObject.tag == "OnSavingButton")
            {
                if (GameManager.instance.p2.ReadActionButton())
                {
                    isSaving1 = true;
                    timer2 += Time.deltaTime;
                }
                else
                {
                    isSaving1 = false;
                    timer2 = 0;
                    SceneControl.instance.p2isKilling = false;
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
                isKilling = false;
                isSaving = false;
                timer1 = 0;
                SceneControl.instance.p1isKilling = false;
                hasTriggered1 = false;

            }
            if (other.gameObject.layer == LayerMask.NameToLayer("P2Collider"))
            {
                isKilling1 = false;
                isSaving1 = false;
                timer2 = 0;
                SceneControl.instance.p2isKilling = false;
                hasTriggered2 = false;
            }

        }

    }

    void Sabotage()
    {
        if (isKilling)
        {
            if (timer1 >= pressingTimer && !hasTriggered1)
            {
                SceneControl.instance.p1isKilling = true;
                timer1 = 0;
                hasTriggered1 = true;
            }
        } else if (isSaving)
        {
            if (timer1 >= pressingTimer && !hasTriggered1)
            {
                SceneControl.instance.p1IsSaving = true;
                timer1 = 0;
                hasTriggered1 = true;
            }
        }

        if (isKilling1)
        {
            if (timer2 >= pressingTimer && !hasTriggered2)
            {
                SceneControl.instance.p2isKilling = true;
                timer2 = 0;
                hasTriggered2 = true;
            }
        } else if (isSaving1)
        {
            if (timer2 >= pressingTimer && !hasTriggered2)
            {
                SceneControl.instance.p2IsSaving = true;
                timer2 = 0;
                hasTriggered2 = true;
            }
        }


    }

}
