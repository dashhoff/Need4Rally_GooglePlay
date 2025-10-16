/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using Unity.Cinemachine;

public class GameCars : MonoBehaviour
{
    public static GameCars Instance;
    
    public Car[] Cars;
    public Car CurrentCar;
    
    [SerializeField] private CinemachineCamera[] _cameras;
    [SerializeField] private CinemachineCamera _face1Camera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Init()
    {
        if (GameSaves.CurrentCar >= 0 && GameSaves.CurrentCar < GameCars.Instance.Cars.Length)
        {
            CurrentCar = Cars[GameSaves.CurrentCar];
        }
        
        for (int i = 0; i < Cars.Length; i++)
        {
            Cars[i].gameObject.SetActive(GameSaves.CurrentCar == i);
        }
        
        for (int i = 0; i < _cameras.Length; i++)
        {
            _cameras[i].Follow = Cars[GameSaves.CurrentCar].transform;
            _cameras[i].LookAt = Cars[GameSaves.CurrentCar].transform;
        }
        
        _face1Camera.Follow = Cars[GameSaves.CurrentCar].Face1CameraTransform;
        _face1Camera.LookAt = Cars[GameSaves.CurrentCar].Face1CameraTransform;
    }
}
