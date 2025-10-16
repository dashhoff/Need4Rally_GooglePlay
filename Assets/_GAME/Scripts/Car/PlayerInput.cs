/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    
    public Car Car;
    
    public float AccelerationInput;
    public float TurnInput;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (GameController.Instance.PausedGame || !Car.Active) return;
        
        // Ввод с клавиатуры
        float keyboardVertical = 0;
        if (Input.GetKey(KeyCode.W)) keyboardVertical = 1;
        else if (Input.GetKey(KeyCode.S)) keyboardVertical = -1;

        float keyboardHorizontal = 0;
        if (Input.GetKey(KeyCode.D)) keyboardHorizontal = 1;
        else if (Input.GetKey(KeyCode.A)) keyboardHorizontal = -1;

        // Ввод с геймпада (по умолчанию оси называются "Vertical" и "Horizontal")
        float gamepadVertical = Input.GetAxis("Vertical");
        float gamepadHorizontal = Input.GetAxis("Horizontal");

        // Приоритет геймпада, если ось активна
        AccelerationInput = Mathf.Abs(gamepadVertical) > 0.1f ? gamepadVertical : keyboardVertical;
        TurnInput = Mathf.Abs(gamepadHorizontal) > 0.1f ? gamepadHorizontal * 60f : keyboardHorizontal * 60f;

        Car.SetAcceleration(AccelerationInput);
        Car.RotateFrontWheel(TurnInput);
    }

    public void OnGas()
    {
        AccelerationInput = 1;
    }
    
    public void OffGas()
    {
        AccelerationInput = 0;
    }
    
    public void OnBreak()
    {
        AccelerationInput = -1;
    }
    
    public void OffBreak()
    {
        AccelerationInput = 0;
    }
    
    public void OnLeft()
    {
        TurnInput = -60;
    }
    
    public void OffLeft()
    {
        TurnInput = 0;
    }
    
    public void OnRight()
    {
        TurnInput = 60;
    }
    
    public void OffRight()
    { 
        TurnInput = 0;
    }

    public void UpGear()
    {
        Car._gearbox.UpGear();
    }

    public void DownGear()
    {
        Car._gearbox.DownGear();
    }
}
