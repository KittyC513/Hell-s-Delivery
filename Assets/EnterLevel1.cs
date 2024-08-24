using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel1 : MonoBehaviour
{
    public static EnterLevel1 instance; 

    [SerializeField]
    private Animator sceneTransitionAnim;
    [SerializeField]
    private Animator Char1Anim;
    [SerializeField]
    private Animator Char2Anim;
    [SerializeField]
    private GameObject transition;


    // Start is called before the first frame update
    void Start()
    {
        transition.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    GameManager.instance.changeSceneTimes = 4;
        //    EnterLevel();

        //}
        //if (Input.GetKey(KeyCode.Alpha0))
        //{
        //    GameManager.instance.changeSceneTimes = 2;
        //    EnterTutorial();

        //}

        //if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    GameManager.instance.changeSceneTimes = 6;
        //    EnterMVPLevel();

        //}
    }

    public void EnterLevel()
    {
        GameManager.instance.sceneChanged = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        GameManager.instance.changeSceneTimes = 4;
        Loader.Load(Loader.Scene.Level1);
    }

    public void EnterTutorial()
    {
        //transition.SetActive(true);
        //sceneTransitionAnim.SetTrigger("End");
        //Char1Anim.SetTrigger("End");
        //Char2Anim.SetTrigger("End");
        //yield return new WaitForSeconds(1);
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //sceneTransitionAnim.SetTrigger("Start");
        //Char1Anim.SetTrigger("Start");
        //Char2Anim.SetTrigger("Start");
        GameManager.instance.changeSceneTimes = 2;
        GameManager.instance.sceneChanged = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        Loader.Load(Loader.Scene.Tutorial);
    }
    public void EnterMVPLevel()
    {
        GameManager.instance.changeSceneTimes = 6;
        GameManager.instance.sceneChanged = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;

        Loader.Load(Loader.Scene.MVPLevel);
    }

    public void EnterLeve3()
    {
        GameManager.instance.changeSceneTimes = 7;
        GameManager.instance.sceneChanged = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;

        Loader.Load(Loader.Scene.Level3);
    }
}
