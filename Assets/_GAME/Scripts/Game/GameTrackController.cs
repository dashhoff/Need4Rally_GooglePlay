/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class GameTrackController : MonoBehaviour
{
    [SerializeField] private GameTrack[] _tracks;
    
    [SerializeField] private Transform _carSpawnPoint;

    public void Init()
    {
        int trackIndex = GameData.CurrentTrack;
        
        if (trackIndex == -1) return;
        
        for (int i = 0; i < _tracks.Length; i++)
        {
            _tracks[i].gameObject.SetActive(i == trackIndex);
        }
        
        _carSpawnPoint = _tracks[trackIndex].CarSpawnPoint;
        
        for (int i = 0; i < GameCars.Instance.Cars.Length; i++)
        {
            GameCars.Instance.Cars[i].transform.position = _carSpawnPoint.position;
            GameCars.Instance.Cars[i].transform.rotation = _carSpawnPoint.rotation;
        }
        
        //GameCars.Instance.CurrentCar.transform.position = _carSpawnPoint.position;
        //GameCars.Instance.CurrentCar.transform.rotation = _carSpawnPoint.rotation;
    }
}
