using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public static ResetGame instance;
    public bool destroy;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        destroy = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            StartCoroutine(Reset());
        }

    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TitleScene");
        destroy = false;
    }

}
