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

    }
    private void FixedUpdate()
    {
        ButtonTrigger();
    }


    void ButtonTrigger()
    {
        if(GameManager.instance.curSceneName == "Tutorial")
        {
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
            else if (!GameManager.instance.p1.ReadSkipTriggerButton() && !GameManager.instance.p2.ReadSkipTriggerButton())
            {
                timer = 0;
                radialUI.fillAmount = timer;
            }
        }
        else if (GameManager.instance.curSceneName == "HubStart")
        {
            if (GameManager.instance.p1.ReadSkipTriggerButton() || GameManager.instance.p2.ReadSkipTriggerButton())
            {
                timer += Time.deltaTime;
                //print("timer" + timer);
                radialUI.enabled = true;
                radialUI.fillAmount = timer;
                //print("radialUI" + radialUI);

                //print("timer" + timer);
                if (timer >= maxTimer)
                {
                    timer = maxTimer;
                    radialUI.fillAmount = maxTimer;
                }
            }
            else if(!GameManager.instance.p1.ReadSkipTriggerButton() && !GameManager.instance.p2.ReadSkipTriggerButton())
            {
                timer = 0;
                radialUI.fillAmount = timer;
            }
        }
        else if (GameManager.instance.curSceneName == "Level1")
        {
            if (GameManager.instance.p1.ReadSkipTriggerButton() || GameManager.instance.p2.ReadSkipTriggerButton())
            {
                timer += Time.deltaTime;
                //print("timer" + timer);
                radialUI.enabled = true;
                radialUI.fillAmount = timer;
                //print("radialUI" + radialUI);

                //print("timer" + timer);
                if (timer >= maxTimer)
                {
                    timer = maxTimer;
                    radialUI.fillAmount = maxTimer;
                }
            }
            else if (!GameManager.instance.p1.ReadSkipTriggerButton() && !GameManager.instance.p2.ReadSkipTriggerButton())
            {
                timer = 0;
                radialUI.fillAmount = timer;
            }
        }


    }
}
