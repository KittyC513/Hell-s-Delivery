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
        if (Input.GetKey(KeyCode.R))
        {
            EnterLevel();
        }
        if (Input.GetKey(KeyCode.T))
        {
            EnterTutorial();
        }

        if (Input.GetKey(KeyCode.Y))
        {
            EnterMVPLevel();
        }
    }

    public void EnterLevel()
    {
        GameManager.instance.sceneChanged = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;

        Loader.Load(Loader.Scene.Level1);
    }

    public void EnterTutorial()
    {
        //GameManager.instance.sceneChanged = true;
        //GameManager.instance.p1.isFreeze = false;
        //GameManager.instance.p2.isFreeze = false;

        //Loader.Load(Loader.Scene.Tutorial);
        StartCoroutine(LoadTutorial());

    }

    IEnumerator LoadTutorial()
    {
        transition.SetActive(true);
        sceneTransitionAnim.SetTrigger("End");
        Char1Anim.SetTrigger("End");
        Char2Anim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        sceneTransitionAnim.SetTrigger("Start");
        Char1Anim.SetTrigger("Start");
        Char2Anim.SetTrigger("Start");
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
    }
    public void EnterMVPLevel()
    {
        GameManager.instance.sceneChanged = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;

        Loader.Load(Loader.Scene.MVPLevel);
    }
}
