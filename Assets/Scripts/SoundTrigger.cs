using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField]
    public AK.Wwise.Event[] sounds;
    private int order = 0;

    public void PlaySound()
    {
        sounds[order].Post(this.gameObject);
        order++;
    }
}
