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
    public ParticleSystem ps;
    public ParticleSystem ps2;
    bool psSwitch = false;
    public GameObject lighting;
    public int force;


    // Start is called before the first frame update
    void Start()
    {
        lighting.SetActive(false);
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
        if (psSwitch == false)
        {
            ps.Play();
            ps2.Play();
            psSwitch = true;
        }
        lighting.SetActive(true);
        
    }

    public void Deactivate()
    {
        anim.SetBool("Activate", false);
        active = false;
        e1.material = defaulttex;
        e2.material = defaulttex;
        if (psSwitch == true)
        {
            ps.Stop();
            ps2.Stop();
            psSwitch = false;
        }
        lighting.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && active == true)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {

                Debug.Log("success");
                rb.AddForce(-transform.forward * force);
            }
        }
    }

}
