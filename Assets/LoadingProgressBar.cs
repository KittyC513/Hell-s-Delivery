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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loadingSlider.value = Loader.GetLoadingProgress();
        LoadingToTV();
    }

    public void LoadingToTV()
    {
        if (GameManager.instance.changeSceneTimes == 2)
        {
            anim.SetBool("LoadingTV", true);
        }
        else
        {
            anim.SetBool("LoadingTV", false);
        }

    }
}
