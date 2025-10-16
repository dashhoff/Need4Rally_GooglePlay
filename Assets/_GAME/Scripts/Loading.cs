/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image _backImage;
    [SerializeField] private Animation _logoAnim;
    [SerializeField] private Animation _textAnim;
    [SerializeField] private Animation _downTextAnim;

    private AsyncOperation _loadingOperation;

    private void OnEnable()
    {
        Application.targetFrameRate = 120;
        
        _loadingOperation = SceneManager.LoadSceneAsync("Menu");
        _loadingOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        LoadingAnim();
    }

    public void LoadingAnim()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Logo, gameObject.transform.position);

        _downTextAnim.Play();
        _logoAnim.Play();
        _textAnim.Play();

        RectTransform backRectTransform = _backImage.GetComponent<RectTransform>();

        DOTween.Sequence()
            .Append(backRectTransform.DOAnchorPosX(backRectTransform.anchoredPosition.x + 1000, 25f))
            .SetEase(Ease.Linear);

        //ScreenFader.Instance.FadeIn(() => { loadingScene.allowSceneActivation = true; });

        //SceneManager.LoadScene("MenuAutumm");
        
        LoadMenu();
    }

    public void LoadMenu()
    {
        //ScreenFader.Instance.FadeIn(() => { _loadingOperation.allowSceneActivation = true; });
        _loadingOperation.allowSceneActivation = true;
    }
}
