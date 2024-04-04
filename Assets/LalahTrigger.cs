using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class LalahTrigger : MonoBehaviour
{
    [SerializeField]
    public SceneControl sC;
    public bool hasTalkedBefore = false;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject npc;
    [SerializeField]
    public GameObject smoke;
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
        LalaLeave();
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
        //if (Input.GetKeyDown(KeyCode.E) && hasTalkedBefore == true)
        //{
        //    GoToLevelScene();
        //    dialogueRunner.Stop();
        //    //dialogueRunner.StartDialogue("Repeat");
        //}
    }

    public void GoToLevelScene()
    {
        //SceneManager.LoadScene("PrototypeLevel");
    }


    #region Lalah
    private void Arrive()
    {
        if (SceneControl.instance.firstCustomer && !npcArrived && !GameManager.instance.WertherRequestWasCompleted && !GameManager.instance.LalahRequestWasCompleted)
        {
            StartCoroutine(Walking());
        }

        if (npcArrived && !dialogueEnd || GameManager.instance.WertherRequestWasCompleted || GameManager.instance.LalahRequestWasCompleted)
        {
            smoke.SetActive(false);
        }
        else if(dialogueEnd && !GameManager.instance.WertherRequestWasCompleted && !GameManager.instance.LalahRequestWasCompleted)
        {
            smoke.SetActive(true);
        }
    }


    IEnumerator Walking()
    {
        if (GameManager.instance.timesEnterHub == 1)
        {
            anim.SetBool("Arrived", true);
            yield return new WaitForSeconds(1.2f);
            smoke.SetActive(false);
            anim.SetBool("Arrived", false);
            npcArrived = true;
        }
    }

    public void LalaLeave()
    {
        if (dialogueEnd && !isLeaving)
        {           
            StartCoroutine(Leaving());
            GameManager.instance.LalahLeft = true;
        }
    }


    IEnumerator Leaving()
    {
        
        anim.SetTrigger("isLeaving");
        yield return new WaitForSeconds(1.2f);
        //smoke.SetActive(false);
        //anim.SetBool("Arrived", false);
        //isLeaving = true;
        this.gameObject.SetActive(false);
        isLeaving = true;
    }
    #endregion
}
