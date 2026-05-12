using Kapuro2025Winter.Scripts.Stiring.Controller;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private InputDirection _inputDirection;
    
    public InputDirection GetCurrentInputDirection() => _inputDirection;

    void Update()
    {
        _inputDirection = TryInputDirectionKey();
    }

    private InputDirection TryInputDirectionKey()
    {
        //上のボタン　Y Joystick3
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton3)) return InputDirection.Up;
        //右のボタン　B Joystick1
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.JoystickButton1)) return InputDirection.Right;
        //下のボタン　A　Joystick0
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.JoystickButton0)) return InputDirection.Down;
        //右のボタン　X Joystick2
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.JoystickButton2)) return InputDirection.Left;
        return InputDirection.None;
    }
}
