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
    public bool startActivated = false;



    // Start is called before the first frame update
    void Start()
    {
        lighting.SetActive(false);

        if (startActivated)
        {
            anim.SetBool("Activate", true);
            active = true;
            e1.material = onActive;
            e2.material = onActive;
            ps.Play();
            ps2.Play();
            psSwitch = true;
            lighting.SetActive(true);
        }
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

               
                Vector3 direction = other.transform.position - transform.position;
                float distance = direction.magnitude;

                Debug.Log(distance);
                rb.AddForce(-transform.forward * force/ (distance / 10));
            }
        }
    }

}
