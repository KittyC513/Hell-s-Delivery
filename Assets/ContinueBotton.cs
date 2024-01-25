using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class ContinueBotton : MonoBehaviour
{
    public static ContinueBotton instance; 
    [SerializeField]
    private InputActionReference continueControl;
    [SerializeField]
    public LineView lineView;


    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {

        continueControl.action.Enable();

    }

    private void OnDisable()
    {

        continueControl.action.Disable();
    }

    public void PressContinue()
    {
        SceneControl.LVNPC.GetComponent<LineView>().OnContinueClicked();
        lineView = FindObjectOfType<LineView>();
        lineView.OnContinueClicked();
     
    }

}
