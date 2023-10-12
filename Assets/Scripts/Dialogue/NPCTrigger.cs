using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPCTrigger : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public bool hasTalkedBefore = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && hasTalkedBefore == false)
        {
            dialogueRunner.StartDialogue("HubStart");
            hasTalkedBefore = true;
        }
        else
        {
            Repeat();
        }
    }
    public void Repeat()
    {
        if (Input.GetKeyDown(KeyCode.E) && hasTalkedBefore == true)
        {
            dialogueRunner.Stop();
            dialogueRunner.StartDialogue("Repeat");
        }
    }
}
