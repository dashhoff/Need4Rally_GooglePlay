/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UITrack : MonoBehaviour
{
    public bool Locked;
    
    [SerializeField] private TMP_Text _infoText;
    [SerializeField] private string _ruInfo;
    [SerializeField] private string _enInfo;

    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _lockImage;

    public void Init()
    {
        SetLockInfo();
        SetTextInfo();
    }

    public void SetLockInfo()
    {
        if (Locked)
            _lockImage.gameObject.SetActive(true);
        else
            _lockImage.gameObject.SetActive(false);
    }
    
    public void SetTextInfo()
    {
        /*if (YandexGame.lang == "ru")
            _infoText.text = _ruInfo;
        else
            _infoText.text = _enInfo;*/
    }

    public void OnHoverEnter()
    {
        DOTween.Sequence()
            .Append(_iconImage.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f));
    }

    public void OnHoverExit()
    {
        DOTween.Sequence()
            .Append(_iconImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
    }
}
