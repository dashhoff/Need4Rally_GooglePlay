/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class Car : MonoBehaviour
{
    public bool Active = false;
    
    [SerializeField] private Wheel[] _wheels;
    [SerializeField] public Wheel[] DrivingWheels;
    [SerializeField] private Wheel[] _frontWheels;
    
    [SerializeField] private Rigidbody _rb;
    
    [SerializeField] private AnimationCurve _steeringCurve;
    [SerializeField] private AnimationCurve _antiSteeringCurve;
    [SerializeField] private float _turningSpeed = 0.5f;
    [SerializeField] private float _steeringOffset = 4;

    public Engine _engine;
    public Gearbox _gearbox;
    public CarUI _carUI;
    
    public PlayerInput PlayerInput;

    public Transform Face1CameraTransform;

    private void OnEnable()
    {
        EventBus.OnStartGame += Activate;
        
        EventBus.OnDeath += OnDefeat;
        EventBus.OnFinish += OnVictory;

        EventBus.OnStartGame += SetTurningSpeed;
        EventBus.OnStopPause += SetTurningSpeed;
    }

    private void OnDisable()
    {
        EventBus.OnStartGame -= Activate;
        
        EventBus.OnDeath -= OnDefeat;
        EventBus.OnFinish -= OnVictory;
        
        EventBus.OnStartGame -= SetTurningSpeed;
        EventBus.OnStopPause -= SetTurningSpeed;
    }

    private void FixedUpdate()
    {
        if (GameController.Instance.PausedGame || !Active) return;
        
        Acceleration(CalculateTorque());
    }

    private void Update()
    {
        if (!Active) return;
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        _carUI.UpdateRPMText(_engine.GetRPM());
        _carUI.UpdateRPMBar();
        _carUI.UpdateGearText(_gearbox.GetCurrentGear());
        _carUI.UpdateSpeedText(Mathf.Round(_rb.linearVelocity.magnitude * 5));
    }

    private void Start()
    {
        PlayerInput = PlayerInput.Instance;
        
        Init();
    }

    public void Init()
    {
        SetTurningSpeed();
        
        for (int i = 0; i < _wheels.Length; i++)
        {
            _wheels[i].SetCarRb(_rb);
            _wheels[i].SetDriveTire(false);
        }

        for (int i = 0; i < DrivingWheels.Length; i++)
        {
            DrivingWheels[i].SetDriveTire(true);
        }
        
        _gearbox.Init();
    }

    public void Activate()
    {
        Active = true;
    }
    
    public float GetCurrentSpeed()
    {
        return _rb.linearVelocity.magnitude;
    }

    public float CalculateTorque()
    {
        return _engine.GetTorque() / _gearbox.GetCurrentGearRatio() * _gearbox.GetCurrentGearRatioStrenght();
    }

    public void Acceleration(float torque)
    {
        if (_gearbox.GetCurrentGear() == 0 && _gearbox._automatic && PlayerInput.AccelerationInput == -1 && GetCurrentSpeed() < 5)
        {
            for (int i = 0; i < DrivingWheels.Length; i++)
            {
                if (DrivingWheels[i] != null)
                    DrivingWheels[i].Acceleration(torque);
            }
        }
        else
        {
            for (int i = 0; i < DrivingWheels.Length; i++)
            {
                if (DrivingWheels[i] != null)
                    DrivingWheels[i].Acceleration(torque);
            }
        }
    }
    
    public void SetAcceleration(float acceleration)
    {
        _engine.SetInput(acceleration);
    }
    
    public void Break(float breakPower)
    {
        for (int i = 0; i < _wheels.Length; i++)
        {
            _wheels[i].Break(breakPower);
        }
    }

    public void SetTurningSpeed()
    {
        _turningSpeed = SettingsSaves.TurningSpeed;
    }

    public void RotateFrontWheel(float angle)
    {
        float carSpeed = _rb.linearVelocity.magnitude;
        float steering = _steeringCurve.Evaluate(carSpeed);
        float antiSteering = _antiSteeringCurve.Evaluate(carSpeed);
        
        for (int i = 0; i < _frontWheels.Length; i++)
        {
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

            if (angle != 0)
            {
                _frontWheels[i].transform.localRotation = Quaternion.Lerp(
                    _frontWheels[i].transform.localRotation,
                    targetRotation,
                    Time.deltaTime * steering * _turningSpeed);
            }
            else
            {
                _frontWheels[i].transform.localRotation = Quaternion.Lerp(
                    _frontWheels[i].transform.localRotation,
                    targetRotation,
                    Time.deltaTime * antiSteering * _turningSpeed);
            }
        }
    }

    public void OnDefeat()
    {
        Active = false;
    }
    
    public void OnVictory()
    {
        Active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameController.Instance.GameOver) return;
        
        if (other.gameObject.CompareTag("Finish"))
            EventBus.InvokeFinish();
        
        if (other.gameObject.CompareTag("Death"))
            EventBus.InvokeDeath();
    }
}
