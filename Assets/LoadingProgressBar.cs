using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    public static LoadingProgressBar instance;

    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private bool loadingTV;
    [SerializeField]
    private bool loadingLevel1;
    [SerializeField]
    private bool loadingMVP;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private bool level1Completed;
    [SerializeField]
    private bool level2Completed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loadingSlider.value = Loader.GetLoadingProgress();
        LoadingScreens();
    }



    public void LoadingScreens()
    {
        if (GameManager.instance.timesEnterHub == 0)
        {
            anim.SetBool("LoadingFight", true);
        }
        else if (GameManager.instance.timesEnterHub == 1)
        {
            if (GameManager.instance.changeSceneTimes != 3 && !GameManager.instance.acceptLalahOrder && !GameManager.instance.accepWertherOrder)
            {
                anim.SetBool("LoadingTV", true);
            }
            if(GameManager.instance.changeSceneTimes == 3 && !GameManager.instance.acceptLalahOrder && !GameManager.instance.accepWertherOrder)
            {
                anim.SetBool("LoadingJuice", true);
            }
            if (GameManager.instance.accepWertherOrder)
            {
                anim.SetBool("LoadingFight", true);
            }
            if (GameManager.instance.acceptLalahOrder)
            {
                anim.SetBool("LoadingLalah", true);
            }
            if(GameManager.instance.changeSceneTimes == 5)
            {
                if(GameManager.instance.LalahRequestWasCompleted || GameManager.instance.WertherRequestWasCompleted)
                {
                    anim.SetBool("LoadingJuice", true);
                }
            }
        }
        else if (GameManager.instance.timesEnterHub == 2)
        {
            if (GameManager.instance.WertherRequestWasCompleted)
            {
                if (!GameManager.instance.acceptLalahOrder)
                {
                    anim.SetBool("LoadingFight", true);
                    level2Completed = true;
                }
                else
                {
                    anim.SetBool("LoadingLalah", true);
                }

            }
            else if (GameManager.instance.LalahRequestWasCompleted )
            {
                if (!GameManager.instance.accepWertherOrder)
                {
                    anim.SetBool("LoadingLalah", true);
                    level1Completed = true;
                }
                else
                {
                    anim.SetBool("LoadingTV", true);
                }

            }
            else
            {
                anim.SetBool("LoadingFight", true);
            }
            
        }
        else if (GameManager.instance.timesEnterHub == 3)
        {
            if (GameManager.instance.WertherRequestWasCompleted && !level1Completed)
            {
                anim.SetBool("LoadingLalah", true);
                level2Completed = true;
            }
            else if (GameManager.instance.LalahRequestWasCompleted && !level2Completed)
            {
                anim.SetBool("LoadingTV", true);
                level1Completed = true;
            }
            else
            {
                anim.SetBool("LoadingJuice", true);
            }

        }
        else if (GameManager.instance.timesEnterHub == 4)
        {
            if (GameManager.instance.WertherRequestWasCompleted && level1Completed)
            {
                anim.SetBool("LoadingTV", true);
            }
            else if (GameManager.instance.LalahRequestWasCompleted && level2Completed)
            {
                anim.SetBool("LoadingLalah", true);
            }
            else
            {
                anim.SetBool("LoadingFight", true);
            }

        }

        //else if (GameManager.instance.changeSceneTimes == 3)
        //{
        //    //out of TV
        //    if(!GameManager.instance.acceptLalahOrder && !GameManager.instance.accepWertherOrder)
        //    {
        //        anim.SetBool("LoadingJuice", true);
        //    }
        //}
        //else if (GameManager.instance.acceptLalahOrder && !GameManager.instance.LalahRequestWasCompleted)
        //{
        //    anim.SetBool("LoadingLalah", true);
        //}
        //else if (GameManager.instance.accepWertherOrder && !GameManager.instance.WertherRequestWasCompleted)
        //{
        //    anim.SetBool("LoadingMVP", true);
        //}

    }
}
