/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private UIPanel _mainPanel;
    [SerializeField] private UIPanel _pausePanel;
    [SerializeField] private UIPanel _victoryPanel;
    [SerializeField] private UIPanel _defeatPanel;

    [SerializeField] private TMP_Text _trackBestTimeText;
    //[SerializeField] private GameObject[] _leaderboards;

    public void Init()
    {
        if (GameData.CurrentTrack == -1) return;
        
        //float time = YandexGame.savesData.TrackBestTimes[GameData.Instance.CurrentTrack];

        /*if (time != 0)
        {
            int minutes = (int)(time / 60);
            int seconds = (int)time % 60;
            int milliseconds = (int)((time - Mathf.Floor(time)) * 1000);
                
            if (YandexGame.lang == "ru") 
                _trackBestTimeText.text = "лучшее время: " + $"{minutes:00}:{seconds:00}:{milliseconds:000}";
            else
                _trackBestTimeText.text = "best time: " + $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }*/
        
        /*for (int i = 0; i < _leaderboards.Length; i++)
        {
            _leaderboards[i].SetActive(GameData.Instance.CurrentTrack == i);
        }*/
    }
    
    private void OnEnable()
    {
        EventBus.OnStartPause += OnStartPause;
        EventBus.OnStopPause += OnStopPause;

        EventBus.OnDeath += OnDefeat;
        EventBus.OnFinish += OnVictory;
    }

    private void OnDisable()
    {
        EventBus.OnStartPause -= OnStartPause;
        EventBus.OnStopPause -= OnStopPause;

        EventBus.OnDeath -= OnDefeat;
        EventBus.OnFinish -= OnVictory;
    }

    public void OnStartPause()
    {
        _pausePanel.Open();
        
        _mainPanel.Close();
    }
    
    public void OnStopPause()
    {
        _mainPanel.Open();
        
        _pausePanel.Close();
    }
    
    public void OnDefeat()
    {
        _defeatPanel.Open();
        
        _mainPanel.Close();
    }
    
    public void OnVictory()
    {
        _victoryPanel.Open();
        
        _mainPanel.Close();
    }
}
