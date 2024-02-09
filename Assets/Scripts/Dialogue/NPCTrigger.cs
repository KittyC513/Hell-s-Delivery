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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Arrive();
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


    private void Arrive()
    {
        if (SceneControl.instance.firstCustomer)
        {
            StartCoroutine(Walking());
        }
    }


    IEnumerator Walking()
    {
        anim.SetBool("Arrived", true);
        yield return new WaitForSeconds(3.5f);
        anim.SetBool("Arrived", false);
    }


}
