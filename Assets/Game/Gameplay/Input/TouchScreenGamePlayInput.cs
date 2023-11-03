using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchScreenGamePlayInput
{
    public event Action<Vector2> TouchPos;
    public event Action TouchCaneled;

    private readonly GamePlayInput input;

    public TouchScreenGamePlayInput(GamePlayInput input)
    {
        this.input = input;
        this.input.TouchScreen.TouchPos.performed += OnChangeMouserPosition;
        this.input.TouchScreen.TouchPress.canceled += OnTouchRelease;
        //this.input.KeyboardAndMouse.MouseClick.performed += OnLeftMouseClick;
    }

    private void OnTouchRelease(InputAction.CallbackContext context)
    {
        TouchCaneled?.Invoke();
    }

    private void OnChangeMouserPosition(InputAction.CallbackContext context)
    {
        TouchPos?.Invoke(context.ReadValue<Vector2>());
    }
}