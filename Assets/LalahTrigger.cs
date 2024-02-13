using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class LalahTrigger : MonoBehaviour
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
    private bool npcArrived;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Arrive();
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
        if (Input.GetKeyDown(KeyCode.E) && hasTalkedBefore == true)
        {
            GoToLevelScene();
            dialogueRunner.Stop();
            //dialogueRunner.StartDialogue("Repeat");
        }
    }

    public void GoToLevelScene()
    {
        //SceneManager.LoadScene("PrototypeLevel");
    }


    #region Weather
    private void Arrive()
    {
        if (SceneControl.instance.firstCustomer && !npcArrived)
        {
            StartCoroutine(Walking());
        }
    }


    IEnumerator Walking()
    {
        anim.SetBool("Arrived", true);
        yield return new WaitForSeconds(1.2f);
        smoke.SetActive(false);
        anim.SetBool("Arrived", false);
        npcArrived = true;

    }
    #endregion
}
