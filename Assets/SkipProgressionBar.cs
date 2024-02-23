using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipProgressionBar : MonoBehaviour
{

    [SerializeField]
    private float timer;
    [SerializeField]
    private float maxTimer;
    [SerializeField]
    private Image radialUI;
    [SerializeField]
    private bool origiTimer;


    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        ButtonTrigger();
    }


    void ButtonTrigger()
    {
        if(GameManager.instance.curSceneName == "Tutorial")
        {
            print("Hello");
            if (GameManager.instance.p1.ReadSkipTriggerButton() || GameManager.instance.p2.ReadSkipTriggerButton())
            {
                timer += Time.deltaTime;
                radialUI.enabled = true;
                radialUI.fillAmount = timer;

                //print("timer" + timer);
                if (timer >= maxTimer)
                {
                    timer = maxTimer;
                    radialUI.fillAmount = maxTimer;
                }
            }
            else
            {
                timer = 0;
                radialUI.fillAmount = timer;
            }
        }
        else if (GameManager.instance.curSceneName == "HubStart")
        {
            print("Hello1");
            if (GameManager.instance.p1.ReadSkipTriggerButton() || GameManager.instance.p2.ReadSkipTriggerButton())
            {
                timer += Time.deltaTime;
                radialUI.enabled = true;
                radialUI.fillAmount = timer;

                //print("timer" + timer);
                if (timer >= maxTimer)
                {
                    timer = maxTimer;
                    radialUI.fillAmount = maxTimer;
                }
            }
            else
            {
                timer = 0;
                radialUI.fillAmount = timer;
            }
        }


    }
}
