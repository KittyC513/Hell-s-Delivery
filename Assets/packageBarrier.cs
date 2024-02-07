using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packageBarrier : MonoBehaviour
{
    public MeshCollider Bc;
    public ObjectGrabbable Og;
    bool blocking;
    public Material passable;
    public Material solid;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        blocking = true;
        Bc = gameObject.GetComponent<MeshCollider>();
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pTrigger")
        {
            Bc.enabled = false;
            blocking = false;
            rend.material = passable;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "pTrigger")
        {
            Bc.enabled = true;
            blocking = true;
            rend.material = solid;
        }
    }
}
