using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class LevelDialogue : MonoBehaviour
{
    [SerializeField]
    private static LevelDialogue instance;

    [SerializeField]
    public DialogueRunner dRP1, dRP2, drAll;
    
    [SerializeField]
    private static GameObject LV, DRP1, DRP2, DRAll;



    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        //LV = GameObject.Find("Devil Players");


        DRP1 = GameObject.Find("DRP1");
        //print("DRP1 found" + DRP1);

        DRP2 = GameObject.Find("DRP2");
        //print("DRP2 found" + DRP1);

        DRAll = GameObject.Find("DRAll");
        
        DRP1.SetActive(false);
        DRP2.SetActive(false);
        DRAll.SetActive(false);
        //LV.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //curSceneName = GameManager.instance.curSceneName;

        //if (curSceneName == ("Tutorial") && DRP1 == null || DRP2 == null )
        //{
        //    LV = GameObject.Find("Devil Players");
        //    LV.SetActive(false);

        //    if (DRP1 == null)
        //    {
        //        DRP1 = GameObject.Find("DRP1");
        //    }

        //    if(DRP2 == null)
        //    {
        //        DRP2 = GameObject.Find("DRP2");
        //    }
 
        //}
    }

    public static void ShowDevilPlayer1()
    {
        DRP1.SetActive(true);
        //DRP2.SetActive(false);
    }

    public static void ShowDevilPlayer2()
    {
        DRP2.SetActive(true);
        //DRP1.SetActive(false);
    }

    public static void ShowDevilPlayerAll()
    {
        DRAll.SetActive(true);
    }

    //public static void ShowBoth()
    //{
    //    P1.SetActive(true);
    //    P2.SetActive(true);
    //}

}
