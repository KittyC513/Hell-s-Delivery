using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private bool loop = false;
    [SerializeField] private AK.Wwise.Event startEvent;

    [SerializeField] private AK.Wwise.Event secondaryEvent;
    private bool isPlaying = false;

    public void TriggerSound()
    {
        

        if (!isPlaying && loop)
        {
            startEvent.Post(this.gameObject);
            isPlaying = true;
        }
        else if (!loop)
        {
            startEvent.Post(this.gameObject);
        }
       
    }

    public void EndEvent()
    {
        if (loop)
        {
            isPlaying = false;
        }

        secondaryEvent.Post(this.gameObject);
    }
}
