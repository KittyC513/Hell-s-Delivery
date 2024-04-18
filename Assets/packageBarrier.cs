using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packageBarrier : MonoBehaviour
{
    public MeshCollider Bc;
    public ObjectGrabbable Og;
    [SerializeField]
    bool blocking;
    public Material passable;
    public Material solid;
    Renderer rend;
    [SerializeField]
    private GameObject instruction;

    [SerializeField]
    PackagePass packagePass;

    // Start is called before the first frame update
    void Start()
    {
        blocking = true;
        Bc = gameObject.GetComponent<MeshCollider>();
        rend = gameObject.GetComponent<Renderer>();
        //instruction = this.gameObject.transform.Find("Canvas").transform;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Package")
        {
            Bc.enabled = false;
            blocking = false;
            rend.material = passable;

            instruction.SetActive(false);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            instruction.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Package")
        {
            if (packagePass.packageIsDetected == false)
            {
                Bc.enabled = true;
                blocking = true;
                rend.material = solid;
                
            }

            instruction.SetActive(false);

        }
    }
}
