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
    public AK.Wwise.Event checkPointEnd;

    [SerializeField]
    private bool shouldPlaySound = true;

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject canvas2;
    [SerializeField]
    private bool isActivated;
    [SerializeField]
    private bool firstTime;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("Activate", false);
        ps.Stop();
        animSwitch = true;
        deActivate = true;
        firstTime = true;
        //canvas.SetActive(false);
    }

    private void Update()
    {
        

        if (deActivate == true)
        {
            anim.SetBool("Activate", false);
            ps.Stop();
            deActivate = false;
            shouldPlaySound = true;
            checkPointEnd.Post(this.gameObject);

            isActivated = false;
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

            if (!isActivated && firstTime)
            {
                //StartCoroutine(ShowUI());
                firstTime = false;
            }
            
        }

        if (isActivated)
        {
            //canvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.tag == ("FindScript"))
        {
            activate = true;
        }
    }

    IEnumerator ShowUI()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        isActivated = true;
    }


}
