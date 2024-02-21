using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostMailTrail : MonoBehaviour
{
    private List<LostMail> lostMails;
    [SerializeField] private int goldeMailValue = 5;

    //its probably bad to run a loop every frame
    //
    private void Start()
    {
        lostMails = new List<LostMail>();
        foreach (var obj in transform.GetComponentsInChildren<LostMail>())
        {
            lostMails.Add(obj);
            obj.goldMailValue = goldeMailValue;
        }
    }

    private void Update()
    {
        CheckMail();
    }

    private void CheckMail()
    {

        for (int i = 0; i < lostMails.Count; i++)
        {
            if (lostMails[i].collected)
            {
                lostMails.Remove(lostMails[i]);
            }
        }

        if (lostMails.Count == 1)
        {
            lostMails[0].goldenMail = true;
        }
    }
}
