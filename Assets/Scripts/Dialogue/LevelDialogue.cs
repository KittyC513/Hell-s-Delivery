using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class LevelDialogue : MonoBehaviour
{
    private static LevelDialogue instance;

    public DialogueRunner dR;

    private static GameObject LVDevil, LVP1, LVP2;


 
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        LVDevil = GameObject.Find("Devil All");
        LVP1 = GameObject.Find("Devil Player 1");
        LVP2 = GameObject.Find("Devil Player 2");

        LVDevil.SetActive(false);
        LVP1.SetActive(false);
        LVP2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ShowDevilAll()
    {
        LVDevil.SetActive(true);
        LVP1.SetActive(false);
        LVP2.SetActive(false);
    }

    public static void ShowDevilPlayer1()
    {
        LVP1.SetActive(true);
        LVDevil.SetActive(false);
        LVP2.SetActive(false);
    }

    public static void ShowDevilPlayer2()
    {
        LVP2.SetActive(true);
        LVP1.SetActive(false);
        LVDevil.SetActive(false);
    }

}
