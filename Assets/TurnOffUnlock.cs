using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffUnlock : MonoBehaviour
{
    [SerializeField]
    private GameObject EnText;
    [SerializeField]
    private GameObject ChText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TurnOffCondition();
    }

    void TurnOffCondition()
    {

        if (GameManager.instance.LalahRequestWasCompleted)
        {
            EnText.SetActive(false);
            ChText.SetActive(false);
        }
        else
        {
            if (GameManager.instance.isCh)
            {
                ChText.SetActive(true);
                EnText.SetActive(false);
            }
            else
            {
                EnText.SetActive(true);
                ChText.SetActive(false);
            }
        }
    }
}
