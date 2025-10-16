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

public class Tachometer : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image[] _circles;
    [SerializeField] private Image[] _greenCircles;
    [SerializeField] private Image[] _yellowCircles;
    [SerializeField] private Image[] _redCircles;

    [Header("Colors")]
    [SerializeField] private Color _greenColor = Color.green;
    [SerializeField] private Color _yellowColor = Color.yellow;
    [SerializeField] private Color _redColor = Color.red;

    [Header("Thresholds")]
    [Range(0f, 1f)] [SerializeField] private float _greenThreshold = 0.30f;
    [Range(0f, 1f)] [SerializeField] private float _yellowThreshold = 0.60f;
    [Range(0f, 1f)] [SerializeField] private float _outerRedThreshold = 0.80f;
    [Range(0f, 1f)] [SerializeField] private float _centerRedThreshold = 0.90f;
    [Range(0f, 1f)] [SerializeField] private float _blinkThreshold = 0.95f;

    [Header("Time")]
    [SerializeField] private float _fillTimeToMin = 0.20f; // время заполения круга от максимума до минимума
    [SerializeField] private float _fillTimeToMax = 0.20f; // время заполения круга от минимума до максимума
    
    [Header("Blink Settings")]
    [SerializeField] private float _unfilledTime = 0.05f; // врермя когда круг пусотой во время мигания
    [SerializeField] private float _filledTime = 0.20f; // врермя когда круг заполнен во время мигания
    [SerializeField] private float _blinkMinAlpha = 0f; // минимальная прозрачность
    [SerializeField] private float _blinkMaxAlpha = 1f; // максимальная прозрачность

    [Header("Debug")]
    [SerializeField, Range(-1f, 1f)] private float _debugPercent = -1;

    private Sequence _blinkSequence;
    [SerializeField] private bool _isBlinking = false;
    [SerializeField] private float _lastProgress = 0;
    [SerializeField] private float _currentProgress = 0f;

    private void Start()
    {
        ClearAll();
    }

    private void Update()
    {
        if (_debugPercent != -1)
            UpdateFill(_debugPercent, 1);
    }

    public void UpdateFill(float progress, float maxProgress)
    {
        _currentProgress = Mathf.Clamp01(progress / maxProgress);
        
        if (_currentProgress > _blinkThreshold && !_isBlinking)
        {
            StartBlink();
            return;
        }

        if (_currentProgress < _blinkThreshold && _isBlinking)
            StopBlink();
        
        if (_currentProgress <= _greenThreshold)
        {
            float greenProgress = Mathf.InverseLerp(0f, _greenThreshold, _currentProgress);
            FillCircles(_greenCircles, greenProgress, _greenColor);
        }
        else if (_currentProgress <= _yellowThreshold)
        {
            FillCircles(_greenCircles, 1f, _greenColor);
            float yellowProgress = Mathf.InverseLerp(_greenThreshold, _yellowThreshold, _currentProgress);
            FillCircles(_yellowCircles, yellowProgress, _yellowColor);
        }
        else if (_currentProgress <= _outerRedThreshold)
        {
            FillCircles(_greenCircles, 1f, _greenColor);
            FillCircles(_yellowCircles, 1f, _yellowColor);
            float redProgress = Mathf.InverseLerp(_yellowThreshold, _outerRedThreshold, _currentProgress);
            FillCircles(_redCircles, redProgress, _redColor);
        }
        else if (_currentProgress >= _outerRedThreshold && _currentProgress < _centerRedThreshold)
        {
            FillCircles(_greenCircles, 1f, _greenColor);
            FillCircles(_yellowCircles, 1f, _yellowColor);
            FillCircles(_redCircles, 1, _redColor);
        }

        _lastProgress = _currentProgress;
    }
    
    private void FillCircles(Image[] circles, float progress, Color color)
    {
        int pairCount = circles.Length / 2; // количество пар слева/справа
        int activePairs = Mathf.FloorToInt(progress * pairCount); // сколько пар должно быть включено

        for (int i = 0; i < circles.Length; i++)
        {
            circles[i].color = color;
            circles[i].DOFade(_blinkMinAlpha, 0);
        }

        // Включаем пары по прогрессу
        for (int i = 0; i < activePairs; i++)
        {
            circles[i].DOFade(_blinkMaxAlpha, 0);
            circles[circles.Length - 1 - i].DOFade(_blinkMaxAlpha, 0);
        }
    }
    
    private void ClearAll()
    {
        for (int i  = 0; i < _circles.Length; i++)
        {
            _circles[i].DOFade(_blinkMinAlpha, _fillTimeToMin);
        }
    }

    private void StartBlink()
    {
        _isBlinking = true;
        
        Blink();
    }

    private void Blink()
    {
        foreach (var circle in _circles)
        {
            circle.DOKill();
            circle.color = _redColor;
        }
        
        _blinkSequence = DOTween.Sequence();

        foreach (var circle in _circles)
        {
            _blinkSequence.Join(
                DOTween.Sequence()
                    .Append(circle.DOFade(_blinkMaxAlpha, _fillTimeToMax))
                    .AppendInterval(_filledTime)
                    .Append(circle.DOFade(_blinkMinAlpha, _fillTimeToMin))
                    .AppendInterval(_unfilledTime)
                    .SetLoops(-1, LoopType.Restart)
            );
        }
    }

    private void StopBlink()
    {
        _isBlinking = false;
        
        if (_blinkSequence != null)
        {
            _blinkSequence.Kill();
            _blinkSequence = null;
        }
        
        foreach (var circle in _greenCircles)
            circle.color = _greenColor;
        
        foreach (var circle in _yellowCircles)
            circle.color = _yellowColor;
        
        _blinkSequence.Kill();
    }
}
