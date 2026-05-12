using UnityEngine;

public static class ControllerKey
{
    public static bool GetKeyDownUp()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton3);
    }

    public static bool GetKeyDownDown()
    {
        return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.JoystickButton0);
    }

    public static bool GetKeyDownLeft()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.JoystickButton1);
    }

    public static bool GetKeyDownRight()
    {
        return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.JoystickButton2);
    }

    public static bool GetKeyUp()
    {
        return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.JoystickButton3);
    }

    public static bool GetKeyDown()
    {
        return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.JoystickButton0);
    }

    public static bool GetKeyLeft()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.JoystickButton1);
    }

    public static bool GetKeyRight()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.JoystickButton2);
    }
}
