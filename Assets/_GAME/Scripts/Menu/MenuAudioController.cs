/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using FMOD.Studio;
using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    private void OnEnable()
    {
        MenuCarManager.OnSelectCar += SelectCar;
    }

    private void OnDisable()
    {
        MenuCarManager.OnSelectCar -= SelectCar;
    }

    public void Init()
    {
        EventInstance musicInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Music);
        musicInstance.start();
    }

    public void SelectCar()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Woosh, Vector3.zero);
    }
}
