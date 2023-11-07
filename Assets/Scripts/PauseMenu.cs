using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //need to trigger pause from probably the player script unless we have a different control asset
    private bool isPaused = false;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject menuOptionsParent;
    [SerializeField]
    private List<PauseMenuOption> menuOptions;

    private int selectNum = 1;

    private void Awake()
    {
        menuOptions = new List<PauseMenuOption>();
    }
    private void Start()
    {
        //need to get the background images for the objects
        //could just grab the child of the main object but theres both text and image objects
        //could also just grab the images themselves
        //could make a small script for menu options
        
        foreach (var obj in menuOptionsParent.GetComponentsInChildren<PauseMenuOption>())
        {
            menuOptions.Add(obj.GetComponent<PauseMenuOption>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        for (int i = 1; i < menuOptions.Count; i++)
        {
            if (selectNum == i)
            {
                menuOptions[i].selected = true;
            }
            else
            {
                menuOptions[i].selected = false;
            }
        }

      
    }


    private void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
