
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class InputData : ScriptableObject
{
    public Vector2 inputVector;        // raw movement input
    public Vector2 mouseDelta;         // mouse movements

    public delegate void InputEvent();
    public delegate void NumEvent(int num);

    public event InputEvent InteractPressed;
    public event InputEvent SprintPressed;
    public event InputEvent SprintReleased;
    public event InputEvent JumpPressed;
    public event InputEvent JumpReleased;
    public event InputEvent ItemDropped;
    public event InputEvent AttackPressed;

    public event NumEvent NumberPress;



    public void Interact(InputAction.CallbackContext context)
    {
        InteractPressed();
    }

    public void SprintPress(InputAction.CallbackContext context)
    {
        SprintPressed();
    }

    public void SprintRelease(InputAction.CallbackContext context)
    {
        SprintReleased();
    }

    public void JumpPress(InputAction.CallbackContext context)
    {
        JumpPressed();
    }

    public void JumpRelease(InputAction.CallbackContext context)
    {
        JumpReleased();
    }

    public void ToolbarPress(InputAction.CallbackContext context)
    {
        // pressed number extraction
        string keycode = context.control.path;
        char pressedChar = keycode[keycode.Length - 1];
        int pressedNumber = pressedChar - '0';

        NumberPress(pressedNumber);

    }

    public void DropItemPress(InputAction.CallbackContext context)
    {
        ItemDropped();
    }

    public void ShootPress(InputAction.CallbackContext context)
    {
        AttackPressed();
    }

}
