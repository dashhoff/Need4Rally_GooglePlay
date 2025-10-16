using System;
using UnityEngine;

public class FakeUILoading : MonoBehaviour
{
    [Range(0f, 1f)] public float loadingProgress;

    [SerializeField] private float _speed = 0.3f;
    [SerializeField] private float _slowdownPoint = 0.8f;
    [SerializeField] private float _finalDelay = 1.5f;

    private float _targetProgress;
    private bool _finished;

    private void Start()
    {
        loadingProgress = 0f;
        _targetProgress = 0f;
        _finished = false;

        LoadingManager.Instance.NextScene = "Menu";
        LoadingManager.Instance.ReadyToLoading = true;
        LoadingManager.Instance.StartLoading();
    }

    private void Update()
    {
        if (_finished) return;

        // Фейковая цель растет
        if (_targetProgress < 1f)
        {
            float add = _speed * Time.deltaTime;

            // замедление ближе к концу
            if (_targetProgress > _slowdownPoint)
                add *= 0.3f;

            _targetProgress = Mathf.Min(1f, _targetProgress + add);
        }

        // задержка перед полной загрузкой
        if (_targetProgress >= 1f)
        {
            _finalDelay -= Time.deltaTime;
            if (_finalDelay <= 0f)
            {
                loadingProgress = 1f;
                _finished = true;
                return;
            }
        }

        // Плавное сглаживание
        loadingProgress = Mathf.Lerp(loadingProgress, _targetProgress, Time.deltaTime * 5f);
    }
}