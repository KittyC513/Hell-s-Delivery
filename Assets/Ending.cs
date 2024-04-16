using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{

    [SerializeField]
    private GameObject canvas;
    private BadgeManager badgeManager;
    [SerializeField]
    private GameObject comicCanvas;
    [SerializeField]
    private Animator comicAnim;

    // Start is called before the first frame update
    void Start()
    {
        badgeManager = FindAnyObjectByType<BadgeManager>();

        if (comicCanvas != null)
        {
            comicAnim = comicCanvas.GetComponent<Animator>();
            if (comicAnim != null)
            {
                comicCanvas.SetActive(false);
            }
        }
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
        if (other.gameObject.tag == "Package")
        {
            CompleteTask();
        }

    }

    void CompleteTask()
    {
        if (GameManager.instance.curSceneName == "Level1")
        {
            GameManager.instance.changeSceneTimes += 1;
            GameManager.instance.LalahRequestWasCompleted = true;

            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
            }


            if (badgeManager != null)
            {
                badgeManager.RunFinalCheck();
            }



            Loader.Load(Loader.Scene.ScoreCards);

        }

        if (GameManager.instance.curSceneName == "MVPLevel")
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
            }

            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;


            //if (badgeManager != null)
            //{
            //    badgeManager.RunFinalCheck();
            //}
            GameManager.instance.changeSceneTimes += 1;
            GameManager.instance.WertherRequestWasCompleted = true;
            StartCoroutine(ComicStart());
        }
        SkipComic();
    }

    IEnumerator ComicStart()
    {
        comicCanvas.SetActive(true);
        comicAnim.SetTrigger("Level2");
        yield return new WaitForSeconds(26);
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        Loader.Load(Loader.Scene.ScoreCards);
    }

    void SkipComic()
    {
        if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
        {
            print("StopComic");
            StopCoroutine(ComicStart());
            comicCanvas.SetActive(false);
            GameManager.instance.UnfreezePlayer();
            Loader.Load(Loader.Scene.ScoreCards);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Package")
        {
            //canvas.gameObject.SetActive(false);
        }
    }
}
