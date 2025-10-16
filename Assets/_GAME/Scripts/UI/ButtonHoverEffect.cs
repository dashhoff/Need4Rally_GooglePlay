using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour
{
    [SerializeField] private float _durationToEnd = 0.2f;
    [SerializeField] private float _durationToStart = 0.2f;

    [Header("SCALE")]
    [SerializeField] private bool _scale;
    private Vector3 _startScale;
    [SerializeField] private Vector3 _endScale = new Vector3(1.1f, 1.1f, 1.1f);

    [Header("SHINE")]
    [SerializeField] private bool _shine;
    [SerializeField] private Image _shineImage;
    [SerializeField] private float _shineAlpha;

    [Header("BACKGROUND")]
    [SerializeField] private bool _background;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Color _startBackgroundColor;
    [SerializeField] private Color _endBackgroundColor;

    [Header("TEXT")]
    [SerializeField] private bool _text;
    [SerializeField] private TMP_Text _TMP;
    [SerializeField] private Color _startTextColor;
    [SerializeField] private Color _endTextColor;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) => PointerEnter();

    public void OnPointerExit(PointerEventData eventData) => PointerExit();

    public void PointerEnter()
    {
        if (_scale)
        {
            transform.DOScale(_endScale, _durationToEnd)
                .SetEase(Ease.InOutBack)
                .SetUpdate(true);
        }

        if (_background)
        {
            _backgroundImage.DOColor(_endBackgroundColor, _durationToEnd)
                .SetUpdate(true);
        }

        if (_shine)
        {
            _shineImage.DOFade(_shineAlpha, _durationToEnd)
                .SetUpdate(true);
        }

        if (_text)
        {
            _TMP.DOColor(_endTextColor, _durationToEnd)
                .SetUpdate(true);
        }
    }

    public void PointerExit()
    {
        if (_scale)
        {
            transform.DOScale(_startScale, _durationToStart)
                .SetEase(Ease.InOutBack)
                .SetUpdate(true);
        }

        if (_background)
        {
            _backgroundImage.DOColor(_startBackgroundColor, _durationToEnd)
                .SetUpdate(true);
        }

        if (_text)
        {
            _TMP.DOColor(_startTextColor, _durationToEnd)
                .SetUpdate(true);
        }

        if (_shine)
        {
            _shineImage.DOFade(0, _durationToStart)
                .SetUpdate(true);
        }
    }

    public void SoundPointerEnter()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ButtonHover, gameObject.transform.position);
    }

    public void ClickSound()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ButtonClick, gameObject.transform.position);
    }
}
