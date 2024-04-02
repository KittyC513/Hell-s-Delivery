using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{

    [SerializeField]
    private GameObject canvas;
    private BadgeManager badgeManager;

    // Start is called before the first frame update
    void Start()
    {
        badgeManager = FindAnyObjectByType<BadgeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && GameManager.instance.curSceneName == "Level1")
        {
            GameManager.instance.changeSceneTimes += 1;
            GameManager.instance.LalahRequestWasCompleted = true;

            if(badgeManager != null)
            {
                badgeManager.RunFinalCheck();
            }

            Loader.Load(Loader.Scene.ScoreCards);

        }

        if (Input.GetKey(KeyCode.E) && GameManager.instance.curSceneName == "MVPLevel")
        {
            GameManager.instance.changeSceneTimes += 1;
            GameManager.instance.WertherRequestWasCompleted = true;
            
            if(badgeManager != null)
            {
                badgeManager.RunFinalCheck();
            }

            Loader.Load(Loader.Scene.ScoreCards);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Package" && !GameManager.instance.LalahRequestWasCompleted)
        {
            if(GameManager.instance.curSceneName == "Level1")
            {
                if (canvas != null)
                {
                    //canvas.gameObject.SetActive(true);
                }

                GameManager.instance.changeSceneTimes += 1;

                if(badgeManager != null)
                {
                    badgeManager.RunFinalCheck();
                }

                GameManager.instance.LalahRequestWasCompleted = true;
                Loader.Load(Loader.Scene.ScoreCards);
                
            }

            if (GameManager.instance.curSceneName == "MVPLevel" && !GameManager.instance.WertherRequestWasCompleted)
            {
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(true);
                }

                GameManager.instance.changeSceneTimes += 1;

                if(badgeManager != null)
                {
                    badgeManager.RunFinalCheck();
                }

                GameManager.instance.WertherRequestWasCompleted = true;
                Loader.Load(Loader.Scene.ScoreCards);

            }


        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Package")
        {
            //canvas.gameObject.SetActive(false);
        }
    }




    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        Loader.Load(Loader.Scene.ScoreCards);
    //    }
    //}
}
