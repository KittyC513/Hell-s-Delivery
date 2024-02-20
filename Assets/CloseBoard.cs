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
    private bool isUnfreezed;
    [SerializeField]
    private bool start;

    [SerializeField] private bool debug = true;


    private void Start()
    {
        scoreScript = this.GetComponent<Scorecards>();
        isUnfreezed = false;

        if (!start)
        {
            StartCoroutine(UnfreezeButton());
            start = true;
        }
    }

    private void Update()
    {
        if (isUnfreezed && !debug)
        {
            if (GameManager.instance.p1.ReadCloseTagButton() || GameManager.instance.p2.ReadCloseTagButton())
            {
                Debug.Log("Close");
                scoreScript.NextSection();
            }
        }

        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                scoreScript.NextSection();
            }
        }
       
    }

    IEnumerator UnfreezeButton()
    {
        yield return new WaitForSeconds(2f);
        isUnfreezed = true;
    }



}
