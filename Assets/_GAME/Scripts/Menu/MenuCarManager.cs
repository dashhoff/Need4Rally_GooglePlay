/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuCarManager : MonoBehaviour
{
    [SerializeField] private MenuCar[] _cars;
    [SerializeField] private int _currentCar;

    [SerializeField] private GameObject _playButton;
    [SerializeField] private Image _lockImage;
    
    [SerializeField] private TMP_Text _currentCarNameText;
    [SerializeField] private TMP_Text _currentCarInfoText;
    
    [SerializeField] private TMP_Text _currentCarPriceText;
    [SerializeField] private GameObject _buyCarButton;
    
    [SerializeField] private UIPanel _errorPanel;

    public void Init()
    {
        _currentCar = GameSaves.CurrentCar;
        
        SetActiveCar();
        SetTextInfo();
        TrySelectCar();
    }
    
    public void LeftArrow()
    {
        _currentCar--;
        
        if(_currentCar < 0)
            _currentCar = _cars.Length - 1;

        SetActiveCar();
        SetTextInfo();
        TrySelectCar();
    }
    
    public void RightArrow()
    {
        _currentCar++;
        
        if(_currentCar > _cars.Length - 1)
            _currentCar = 0;

        SetActiveCar();
        SetTextInfo();
        TrySelectCar();
    }

    public void SetActiveCar()
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            _cars[i].Model.SetActive(i == _currentCar);
        }
    }

    public void SetTextInfo()
    {
        _currentCarNameText.text = _cars[_currentCar].CarName;
        
        /*if (YandexGame.lang == "ru")
            _currentCarPriceText.text = "стоимость: " + _cars[_currentCar].Price;
        else
            _currentCarPriceText.text = "price: " + _cars[_currentCar].Price;
        
        if (YandexGame.lang == "ru")
            _currentCarInfoText.text = _cars[_currentCar].CarRuInfo;
        else
            _currentCarInfoText.text = _cars[_currentCar].CarEnInfo;*/
    }

    public void TrySelectCar()
    {
        if (GameSaves.OpenCars[_currentCar] == 1)
        {
            _lockImage.gameObject.SetActive(true);
            _playButton.SetActive(false);
            
            _currentCarPriceText.gameObject.SetActive(false);
            _buyCarButton.SetActive(false);
            
            GameSaves.CurrentCar = _currentCar;
        }
        else
        {
            _lockImage.gameObject.SetActive(false);
            _playButton.SetActive(true);
            
            _currentCarPriceText.gameObject.SetActive(true);
            _buyCarButton.SetActive(true);
        }
    }

    public void TryBuyCar()
    {
        if (_cars[_currentCar].Price <= GameSaves.Money)
            BuyCar();
        else
            _errorPanel.Open();
    }

    public void BuyCar()
    {
        GameSaves.SubMoney(_cars[_currentCar].Price);
        GameSaves.OpenCars[_currentCar] = 1;
        GameSaves.CurrentCar = _currentCar;
        
        GameSaves.MainSave();
        
        SetTextInfo();
        TrySelectCar();
    }
}
