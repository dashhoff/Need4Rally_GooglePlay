/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] _cameras;
    [SerializeField] private int _currentIndex = 0;

    [SerializeField] private GameObject _defeatCamera;

    private void OnEnable()
    {
        EventBus.OnSwitchCamera += SwitchCamera;
        
        EventBus.OnDeath += OnDefeat;
    }

    private void OnDisable()
    {
        EventBus.OnSwitchCamera -= SwitchCamera;
        
        EventBus.OnDeath -= OnDefeat;
    }

    public void SwitchCamera()
    {
        if (GameController.Instance.PausedGame) return;
        
        _currentIndex++;
            
        if (_currentIndex > _cameras.Length - 1)
            _currentIndex = 0;
        
        for (int i = 0; i < _cameras.Length; i++)
        {
            _cameras[i].SetActive(i == _currentIndex);
        }
    }

    public void OnDefeat()
    {
        for (int i = 0; i < _cameras.Length; i++)
        {
            _cameras[i].SetActive(false);
        }
        
        _defeatCamera.SetActive(true);
    }
}
