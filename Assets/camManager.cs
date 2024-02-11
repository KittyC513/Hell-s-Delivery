using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camManager : MonoBehaviour
{

    public static camManager instance;

    [Header("Identify Variables")]
    public GameObject[] players;
    public GameObject cam1;
    public GameObject cam2;
    [SerializeField]
    private float freezeTimer;

    [Header("Puzzle 1")]
    [SerializeField]
    public Camera puzzle1Cam, puzzle1CamP2, puzzle1CutScene;
    public float time;
    [SerializeField]
    private bool puzzle1ButtonHasTriggered, puzzle1SwitchedCamP1, puzzle1SwitchedCamBackP1, puzzle1SwitchedCamP2, puzzle1SwitchedCamBackP2;


    [Header("Puzzle 2")]
    [SerializeField]
    public Camera puzzle2Cam, puzzle2Cam2, puzzle2CamP2, puzzle2CamP1, puzzle2CutScene;
    public float timePuzzle2;
    [SerializeField]
    private bool puzzle2ButtonHasTriggered,puzzle2SwitchedCamP1L, puzzle2SwitchedCamP1R, puzzle2SwitchedCamBackP1L, puzzle2SwitchedCamBackP1R, puzzle2SwitchedCamP2L, puzzle2SwitchedCamP2R, puzzle2SwitchedCamBackP2L,puzzle2SwitchedCamBackP2R;

    [Header("Bridge")]
    [SerializeField]
    public Camera cutCam;
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
        BridgeCamControl();

    }

    #region Bridge Camera Control
    private void BridgeCamControl()
    {
        players = GameObject.FindGameObjectsWithTag("Camera");

        if (GameManager.instance.curSceneName == "MVPLevel")
        {
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


    #endregion

    #region Puzzle1 Camera Control
    public void switchPuzzle1Cam()
    {
        //cam1.SetActive(false);
        //puzzle1Cam.gameObject.SetActive(true);
        if (!puzzle1SwitchedCamP1)
        {
            cam1.SetActive(false);
            puzzle1Cam.gameObject.SetActive(true);
            StartCoroutine(changeCameraPuzzle1P1());
            puzzle1SwitchedCamP1 = true;
        }
    }

    IEnumerator changeCameraPuzzle1P1()
    {
        GameManager.instance.p1.isFreeze = true;
        yield return new WaitForSeconds(freezeTimer);
        GameManager.instance.p1.isFreeze = false;
        print("Working");
    }

    public void switchPuzzle1CamP2()
    {
        if (!puzzle1SwitchedCamP2)
        {
            cam2.SetActive(false);
            puzzle1CamP2.gameObject.SetActive(true);
            StartCoroutine(changeCameraPuzzle1P2());
            puzzle1SwitchedCamP2 = true;
        }


    }

    IEnumerator changeCameraPuzzle1P2()
    {
        GameManager.instance.p2.isFreeze = true;
        yield return new WaitForSeconds(freezeTimer);
        GameManager.instance.p2.isFreeze = false;
        print("Working2");
    }

    public void switchPuzzle1CutScene()
    {
        cam2.SetActive(false);
        puzzle1CutScene.gameObject.SetActive(true);
        StartCoroutine(changeCameraPuzzle1P1());

    }
    public void switchPuzzle1CamBack()
    {
        cam1.SetActive(true);
        puzzle1Cam.gameObject.SetActive(false);
        puzzle1SwitchedCamP1 = false;
        if (!puzzle1SwitchedCamBackP1)
        {
            StartCoroutine(changeCameraPuzzle1P1());
            puzzle1SwitchedCamBackP1 = true;
        }


    }

    public void switchPuzzle1CamBackP2()
    {
        cam2.SetActive(true);
        puzzle1CamP2.gameObject.SetActive(false);
        puzzle1SwitchedCamP2 = false;
        if (!puzzle1SwitchedCamBackP2)
        {
            StartCoroutine(changeCameraPuzzle1P2());
            puzzle1SwitchedCamBackP2 = true;
        }
    }

    public void switchPuzzle1CamBackCutScene()
    {
        cam2.SetActive(true);
        puzzle1CutScene.gameObject.SetActive(false);

    }

    public void FirstTimeTriggerButtonPuzzle1()
    {
        if (!puzzle1ButtonHasTriggered)
        {
            StartCoroutine(changeCameraViewPuzzle1());
        }
    }

    IEnumerator changeCameraViewPuzzle1()
    {
        GameManager.instance.p2.isFreeze = true;
        switchPuzzle1CutScene();
        yield return new WaitForSeconds(3);
        switchPuzzle1CamBackCutScene();
        puzzle1ButtonHasTriggered = true;
        GameManager.instance.p2.isFreeze = false;
    }

    #endregion


    #region Puzzle2 Camera Control

      public void switchPuzzle2CamL()
    {
        if (!puzzle2SwitchedCamP1L)
        {
            cam1.SetActive(false);
            puzzle2Cam.gameObject.SetActive(true);
            StartCoroutine(changeCameraPuzzle1P1());
            puzzle2SwitchedCamP1L = true;
        }


    }

    public void switchPuzzle2CamR()
    {
        if (!puzzle2SwitchedCamP1R)
        {
            cam1.SetActive(false);
            puzzle2Cam2.gameObject.SetActive(true);
            StartCoroutine(changeCameraPuzzle1P2());
            puzzle2SwitchedCamP1R = true;
        }
    }



    public void switchPuzzle2CamP2()
    {
        if (!puzzle2SwitchedCamP2R)
        {
            cam2.SetActive(false);
            puzzle2CamP2.gameObject.SetActive(true);
        }

    }

    public void switchPuzzle2CamP2L()
    {
        if (!puzzle2SwitchedCamP2L)
        {
            cam2.SetActive(false);
            puzzle2CamP1.gameObject.SetActive(true);
        }

    }


    public void switchPuzzle2CamBack()
    {
        cam1.SetActive(true);
        puzzle2Cam.gameObject.SetActive(false);
        puzzle2SwitchedCamP1L = false;
        if (!puzzle2SwitchedCamBackP1L)
        {
            StartCoroutine(changeCameraPuzzle1P1());
            puzzle2SwitchedCamBackP1L = true;
        }


    }

    public void switchPuzzle2CamBackR()
    {
        cam1.SetActive(true);
        puzzle2Cam2.gameObject.SetActive(false);
        puzzle2SwitchedCamP1R = false;
        if (!puzzle2SwitchedCamBackP1R)
        {
            StartCoroutine(changeCameraPuzzle1P2());
            puzzle2SwitchedCamBackP1R = true;
        }
    }

    public void switchPuzzle2CamBackP2()
    {
        cam2.SetActive(true);
        puzzle2CamP2.gameObject.SetActive(false);
        puzzle2SwitchedCamP2R = false;
        if (!puzzle2SwitchedCamBackP2R)
        {
            StartCoroutine(changeCameraPuzzle1P2());
            puzzle2SwitchedCamBackP2R = true;
        }
    }

    public void switchPuzzle2CamBackP2L()
    {
        cam2.SetActive(true);
        puzzle2CamP1.gameObject.SetActive(false);
        puzzle2SwitchedCamP2L = false;
        if (!puzzle2SwitchedCamBackP2L)
        {
            StartCoroutine(changeCameraPuzzle1P1());
            puzzle2SwitchedCamBackP2L = true;
        }
    }

    #endregion


}
