/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System.Collections.Generic;
using UnityEngine;

public enum SurfaceType
{
    Asphalt, 
    Gravel, 
    Dirt, 
    Snow, 
    Ice, 
    Other
}

public class Wheel : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private Rigidbody _carRb;

    [Header("Suspension")]
    [SerializeField] private float _springLenght;
    [SerializeField] private float _springStrength;
    [SerializeField] private float _springDamping;
    
    [SerializeField] private float _tireModelRadius;

    [Header("Side slip")] 
    public bool IsDrifting = false;
    [SerializeField] private AnimationCurve _sideSlipCurve;
    [SerializeField] private float _sideSlipStrength = 1;
    [SerializeField] private float _sideSlipDetection;
    [SerializeField] private float _sideSlipOffset;
    [SerializeField] private float _finalSideSlipStrenght;

    [Header("Break")]
    [SerializeField] private float _breakPower;

    public SurfaceType CurrentSurface;
    private Dictionary<string, float> _surfaceFriction = new()
    {
        { "Asphalt", 0.9f },
        { "Gravel", 0.8f },
        { "Dirt", 0.7f },
        { "Snow", 0.3f },
        { "Ice", 0.1f },
        { "Other", 0.5f}
    };
    
    [Header("Other")] 
    public bool _isGrounded;
    
    [SerializeField] private GameObject _tireModel;
    
    [SerializeField] private float _tireMass;
    
    public bool _isDriveTire;

    [SerializeField] private float _tireXStartRotation;
    [SerializeField] private float _tireYStartRotation;
    [SerializeField] private float _tireZStartRotation;

    private void Start()
    {
       //_tireXStartRotation = _tireModel.transform.localRotation.x;
       //_tireYStartRotation = _tireModel.transform.localRotation.y;
       //_tireZStartRotation = _tireModel.transform.localRotation.z;
    }

    private void FixedUpdate()
    {
        if (GameController.Instance.PausedGame || !_car.Active) return;
        
        Suspension();
        SurfaceDetection();
        SlipDetection();
        //WheelIsDrifting();
        SideSlip();

        RotateWheel();

        //RollingResistance();
    }

    private void Suspension()
    {
        _isGrounded = false;
        
        Ray tireRay = new Ray(transform.position, -transform.up);
        float radius = 0.2f;
        
        if (Physics.SphereCast(tireRay, radius, out RaycastHit tireHit, _springLenght + radius))
        {
            _isGrounded = true;

            _tireModel.transform.position = tireHit.point + transform.up * _tireModelRadius;

            Vector3 springDirection = transform.up;

            Vector3 tireWorldVelocity = _carRb.GetPointVelocity(transform.position);

            //float offset = _springLenght - tireHit.distance;
            float compression = Mathf.Clamp(_springLenght - tireHit.distance, 0, _springLenght);

            float velocity = Vector3.Dot(springDirection, tireWorldVelocity);

            /* (offset > 0f)
            {
                float force = (offset * _springStrength) - (velocity * _springDamping);
                _carRb.AddForceAtPosition(springDirection * force, transform.position);
            }*/

            if (compression > 0f)
            {
                float force = (compression * _springStrength) - (velocity * _springDamping);
                _carRb.AddForceAtPosition(springDirection * force, transform.position);
            }
            
            Debug.DrawRay(transform.position, -transform.up * _springLenght, Color.red);
        }
    }

    private void SideSlip()
    {
        if (!_isGrounded) return;
        
        Vector3 steeringDirection = transform.right;
        
        Vector3 tireWorldVelocity = _carRb.GetPointVelocity(transform.position);
        
        float steeringVel = Vector3.Dot(steeringDirection, tireWorldVelocity);
        
        float carSpeed = Vector3.Dot(transform.forward, _carRb.linearVelocity);
        float sideSlip = _sideSlipCurve.Evaluate(0.5f);                             //TODO Исправить на скорость автомобиля (чем выше - тем больше)
        
        _finalSideSlipStrenght = _sideSlipStrength * _sideSlipDetection * sideSlip + _sideSlipOffset;
        if (_finalSideSlipStrenght < 0)
            _finalSideSlipStrenght = 0;
                    
        float directionVelChange = -steeringVel * _finalSideSlipStrenght;
        
        float desiredAccel =  directionVelChange / Time.fixedDeltaTime;
        
        _carRb.AddForceAtPosition(steeringDirection * _tireMass * desiredAccel, transform.position);
    }

    private void SlipDetection()
    { 
        if (!_isDriveTire)
        {
            _sideSlipDetection = 1;
            return;
        }
        
        float carSpeed = _carRb.linearVelocity.magnitude;
        float wheelAngilarVelosity = _car.CalculateTorque() / 5000;
        
        if (wheelAngilarVelosity < 0.01f)
            wheelAngilarVelosity = 0.01f;
        
        float slipRatio = Mathf.Clamp01(wheelAngilarVelosity / carSpeed);
        
        if (slipRatio > 0.3f)
        {
            IsDrifting = true;
            _sideSlipDetection = _sideSlipStrength * (1f - slipRatio);
        }
        else
        {
            IsDrifting = false;
            _sideSlipDetection = 1f;
        }
    }

    //TODO Переделать метод под определение в заносе ли колесо в соответствии с определением угла колеса и его вектора движения
    private void WheelIsDrifting()
    {
        Vector3 localVelocity = _carRb.transform.InverseTransformDirection(_carRb.linearVelocity);
        float sideSpeed = localVelocity.x;
        float forwardSpeed = localVelocity.z;

        float slipAngle = Mathf.Atan2(sideSpeed, Mathf.Abs(forwardSpeed)) * Mathf.Rad2Deg;

        if (Mathf.Abs(slipAngle) > 0.5f)
        {
            IsDrifting = true;
            _sideSlipDetection = _sideSlipStrength * Mathf.Clamp01(Mathf.Abs(slipAngle) / 30f);
        }
        else
        {
            IsDrifting = false;
            _sideSlipDetection = 1f;
        }
    }

    public void Acceleration(float torque)
    {
        if (!_isGrounded) return;
        
        Vector3 accelerationDirection = transform.forward;
        
        _carRb.AddForceAtPosition(accelerationDirection * torque * _sideSlipStrength, transform.position);
    }
    
    public void Break(float breakPower)
    {
        if (!_isGrounded) return;
        
        Vector3 localVelocity = _carRb.transform.InverseTransformDirection(_carRb.linearVelocity);
        Debug.Log("LocVel: " + localVelocity.magnitude);

        if (localVelocity.magnitude > 0.5f && _car._gearbox.GetCurrentGear() > 0)
        {
            Vector3 breakDirection = -transform.forward;
            _carRb.AddForceAtPosition(breakDirection * _breakPower * -breakPower, transform.position);
        }
        else if (localVelocity.magnitude > 0.5f && _car._gearbox.GetCurrentGear() == 0)
        {
            Vector3 breakDirection = transform.forward;
            _carRb.AddForceAtPosition(breakDirection * _breakPower * -breakPower, transform.position);
        }
    }

    public void RotateWheel()
    {
        /*float carSpeed = _carRb.linearVelocity.magnitude;
        
        float rotationsPerSecond = Mathf.PI * carSpeed * 25;
        
        if (_isDriveTire && _isGrounded)
            rotationsPerSecond = _car._engine.GetTorque() * Mathf.PI * carSpeed / 1000;
        
        if (_isDriveTire && !_isGrounded)
            rotationsPerSecond = _car._engine.GetTorque() * Mathf.PI / 2500; 

        float rotation = _tireModel.transform.localEulerAngles.x;
        rotation += Mathf.Rad2Deg * rotationsPerSecond * Time.fixedDeltaTime;

        _tireModel.transform.localRotation = Quaternion.Euler(
            rotation,
            _tireYStartRotation,
            _tireZStartRotation
        );*/
        
        float carSpeed = _carRb.linearVelocity.magnitude;

        float wheelCircumference = 2 * Mathf.PI;
        float rotationsPerSecond = carSpeed / wheelCircumference;

        float angle = rotationsPerSecond * 360f * Time.fixedDeltaTime * 4;

        _tireModel.transform.Rotate(Vector3.right, angle, Space.Self);
    }

    private void RollingResistance()
    {
        if (!_isGrounded) return;

        Vector3 velocity = _carRb.GetPointVelocity(transform.position);
    
        Vector3 resistanceForce = -velocity.normalized;

        _carRb.AddForceAtPosition(resistanceForce, transform.position);
    }

    public void SurfaceDetection()
    {
        Ray ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            switch (hit.transform.tag)
            {
                case "Asphalt":
                    CurrentSurface = SurfaceType.Asphalt;
                    _sideSlipStrength = 1 * _surfaceFriction["Asphalt"];
                    break;
                case "Gravel":
                    CurrentSurface = SurfaceType.Gravel;
                    _sideSlipStrength = 1 * _surfaceFriction["Gravel"];
                    break;
                case "Dirt":
                    CurrentSurface = SurfaceType.Dirt;
                    _sideSlipStrength = 1 * _surfaceFriction["Dirt"];
                    break;
                case "Snow":
                    CurrentSurface = SurfaceType.Snow;
                    _sideSlipStrength = 1 * _surfaceFriction["Snow"];
                    break;
                case "Ice":
                    CurrentSurface = SurfaceType.Ice;
                    _sideSlipStrength = 1 * _surfaceFriction["Ice"];
                    break;
                default:
                    CurrentSurface = SurfaceType.Other;
                    _sideSlipStrength = 1 * _surfaceFriction["Other"];
                    break;
            }
        }
    }
    
    public void SetCarRb(Rigidbody newRb) => _carRb = newRb;
    
    public void SetDriveTire(bool value) => _isDriveTire = value;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * _springLenght);
        
        Vector3 origin = transform.position;
        Vector3 direction = -transform.up;
        float radius = 0.2f;

        Gizmos.DrawWireSphere(origin + direction * _springLenght, radius);
        Gizmos.DrawLine(origin, origin + direction * _springLenght);
    }
}
