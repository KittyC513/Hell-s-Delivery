using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geiser : MonoBehaviour
{

    public bool active;
    ParticleSystem ps;
    BoxCollider bc;

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
            ps.Play();
            bc.enabled = true;
        } else
        {
            ps.Stop();
            bc.enabled = false;
        }
    }

    public void ActivateGeiser()
    {

        active = true;
    }

    public void DeactivateGeiser()
    {

        active = false;

    }
}
