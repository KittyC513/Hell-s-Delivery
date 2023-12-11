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

    public AK.Wwise.Event checkPointGetSound;
    private bool shouldPlaySound = true;

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
            shouldPlaySound = true;
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
            if (shouldPlaySound) checkPointGetSound.Post(this.gameObject);
            shouldPlaySound = false;
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
