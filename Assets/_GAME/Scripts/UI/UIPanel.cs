using UnityEngine;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float _openDuration = 0.2f;
    [SerializeField] private float _closeDuration = 0.2f;

    public void Open()
    {
        _canvasGroup.blocksRaycasts = true;
        
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(1, _openDuration))
            .SetUpdate(true);
    }

    public void Close()
    {
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(0, _closeDuration))
            .SetUpdate(true)
            .OnComplete(()=>{ _canvasGroup.blocksRaycasts = false; });
    }

    public void ChangeState()
    {
        if (_canvasGroup.alpha == 0)
            Open();
        else
            Close();
    }
}
