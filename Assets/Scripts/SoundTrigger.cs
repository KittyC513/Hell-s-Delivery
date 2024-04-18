using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public int currentBankNum = 0;
    [SerializeField] private SoundCollection[] soundCollections;

    public void SetBankNum(int num)
    {
        currentBankNum = num;
    }

    public void PlaySound()
    {
        SoundCollection bank = soundCollections[currentBankNum];
        bank.sounds[bank.order].Post(this.gameObject);
        bank.order++;
    }
}

[System.Serializable]
public class SoundCollection
{
    [SerializeField] public string bankName;
    public AK.Wwise.Event[] sounds;
    public int order = 0;
}
