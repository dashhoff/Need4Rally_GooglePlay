/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public enum EngineLayout
{
    Inline4,
    Inline5,
    Inline6,
    V4,
    V6,
    V8,
    Opposed,
    Rotary
}

public enum Boost
{
    None,
    Supercharger,
    Turbine
}

public class Engine : MonoBehaviour
{
    [SerializeField] private Car _car;
    
    [SerializeField] private Gearbox _gearbox;
    [SerializeField] private EngineLayout _engineLayout = EngineLayout.Inline4;
    [SerializeField] private Boost _boost = Boost.None;

    [SerializeField] private int _maxRPM;
    [SerializeField] private int _currentRPM;
    [SerializeField] private AnimationCurve _RPMAccelerationCurve;
    
    [SerializeField] private float _RPMDeAccelerationMultiplier;
    //[SerializeField] private int _RPMAcceleration;
    //[SerializeField] private int _RPMDeceleration;

    [SerializeField] private int _hp = 100;
    [SerializeField] private AnimationCurve _torqueCurve;
    [SerializeField] private float _torqueOffset;

    public EngineLayout GetEngineLayout()
    {
        return _engineLayout;
    }
    
    public int GetMaxRPM() => _maxRPM;
    public int GetRPM() => _currentRPM;

    public void AddRPM(int value)
    {
        _currentRPM += value;
        if (_currentRPM > _maxRPM)
            _currentRPM = _maxRPM;
    }
    
    public void SubRPM(int value)
    {
        _currentRPM -= value;
        if (_currentRPM < 0)
            _currentRPM = 0;
    }
    
    public float GetTorque()
    {
        float normalizedRPM = Mathf.InverseLerp(0, _maxRPM, _currentRPM);
        float torque = _torqueCurve.Evaluate(normalizedRPM) * _torqueOffset * _hp * _gearbox.GetCurrentGearRatioStrenght();
        
        //Debug.Log("Torque: " + torque);
        
        return torque;
    }

    public void Acceleration(float acceleration)
    {
        float normalizedRPM = Mathf.InverseLerp(0, _maxRPM, _currentRPM);

        if (_gearbox.GetCurrentGearRatio() < 0)
            _currentRPM += Mathf.RoundToInt(_RPMAccelerationCurve.Evaluate(normalizedRPM) * -1 * _gearbox.GetCurrentGearRatio());
        else
            _currentRPM += Mathf.RoundToInt(_RPMAccelerationCurve.Evaluate(normalizedRPM) * _gearbox.GetCurrentGearRatio());

        if (_currentRPM >= _maxRPM)
        {
            _currentRPM = _maxRPM;
            return;
        }

        if (_currentRPM < 0)
        {
            _currentRPM = 0;
            return;
        }
    }
    
    public void SetInput(float acceleration)
    {
        float normalizedRPM = Mathf.InverseLerp(0, _maxRPM, _currentRPM);

        if (!_gearbox._automatic)
        {
            if (acceleration > 0 ) 
                Acceleration(acceleration);
            else
            {
                if (_gearbox.GetCurrentGear() != 0)
                    _currentRPM = Mathf.RoundToInt(Mathf.Max(0, _currentRPM - _RPMAccelerationCurve.Evaluate(normalizedRPM) * _gearbox.GetCurrentGearRatio() * _RPMDeAccelerationMultiplier));
                else
                    _currentRPM = Mathf.RoundToInt(Mathf.Max(0, _currentRPM + _RPMAccelerationCurve.Evaluate(normalizedRPM) * _gearbox.GetCurrentGearRatio() * _RPMDeAccelerationMultiplier));
            }

            if (acceleration < 0)
            {
                _car.Break(acceleration);
            }
        }
        else
        {
            if (_gearbox.GetCurrentGear() > 0)
            {
                if (acceleration > 0 ) 
                    Acceleration(acceleration);
                else
                    _currentRPM = Mathf.RoundToInt(Mathf.Max(0, _currentRPM - _RPMAccelerationCurve.Evaluate(normalizedRPM) * _gearbox.GetCurrentGearRatio() * _RPMDeAccelerationMultiplier));

                if (acceleration < 0)
                {
                    _car.Break(acceleration);
                }
            }
            else
            {
                if (acceleration > 0 ) 
                    _car.Break(-acceleration);
                else if (acceleration < 0)
                    Acceleration(acceleration);
                else
                {
                    _currentRPM = Mathf.RoundToInt(Mathf.Max(0, _currentRPM + _RPMAccelerationCurve.Evaluate(normalizedRPM) * _gearbox.GetCurrentGearRatio() * _RPMDeAccelerationMultiplier));
                }
            }
        }
    }
}
