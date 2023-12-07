using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    public static SceneControl instance;
    [Header("Loading Screen")]
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider loadingSlider;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneToLoad)
    {
        loadingScreen.SetActive(true);
        //Run the A sync
        StartCoroutine(LoadSceneAsync(sceneToLoad));

    }

    IEnumerator LoadSceneAsync(string sceneToLoad)
    {

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneToLoad);
        yield return new WaitForSeconds(1);


    }
}
