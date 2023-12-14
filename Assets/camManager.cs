using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camManager : MonoBehaviour
{
    public GameObject[] players;
    public Camera cam1;
    public Camera cam2;
    public Camera cutCam;
    public float time;
    [SerializeField]
    float timer;
    public Animator anim;
    bool leftActive;
    bool rightActive;
    bool lSwitch = false;
    bool rSwitch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            players = GameObject.FindGameObjectsWithTag("Camera");

        if (!leftActive && !rightActive)
        {
            cam1 = players[0].gameObject.GetComponent<Camera>();
            cam2 = players[1].gameObject.GetComponent<Camera>();
        }
            
        



        if (leftActive == true)
        {
            anim.SetBool("right", false);
            if (time > timer)
            {
                if (cam1 != null)
                {
                    cam1.gameObject.SetActive(false);
                }
                if (cam2 != null)
                {
                    cam2.gameObject.SetActive(false);
                }

                cutCam.gameObject.SetActive(true);
                anim.SetBool("left", true);

                timer += Time.deltaTime;

            }
            else
            {
                if (cam1 != null)
                {
                    cam1.gameObject.SetActive(true);
                }
                if (cam2 != null)
                {
                    cam2.gameObject.SetActive(true);
                }
                cutCam.gameObject.SetActive(false);

                
                leftActive = false;

                timer = 0;

            }
        }

        if (rightActive == true)
        {
            anim.SetBool("left", false);
            if (time > timer)
            {
                if (cam1 != null)
                {
                    cam1.gameObject.SetActive(false);
                }
                if (cam2 != null)
                {
                    cam2.gameObject.SetActive(false);
                }

                cutCam.gameObject.SetActive(true);
                anim.SetBool("right", true);

                timer += Time.deltaTime;

            }
            else
            {
                if (cam1 != null)
                {
                    cam1.gameObject.SetActive(true);
                }
                if (cam2 != null)
                {
                    cam2.gameObject.SetActive(true);
                }
                cutCam.gameObject.SetActive(false);

                timer = 0;
                rightActive = false;
            }
        }
        
    }

    public void ActivateCutsceneLeft()
    {
        if (lSwitch == false)
        {
            leftActive = true;
            lSwitch = true;
        }
        
    }

    public void ActivateCutsceneRight()
    {
        if (rSwitch == false)
        {
            rightActive = true;
            rSwitch = true;
        }
        

    }
}
