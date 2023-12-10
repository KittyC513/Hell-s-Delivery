using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointControl : MonoBehaviour
{

    RespawnControl rc;
    public Animator anim;
    public bool activate;
    public ParticleSystem ps;
    public Animator cubeAnim;
    bool animSwitch;
    [SerializeField]
    GameObject[] cps;
    public GameObject cpParent;
    public bool deActivate;
    RespawnControl rsc;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("Activate", false);
        ps.Stop();
        animSwitch = true;
        deActivate = false;
        
    }

    private void Update()
    {
        

        if (deActivate == true)
        {
            anim.SetBool("Activate", false);
            ps.Stop();
            deActivate = false;
        }

        if (activate == true)
        {
            anim.SetBool("Activate", true);
            ps.Play();
            if (animSwitch == true)
            {
                cubeAnim.SetTrigger("GemActivate");
                animSwitch = false;
            }
            activate = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        
        if (other.gameObject.tag == ("FindScript"))
        {
            activate = true;
            
        }
    }


}
