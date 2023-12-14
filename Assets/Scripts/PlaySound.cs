using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private bool loop = false;
    [SerializeField] private AK.Wwise.Event startEvent;

    [SerializeField] private AK.Wwise.Event secondaryEvent;
    private bool isPlaying = false;
    uint playingId;
    uint playingIdSecondary;

    private void OnDestroy()
    {
        AkSoundEngine.StopPlayingID(playingId);
        AkSoundEngine.StopPlayingID(playingIdSecondary);
    }

    public void TriggerSound()
    {
        

        if (!isPlaying && loop)
        {
            playingId = startEvent.Post(this.gameObject);
            isPlaying = true;
        }
        else if (!loop)
        {
            playingId = startEvent.Post(this.gameObject);
        }

       
       
    }

    public void EndEvent()
    {
        if (loop)
        {
            isPlaying = false;
        }

        playingIdSecondary = secondaryEvent.Post(this.gameObject);
    }
}
