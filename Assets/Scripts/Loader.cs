using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Loader 
{
    private class LoadingMonoBehaviour : MonoBehaviour
    {
        [SerializeField]
        Slider loadingSlider;
    }
    public enum Scene
    {
        TitleScene,
        HubStart,
        PrototypeLevel,
        HubEnd,
        MVPLevel,
        Tutorial,
        LoadingScene,
        ScoreCards,
        Level1

    }

    private static Action onLoaderCallback;
    public static AsyncOperation asyncOperation;
    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));

        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());


    }

    public static void LoaderCallback()
    {
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        //yield return new WaitForSeconds(3);

        asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (asyncOperation.isDone)
        {
            yield return null;
        }


    }

    public static float GetLoadingProgress()
    {
        if(asyncOperation != null)
        {
            return asyncOperation.progress;
        }
        else
        {
            return 1f;
        }

    }

}
