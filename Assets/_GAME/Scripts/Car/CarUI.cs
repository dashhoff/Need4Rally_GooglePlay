/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    [SerializeField] private Car _car;
    
    [SerializeField] private TMP_Text _currentGearText;
    [SerializeField] private TMP_Text _currentRpmText;
    [SerializeField] private TMP_Text _currentSpeedText;

    [SerializeField] private Image _RPMBar;

    public void UpdateGearText(float newGear)
    {
        if(newGear == 0)
            _currentGearText.text = "R";
        else
            _currentGearText.text = newGear.ToString();
    }
    
    public void UpdateRPMText(float newRPM)
    {
        _currentRpmText.text = newRPM.ToString();
    }
    
    public void UpdateSpeedText(float newSpeed)
    {
        _currentSpeedText.text = newSpeed.ToString();
    }

    public void UpdateRPMBar()
    {
        float value = Mathf.InverseLerp(0, _car._engine.GetMaxRPM(), _car._engine.GetRPM());
        _RPMBar.fillAmount = value;
    }
}
