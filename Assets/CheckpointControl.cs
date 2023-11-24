using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointControl : MonoBehaviour
{

    RespawnControl rc;
    public Animator anim;
    public bool activate;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("Activate", false);
        ps.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {


        
        if (other.gameObject.tag == ("FindScript"))
        {
            Debug.Log("Collide");
            anim.SetBool("Activate", true);
            ps.Play();
        }
    }


}
