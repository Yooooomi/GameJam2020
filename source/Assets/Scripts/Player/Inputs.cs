
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public enum ControllerType
    {
        NONE,
        KEYBOARD,
        CONTROLLER,
    }

    public enum ButtonType
    {
        DOWN,
        UP,
        PRESSED,
    }

    static Dictionary<ControllerType, int> NumberOfControllers = new Dictionary<ControllerType, int>
    {
        { ControllerType.KEYBOARD, 0 },
        { ControllerType.CONTROLLER, 0 },
    };

    public ControllerType controllerType;

    // The index of the player in this controller mode
    public int controllerIndex = 0;

    private string getSuffix()
    {
        return "_" + (this.controllerType == ControllerType.KEYBOARD ? "K_" : "C_") + this.controllerIndex;
    }

    private void setControllerType(ControllerType type)
    {
        // Readjusting number of players per controller
        if (this.controllerType != ControllerType.NONE && this.controllerType != type)
        {
            this.controllerIndex = NumberOfControllers[type];

            NumberOfControllers[this.controllerType]--;
            NumberOfControllers[type]++;
        }
        this.controllerType = type;
    }

    public void Connect(ControllerType type, int controllerIndex)
    {
        this.setControllerType(type);
        this.controllerIndex = controllerIndex;
    }

    public float GetAxis(string name)
    {
        return Input.GetAxis(name + this.getSuffix());
    }

    public bool GetButton(string name, ButtonType pressType = ButtonType.DOWN, bool useSuffix = true)
    {
        string realName = name;

        if (useSuffix)
        {
            realName += this.getSuffix();
        }

        if (pressType == ButtonType.DOWN) return Input.GetButtonDown(realName);
        if (pressType == ButtonType.PRESSED) return Input.GetButton(realName);
        if (pressType == ButtonType.UP) return Input.GetButtonUp(realName);
        return Input.GetButton(realName);
    }

    public static bool GetButton(string name, ButtonType pressType = ButtonType.DOWN)
    {
        if (pressType == ButtonType.DOWN) return Input.GetButtonDown(name);
        if (pressType == ButtonType.PRESSED) return Input.GetButton(name);
        if (pressType == ButtonType.UP) return Input.GetButtonUp(name);
        return Input.GetButton(name);
    }
}
