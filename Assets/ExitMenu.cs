using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject enText;
    [SerializeField]
    private GameObject chText;
    [SerializeField]
    private GameObject enText1;
    [SerializeField]
    private GameObject chText1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void switchToCh()
    {
        enText.SetActive(false);
        enText1.SetActive(false);
        chText.SetActive(true);
        chText1.SetActive(true);
        GameManager.instance.isCh = true;
    }

    public void switchToEn()
    {
        enText.SetActive(true);
        enText1.SetActive(true);
        chText.SetActive(false);
        chText1.SetActive(false);
        GameManager.instance.isCh = false;
    }
}
