using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camManager : MonoBehaviour
{

    public static camManager instance;

    public GameObject[] players;
    public GameObject cam1;
    public GameObject cam2;
    public Camera cutCam;
    [SerializeField]
    public Camera puzzle1Cam, puzzle1CamP2;
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
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Camera");

        if (!leftActive && !rightActive)
        {
            //cam1 = players[0].gameObject.GetComponent<Camera>();
            //cam2 = players[1].gameObject.GetComponent<Camera>();
            cam1 = GameManager.instance.cam1;
            cam2 = GameManager.instance.cam2;
        }
           

        if (leftActive == true)
        {
            anim.SetBool("right", false);
            if (time > timer)
            {
                if (cam1 != null)
                {
                    cam1.SetActive(false);
                }
                if (cam2 != null)
                {
                    cam2.SetActive(false);
                }

                cutCam.gameObject.SetActive(true);
                anim.SetBool("left", true);

                timer += Time.deltaTime;

            }
            else
            {
                if (cam1 != null)
                {
                    cam1.SetActive(true);
                }
                if (cam2 != null)
                {
                    cam2.SetActive(true);
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
                    cam1.SetActive(false);
                }
                if (cam2 != null)
                {
                    cam2.SetActive(false);
                }

                cutCam.gameObject.SetActive(true);
                anim.SetBool("right", true);

                timer += Time.deltaTime;

            }
            else
            {
                if (cam1 != null)
                {
                    cam1.SetActive(true);
                }
                if (cam2 != null)
                {
                    cam2.SetActive(true);
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

    public void switchPuzzle1Cam()
    {
        cam1.SetActive(false);
        puzzle1Cam.gameObject.SetActive(true);
    }

    public void switchPuzzle1CamP2()
    {
        cam2.SetActive(false);
        puzzle1CamP2.gameObject.SetActive(true);
    }

    public void switchPuzzle1CamBack()
    {
        cam1.SetActive(true);
        puzzle1Cam.gameObject.SetActive(false);
    }

    public void switchPuzzle1CamBackP2()
    {
        cam2.SetActive(true);
        puzzle1CamP2.gameObject.SetActive(false);
    }



}
