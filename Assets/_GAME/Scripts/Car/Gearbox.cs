/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class Gearbox : MonoBehaviour
{
    [SerializeField] private Car _car;

    [SerializeField] public bool _automatic { get; private set; }
    
    [field: SerializeField] public int _gearsNumber { get; private set; } = 5;
    [SerializeField] private int _currentGear;

    [SerializeField] private float _gearSwitchOffest = 0.5f;

    [field: SerializeField] public float[] _gearRations { get; private set; } =
    {
        -3f, //reversed
        3.5f, 
        2,1f, 
        1.3f, 
        1.0f, 
        0.75f
    }; //2105
    
    [SerializeField] private AnimationCurve[] _gearRationsStrenght;

    private void OnEnable()
    {
        EventBus.OnGearboxChanget += SetAutomatic;
    }

    private void OnDisable()
    {
        EventBus.OnGearboxChanget -= SetAutomatic;
    }

    private void FixedUpdate()
    {
        if (!_car.Active) return;
        
        if(_automatic) 
            AutomaticGear();
    }

    public void Init()
    {
        SetAutomatic(SettingsSaves.AutomaticGear);
    }
    
    public float GetCurrentGear()
    {
        return _currentGear;
    }
    
    public float GetCurrentGearRatio()
    {
        float ratio = _gearRations[_currentGear];
        
        Debug.Log("Ratio: " + ratio);
        
        return ratio;
    }
    
    public float GetCurrentGearRatioStrenght()
    {
        float normalizedRPM = Mathf.InverseLerp(0, _car._engine.GetMaxRPM(), _car._engine.GetRPM());
        float ratioStrenght = _gearRationsStrenght[_currentGear].Evaluate(normalizedRPM);
        
        //Debug.Log("RatioStrenght: " + ratioStrenght);
        
        return ratioStrenght;
    }

    public void UpGear()
    {
        if (_automatic) return;
        
        if (_currentGear != _gearsNumber - 1)
        {
            if (_currentGear != 0)
                _car._engine.SubRPM(Mathf.RoundToInt(_car._engine.GetRPM() * _gearSwitchOffest));
                
            _currentGear++;
            
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ShiftGearbox, transform.position);
        }
    }
    
    public void AutomaticUpGear()
    {
        if (_currentGear != _gearsNumber - 1)
        {
            if (_currentGear != 0)
                _car._engine.SubRPM(Mathf.RoundToInt(_car._engine.GetRPM() * _gearSwitchOffest));
                
            _currentGear++;
            
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ShiftGearbox, transform.position);
        }
    }

    public void DownGear()
    {
        if (_automatic) return;

        if (_currentGear != 0)
        {
            if (_currentGear != 1)
            {
                _car._engine.AddRPM(Mathf.RoundToInt(_car._engine.GetRPM() * _gearSwitchOffest));
            }

            _currentGear--;

            if (_currentGear != 0 && _car._engine.GetRPM() > 2000)
            {
                CarEventBus.InvokeExhaust();
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ExhaustShot, transform.position);
            }
            
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ShiftGearbox, transform.position);
        }
    }
    
    public void AutomaticDownGear()
    {
        if (_currentGear != 0)
        {
            if (_currentGear != 1)
            {
                _car._engine.AddRPM(Mathf.RoundToInt(_car._engine.GetRPM() * _gearSwitchOffest));
            }
            _currentGear--;

            if (_currentGear != 0 && _car._engine.GetRPM() > 1000)
            {
                CarEventBus.InvokeExhaust();
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ExhaustShot, transform.position);
            }

            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ShiftGearbox, transform.position);
        }
    }

    public void AutomaticGear()
    {
        float engineRPMNormalized = Mathf.InverseLerp(0, _car._engine.GetMaxRPM(), _car._engine.GetRPM());
        
        if (engineRPMNormalized > 0.8f && _currentGear != 0)
            AutomaticUpGear();
        else if (engineRPMNormalized < 0.35f && _currentGear != 1)
            AutomaticDownGear();

        if (_currentGear == 1 && _car.GetCurrentSpeed() < 5 && _car.PlayerInput.AccelerationInput == -1)
        {
            AutomaticDownGear();
        }
        
        if (_currentGear == 0 && _car.GetCurrentSpeed() < 5 && _car.PlayerInput.AccelerationInput == 1)
        {
            AutomaticUpGear();
        }
    }

    public void SetAutomatic(bool value)
    {
        _automatic = value;
    }
    
    public void SetAutomatic()
    {
        _automatic = SettingsSaves.AutomaticGear;
    }
}
