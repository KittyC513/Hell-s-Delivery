using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonMouth : MonoBehaviour
{

    public bool active = false;
    public Material onActive;
    public Material defaulttex;
    public Animator anim;
    public Renderer e1;
    public Renderer e2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        anim.SetBool("Activate", true);
        active = true;
        e1.material = onActive;
        e2.material = onActive;
    }

    public void Deactivate()
    {
        anim.SetBool("Activate", false);
        active = false;
        e1.material = defaulttex;
        e2.material = defaulttex;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && active == true)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {

                Debug.Log("success");
                rb.AddForce(-transform.forward * 2000);
            }
        }
    }

}
