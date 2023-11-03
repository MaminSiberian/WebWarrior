using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAndKeyboardGamePlayInput 
{
    public event Action<Vector2> MousePosition;
    public event Action MouseClick;

    private readonly GamePlayInput input;
    
    public MouseAndKeyboardGamePlayInput (GamePlayInput input)
    {
        this.input = input;
        this.input.KeyboardAndMouse.MousePosition.performed += OnChangeMouserPosition;
        this.input.KeyboardAndMouse.MouseClick.performed += OnLeftMouseClick;
    }

    private void OnLeftMouseClick(InputAction.CallbackContext context)
    {
        MouseClick?.Invoke();
    }

    private void OnChangeMouserPosition(InputAction.CallbackContext context)
    {
        MousePosition?.Invoke(context.ReadValue<Vector2>());
    }
}
