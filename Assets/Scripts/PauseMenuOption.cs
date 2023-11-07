using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseMenuOption : MonoBehaviour
{
    [SerializeField]
    private Image background;
    public bool selected = false;

    [SerializeField]
    private Color selectedColour;
    [SerializeField]
    private Color unselectedColour;

    [SerializeField]
    private UnityEvent onActivate;

    private void Update()
    {
        //if we're hovered over change the background colour
        if (selected)
        {
            background.color = selectedColour;
        }
        else
        {
            background.color = unselectedColour;
        }
    }

    //if the player presses A on this option we do something
    public void OnSelect()
    {
        onActivate.Invoke();
    }
}
