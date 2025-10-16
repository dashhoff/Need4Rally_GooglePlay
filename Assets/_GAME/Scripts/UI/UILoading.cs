using System;
using TMPro;
using UnityEngine;

public class UILoading : MonoBehaviour
{
    [SerializeField] private LoadingManager _loadingManager;
    [SerializeField] private FakeUILoading _fakeUILoading;
    [SerializeField] private Tachometer _tachometer;

    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private float _dotInterval = 0.5f; // интервал смены точек

    private float _dotTimer = 0f;
    private int _dotCount = 0; // 0..3

    private void Update()
    {
        // Получаем прогресс
        float loadingProgress = _fakeUILoading.loadingProgress;
        _tachometer.UpdateFill(loadingProgress, 1);

        // Анимация текста
        _dotTimer += Time.deltaTime;
        if (_dotTimer >= _dotInterval)
        {
            _dotTimer = 0f;
            _dotCount = (_dotCount + 1) % 4; // 0..3
            string dots = new string('.', _dotCount);
            _loadingText.text = "loading" + dots;
        }
    }
}