using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class NPCTrigger : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public bool hasTalkedBefore = false;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject npc;
    [SerializeField]
    private GameObject smoke;
    [SerializeField]
    public bool npcArrived;
    [SerializeField]
    public bool dialogueEnd;
    [SerializeField]
    public bool isLeaving;

    // Start is called before the first frame update

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Arrive();
        WeatherLeave();
        //ArriveLalah();
        //if (Input.GetKeyDown(KeyCode.E) && hasTalkedBefore == false)
        //{
        //    dialogueRunner.StartDialogue("HubEnd");
        //    hasTalkedBefore = true;
        //}
        //else
        //{
        //    Repeat();
        //}
    }
    public void Repeat()
    {


    }

    public void GoToLevelScene()
    {
        //SceneManager.LoadScene("PrototypeLevel");
    }


    #region Weather
    private void Arrive()
    {
        if (SceneControl.instance.secondCustomer && !npcArrived && !GameManager.instance.WertherRequestWasCompleted && !GameManager.instance.LalahRequestWasCompleted)
        {
            StartCoroutine(Walking());
        }
        if (npcArrived && !dialogueEnd)
        {
            smoke.SetActive(false);
        }
        else if (dialogueEnd && !GameManager.instance.WertherRequestWasCompleted && !GameManager.instance.LalahRequestWasCompleted)
        {
            smoke.SetActive(true);
        }
    }

    IEnumerator Walking()
    {
        if(!SceneControl.instance.level1Overview || GameManager.instance.timesEnterHub <=1 || GameManager.instance.WertherRequestWasCompleted || GameManager.instance.LalahRequestWasCompleted)
        {
            anim.SetBool("Arrived", true);
            yield return new WaitForSeconds(1.2f);
            smoke.SetActive(false);
            anim.SetBool("Arrived", false);
            npcArrived = true;
        }
    }

    public void WeatherLeave()
    {
        if (dialogueEnd && !isLeaving)
        {
            StartCoroutine(Leaving());
        }

    }


    public IEnumerator Leaving()
    {
        anim.SetTrigger("isLeaving");
        yield return new WaitForSeconds(1.2f);
        //smoke.SetActive(false);
        //anim.SetBool("Arrived", false);
        this.gameObject.SetActive(false);
        isLeaving = true;
    }
    #endregion

}
