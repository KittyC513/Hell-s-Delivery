using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CloseBoard : MonoBehaviour
{
    [SerializeField]
    private InputActionReference closeControl;



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
        if (closeControl.action.triggered)
        {
            Debug.Log("Close");
            SceneManager.LoadScene("HubEnd");
        }
    }



}
