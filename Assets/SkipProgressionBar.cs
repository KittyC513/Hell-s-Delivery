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
    [SerializeField]
    private GameObject Entext;
    [SerializeField]
    private GameObject Chtext;


    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        switchText();
    }
    private void FixedUpdate()
    {
        ButtonTrigger();
    }

    private void switchText()
    {
        if(Entext != null && Chtext != null)
        {
            if (!GameManager.instance.isCh)
            {
                Entext.SetActive(true);
                Chtext.SetActive(false);
            }
            else
            {
                Entext.SetActive(false);
                Chtext.SetActive(true);
            }
        }
    }


    void ButtonTrigger()
    {
        if(GameManager.instance.curSceneName == "Tutorial")
        {
            if (GameManager.instance.p1.ReadSkipTriggerButtonArcade() || GameManager.instance.p2.ReadSkipTriggerButtonArcade())
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
            else if (!GameManager.instance.p1.ReadSkipTriggerButtonArcade() && !GameManager.instance.p2.ReadSkipTriggerButtonArcade())
            {
                timer = 0;
                radialUI.fillAmount = timer;
            }
        }
        else if (GameManager.instance.curSceneName == "HubStart")
        {
            if (GameManager.instance.p1.ReadSkipTriggerButtonArcade() || GameManager.instance.p2.ReadSkipTriggerButtonArcade())
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
            else if(!GameManager.instance.p1.ReadSkipTriggerButtonArcade() && !GameManager.instance.p2.ReadSkipTriggerButtonArcade())
            {
                timer = 0;
                radialUI.fillAmount = timer;
            }
        }
        else if (GameManager.instance.curSceneName == "Level1" || GameManager.instance.curSceneName == "MVPLevel")
        {
            if (GameManager.instance.p1.ReadSkipTriggerButtonArcade() || GameManager.instance.p2.ReadSkipTriggerButtonArcade())
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
            else if (!GameManager.instance.p1.ReadSkipTriggerButtonArcade() && !GameManager.instance.p2.ReadSkipTriggerButtonArcade())
            {
                timer = 0;
                radialUI.fillAmount = timer;
            }
        }


    }
}
