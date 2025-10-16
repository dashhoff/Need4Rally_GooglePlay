/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private GameTrackController _gameTrackController;
    
    [SerializeField] private Settings _settings;
    
    [SerializeField] private GameTimer _gameTimer;
    
    [SerializeField] private GameAudioController _gameAudioController;
    
    [SerializeField] private GameUIController _gameUIController;
    
    [SerializeField] private GameCars _gameCars;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        //ScreenFader.Instance.FadeOut();
        
        _gameTrackController.Init();
        
        GameCars.Instance.Init();
        
        _settings.Init();
        
        _gameAudioController.Init();
        
        _gameUIController.Init();
        
        PlayerInput.Instance.Car = _gameCars.CurrentCar;
    }
}
