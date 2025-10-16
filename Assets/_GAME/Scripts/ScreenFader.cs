/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using DG.Tweening;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;
    
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void FadeIn(System.Action callback = null)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        _canvasGroup.DOFade(1f, _fadeDuration).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }
    
    public void FadeOut(System.Action callback = null)
    {
        _canvasGroup.DOFade(0f, _fadeDuration).OnComplete(() =>
            {
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.interactable = false;
                callback?.Invoke();
            });
    }
}