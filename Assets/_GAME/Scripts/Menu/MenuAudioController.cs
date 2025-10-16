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
    public void Init()
    {
        EventInstance musicInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Music);
        musicInstance.start();
    }
}
