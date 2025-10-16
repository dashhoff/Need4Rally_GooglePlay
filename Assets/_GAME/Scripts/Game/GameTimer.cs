/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _victoryTimeText;
    [SerializeField] private TMP_Text _newTimeRecordText;
    
    [SerializeField] private bool _running = false;
    
    [SerializeField] private float _bestTime;
    [SerializeField] private float _currentTime = 0;
    
    private void OnEnable()
    {
        EventBus.OnStartGame += StartGameTimer;

        EventBus.OnStartPause += StopTimer;
        EventBus.OnStopPause += StartTimer;
        
        EventBus.OnDeath += StopTimer;
        EventBus.OnFinish += CheckBestTime;
    }

    private void OnDisable()
    {
        EventBus.OnStartGame -= StartGameTimer;

        EventBus.OnStartPause -= StopTimer;
        EventBus.OnStopPause -= StartTimer;
        
        EventBus.OnDeath -= StopTimer;
        EventBus.OnFinish -= CheckBestTime;
    }

    private void Update()
    {
        if (!_running) return;
        
        _currentTime += Time.deltaTime;

        int minutes = (int)(_currentTime / 60);
        int seconds = (int)_currentTime % 60;
        int milliseconds = (int)((_currentTime - Mathf.Floor(_currentTime)) * 1000);

        _timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    public void StartGameTimer()
    {
        if (GameData.CurrentTrack == -1)
        {
            _timerText.gameObject.SetActive(false);
            _victoryTimeText.gameObject.SetActive(false);
            _newTimeRecordText.gameObject.SetActive(false);
            
            return;
        }
        
        _running = true;
    }
    
    public void StartTimer()
    {
        _running = true;
    }

    public void StopTimer()
    {
        _running = false;
    }
    
    public void CheckBestTime()
    {
        StopTimer();
        
        if (GameData.CurrentTrack == -1) return;
        
        int minutes = (int)(_currentTime / 60);
        int seconds = (int)_currentTime % 60;
        int milliseconds = (int)((_currentTime - Mathf.Floor(_currentTime)) * 1000);
        
        if (true) //TODO Сделать свой локализатор
            _victoryTimeText.text = "время: " + $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        else
            _victoryTimeText.text = "time: " + $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        
        if (_currentTime < GameSaves.TrackBestTimes[GameData.CurrentTrack] || GameSaves.TrackBestTimes[GameData.CurrentTrack] == 0)
        {
            _newTimeRecordText.gameObject.SetActive(true);
            GameSaves.TrackBestTimes[GameData.CurrentTrack] = _currentTime;
            GameSaves.MainSave();
            
            //long score = (long)(_currentTime* 1000f);
            
            Debug.Log("Track" + GameData.CurrentTrack);
            //YandexGame.NewLeaderboardScores("Track" + GameData.Instance.CurrentTrack, score);
        }
        else
        {
            _newTimeRecordText.gameObject.SetActive(false);
        }
    }
}
