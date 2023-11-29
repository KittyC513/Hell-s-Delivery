using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geiser : MonoBehaviour
{

    public bool active;
    ParticleSystem ps;
    BoxCollider bc;
    bool activeSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        bc = GetComponent<BoxCollider>();

        ps.Stop();
        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            
        } else
        {
            
        }
    }

    public void ActivateGeiser()
    {
        if (activeSwitch)
        {
            Debug.Log("FanActive");
            active = true;
            ps.Play();
            bc.enabled = true;
            activeSwitch = false;
        }
        
    }

    public void DeactivateGeiser()
    {
        if (activeSwitch == false)
        {
            Debug.Log("FanDeactive");
            active = false;
            ps.Stop();
            bc.enabled = false;
            activeSwitch = true;
        }
        

    }
}
