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
    private GameObject quitOptionsParent;
    [SerializeField]
    private GameObject quitMenu;
    [SerializeField]
    private List<PauseMenuOption> menuOptions;
    private PauseMenuOption selectedOption;
    
    [SerializeField]
    private Test inputActions;
    private InputAction pauseGame, pauseJoystick, selectOption;

    private Vector2 joystickValue;
    private bool canToggle = false;
    private bool canMove = true;
    private bool canPress = true;
    private bool quitActive = false;

    [SerializeField] private int selectNum = 1;
   

    private void Awake()
    {
        menuOptions = new List<PauseMenuOption>();
        inputActions = new Test();
    }

    private void OnEnable()
    {
        
        //pauseGame = inputActions.PauseControls.PauseUnpause;
        //pauseGame.Enable();
        pauseJoystick = inputActions.PauseControls.MenuJoystick;
        pauseJoystick.Enable();
        selectOption = inputActions.PauseControls.SelectOption;
        selectOption.Enable();
    }

    private void OnDisable()
    {
        pauseGame.Disable();
        pauseJoystick.Disable();
        selectOption.Disable();
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
        joystickValue = pauseJoystick.ReadValue<Vector2>();

        if (pauseGame.IsPressed() && canToggle)
        {
            print("Pause");
            if (isPaused)
            {
                Resume();
                canToggle = false;
            }
            else 
            {
                Pause();
                canToggle = false;
            }
        }

        if (!pauseGame.IsPressed())
        {
            print("!Pause");
            canToggle = true;
        }

        if (isPaused && canMove)
        {
            if (joystickValue.y > 0 && selectNum > 0)
            {
                selectNum -= 1;
                canMove = false;
            }
            else if (joystickValue.y < 0 && selectNum < menuOptions.Count - 1)
            {
                selectNum += 1;
                canMove = false;
            }
            
        }

        if (joystickValue.y == 0)
        {
            canMove = true;
        }

            

        for (int i = 0; i < menuOptions.Count; i++)
            {
                if (selectNum == i)
                {
                    menuOptions[i].selected = true;
                    selectedOption = menuOptions[i];
                }
                else
                {
                    menuOptions[i].selected = false;
                }
            }

        if (selectOption.IsPressed() && canPress)
        {
            selectedOption.OnSelect();
            canPress = false;
            if (selectNum == 0)
            {
                Resume();
            }
        }

        if (!selectOption.IsPressed())
        {
            canPress = true;
        }

        selectNum = Mathf.Clamp(selectNum, 0, menuOptions.Count - 1);
    }


    public void Pause()
    {
        quitMenu.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused = true;
        GameManager.instance.p1.isFreeze = true;
        GameManager.instance.p2.isFreeze = true;
        Time.timeScale = 0;

        menuOptions.Clear();
        foreach (var obj in menuOptionsParent.GetComponentsInChildren<PauseMenuOption>())
        {
            menuOptions.Add(obj.GetComponent<PauseMenuOption>());
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        Time.timeScale = 1;
    }

    public void QuitMenu()
    {
        pauseMenu.SetActive(false);
        quitMenu.SetActive(true);

        menuOptions.Clear();
        foreach (var obj in quitOptionsParent.GetComponentsInChildren<PauseMenuOption>())
        {
            menuOptions.Add(obj.GetComponent<PauseMenuOption>());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Loader.Load(Loader.Scene.TitleScene);
    }
}
