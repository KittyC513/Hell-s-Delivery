using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLanguage : MonoBehaviour
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
        SwitchText();
    }

    private void SwitchText()
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
