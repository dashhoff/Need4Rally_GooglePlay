/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using TMPro;

public class MenuUIController : MonoBehaviour
{
    public static MenuUIController Instance;

    [SerializeField] private GameObject _freeDriveButton;
    
    [SerializeField] private TMP_Text _moneyText;
    
    [SerializeField] private TMP_Text[] _tracksBestTimeText;

    [SerializeField] private UIPanel _mainPanel;
    [SerializeField] private UIPanel _infoPanel;

    private void OnEnable()
    {
        GameSaves.OnMoneyChanget += UpdateMoneyText;
    }

    private void OnDisable()
    {
        GameSaves.OnMoneyChanget -= UpdateMoneyText;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Init()
    {
        UpdateMoneyText();

       /* if (YandexGame.savesData.isFirstSession)
        {
            _infoPanel.Open();
            _mainPanel.Close();
        }*/
        
        _freeDriveButton.SetActive(GameSaves.OpenTracks[GameSaves.OpenTracks.Length - 1] == 1);

        for (int i = 0; i < _tracksBestTimeText.Length; i++)
        {
            /*if (YandexGame.savesData.TrackBestTimes[i] != 0)
            {
                float time = YandexGame.savesData.TrackBestTimes[i];
                
                int minutes = (int)(time / 60);
                int seconds = (int)time % 60;
                int milliseconds = (int)((time - Mathf.Floor(time)) * 1000);
                
                _tracksBestTimeText[i].text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
            }*/
        }
    }

    public void UpdateMoneyText()
    {
        /*if (YandexGame.lang == "ru")
            _moneyText.text = "деньги: " + GameSaves.Instance.Money;
        else
            _moneyText.text = "money: " + GameSaves.Instance.Money;*/
    }
}
