/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;
    
    public string NextScene = "Game";
    public bool ReadyToLoading = true;
    [SerializeField] public float loadingProgress { get; private set; }

    [SerializeField] private float _loadingDelay = 1;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartLoading()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        loadingProgress = 0;
        
        AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(NextScene);
        asyncLoading.allowSceneActivation = false;

        while (!asyncLoading.isDone)
        {
            loadingProgress = Mathf.Clamp01(asyncLoading.progress / 0.9f);
            
            Debug.Log("Прогресс загрузки сцены: " + loadingProgress);

            while (!ReadyToLoading)
            {
                yield return null;
            }
            
            if (loadingProgress >= 0.9f)
            {
                yield return new WaitForSecondsRealtime(_loadingDelay);
                ScreenFader.Instance.FadeIn(callback: () =>
                {
                    asyncLoading.allowSceneActivation = true; 
                });
            }

            yield return null;
        }
    }
}