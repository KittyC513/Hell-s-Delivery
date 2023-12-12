using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class LineViewShow : MonoBehaviour
{
    private static GameObject LVPlayers;
    public DialogueRunner dR;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLV()
    {
        LVPlayers = GameObject.Find("Line View Players");

        LVPlayers.SetActive(true);
        dR.StartDialogue("HubStart");
    }
}
