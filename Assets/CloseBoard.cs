using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CloseBoard : MonoBehaviour
{

    [SerializeField] 
    private Scorecards scoreScript;
    [SerializeField] 
    private bool isPressed;

    private void Start()
    {
        scoreScript = this.GetComponent<Scorecards>();
        isPressed = false;
    }

    private void Update()
    {
        if (GameManager.instance.p1.ReadCloseTagButton() || GameManager.instance.p2.ReadCloseTagButton())
        {
            if (!isPressed)
            {
                Debug.Log("Close");
                scoreScript.NextSection();
                isPressed = true;
            }
        }

    }



}
