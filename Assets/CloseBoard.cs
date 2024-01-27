using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CloseBoard : MonoBehaviour
{
    [SerializeField]
    private InputActionReference closeControl;

    [SerializeField] private Scorecards scoreScript;
    private bool canPress = false;

    private void Start()
    {
        scoreScript = this.GetComponent<Scorecards>();
        canPress = true;
    }
    private void OnEnable()
    {

        closeControl.action.Enable();

    }

    private void OnDisable()
    { 
        closeControl.action.Disable();
    }
    private void Update()
    {
        if (closeControl.action.triggered && canPress)
        {
            Debug.Log("Close");
            scoreScript.NextSection();
            canPress = false;
        }
        else if (!closeControl.action.triggered)
        {
            canPress = true;
        }
    }



}
