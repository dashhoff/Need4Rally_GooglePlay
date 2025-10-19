/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MenuCarManager : MonoBehaviour
{
    [SerializeField] private MenuCar[] _cars;
    [SerializeField] private int _currentCar;

    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _buyCarButton;
    [SerializeField] private TMP_Text _currentCarPriceText;
    [SerializeField] private Image _lockImage;
    
    [SerializeField] private TextAnimator _currentCarNameText;
    [SerializeField] private float _carNameTextAnimDuration = 0.5f;
    [SerializeField] private TMP_Text _currentCarInfoText;
    [SerializeField] private float _carInfoTextAnimDuration = 0.5f;
    
    [SerializeField] private UIPanel _errorPanel;
    
    [Header("Animation")]
    [SerializeField] private float _animSpeedToStartScale = 0.25f;
    [SerializeField] private float _animSpeedToZeroScale = 0.25f;
    
    [SerializeField] private Ease _easeToStartScale = Ease.InOutBack;
    [SerializeField] private Ease _easeToZeroScale = Ease.InOutBack;

    public static Action OnSelectCar;

    public void Init()
    {
        _currentCar = GameSaves.CurrentCar;
        
        CarAnimation(true, _cars[_currentCar]);
        SetTextInfo();
        TrySelectCar();
    }
    
    public void PreviousCar()
    {
        _currentCar--;
        
        if(_currentCar < 0)
            _currentCar = _cars.Length - 1;

        SetActiveCar();
        SetTextInfo();
        TrySelectCar();
    }
    
    public void NextCar()
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
        if (_currentCar - 1 >= 0)
            CarAnimation(false, _cars[_currentCar - 1]);
        else
            CarAnimation(false, _cars[_cars.Length - 1]);
        
        if (_currentCar + 1 <= _cars.Length - 1)
            CarAnimation(false, _cars[_currentCar + 1]);
        else
            CarAnimation(false, _cars[0]);
        
        CarAnimation(true, _cars[_currentCar]);

        OnSelectCar?.Invoke();
        
        Debug.Log("CurrentCar :" + _currentCar);
        
        /*for (int i = 0; i < _cars.Length; i++)
        {
            _cars[i].Model.SetActive(i == _currentCar);
        }*/
    }
    
    public void SetTextInfo()
    {
        _currentCarNameText.TextAnim(_cars[_currentCar].CarName, 0.5f);
        _currentCarInfoText.text = _cars[_currentCar].CarEnInfo;
        
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
            //_lockImage.gameObject.SetActive(true);
            _playButton.SetActive(false);
            
            _currentCarPriceText.gameObject.SetActive(false);
            _buyCarButton.SetActive(false);
            
            LockAnim(false);
            
            GameSaves.CurrentCar = _currentCar;
        }
        else
        {
            //_lockImage.gameObject.SetActive(false);
            _playButton.SetActive(true);
            
            _currentCarPriceText.gameObject.SetActive(true);
            _buyCarButton.SetActive(true);
            
            LockAnim(true);
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
    
    private void CarAnimation(bool active, MenuCar car)
    {
        Sequence animation = DOTween.Sequence();

        if (active)
        {
            car.Model.SetActive(true);
            animation.SetEase(_easeToStartScale)
                .SetUpdate(true)
                .Append(car.Model.transform.DOScale(car.StartScale, 0.5f));
        }
        else
            animation.SetEase(_easeToZeroScale)
                .SetUpdate(true)
                .Append(car.Model.transform.DOScale(new Vector3(0,0,0), 0.5f));
    }

    private void LockAnim(bool active)
    {
        Sequence animation = DOTween.Sequence();
        
        if (active)
            animation.SetUpdate(true).Append(_lockImage.DOFade(0.5f, 0.3f));
        else
            animation.SetUpdate(true).Append(_lockImage.DOFade(0, 0.3f));
    }
}
