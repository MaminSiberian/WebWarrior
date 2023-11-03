using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HookControl;

public class GamePlayInputManager : MonoBehaviour
{
    public static event Action<Vector2> MousePosition;
    public static event Action MouseClick;
    //InputMAP from /Asets/Game/Gameplay/Input
    private GamePlayInput input;
    //script from /Asets/Game/Gameplay/Input
    private MouseAndKeyboardGamePlayInput MKInput;
    private TouchScreenGamePlayInput TSInput;

    private void Awake()
    {
        input = new GamePlayInput();
        input.Enable();
        InitMouseAndKeyboardInput(input);
        InitTouchScreeenInput(input);
    }

    private void InitTouchScreeenInput(GamePlayInput input)
    {
        TSInput = new TouchScreenGamePlayInput(input);
        TSInput.TouchPos += OnChangePosition;
        TSInput.TouchCaneled += OnClick;
    }

    private void InitMouseAndKeyboardInput(GamePlayInput input)
    {
        MKInput = new MouseAndKeyboardGamePlayInput(input);
        MKInput.MousePosition += OnChangePosition;
        MKInput.MouseClick += OnClick;
    }

    private void OnClick()
    {
        MouseClick?.Invoke();
    }

    private void OnChangePosition(Vector2 mousePos)
    {
        MousePosition?.Invoke(mousePos);
    }

    private void OnDisable()
    {
        RemoveLisner();
    }

    private void RemoveLisner()
    {
        MKInput.MousePosition -= OnChangePosition;
        MKInput.MouseClick -= OnClick;
        TSInput.TouchPos -= OnChangePosition;
        TSInput.TouchCaneled -= OnClick;
    }

    private void OnDestroy()
    {
        RemoveLisner();
    }
}
