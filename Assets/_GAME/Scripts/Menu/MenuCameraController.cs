/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] _camers;

    public void SetActiveCamera(int id)
    {
        for (int i = 0; i < _camers.Length; i++)
        {
            _camers[i].SetActive(false);
        }

        _camers[id].SetActive(true);
    }
    
    public void SetActiveCamera(GameObject camera)
    {
        for (int i = 0; i < _camers.Length; i++)
        {
            _camers[i].SetActive(false);
        }
        
        camera.SetActive(true);
    }
}
