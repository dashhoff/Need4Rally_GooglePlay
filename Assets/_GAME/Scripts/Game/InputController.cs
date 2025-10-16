/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if (GameController.Instance.GameOver) return;
        
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton2)) // X на геймпаде
            EventBus.InvokeSwitchCamera();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) // Start на геймпаде
            GameController.Instance.StartPause();
    }
}
