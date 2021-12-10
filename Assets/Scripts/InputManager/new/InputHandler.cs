using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InputHandler : MonoBehaviour
{
    public int priority = 0;

    public InputManager_SO InputManager_SO;

    public GameInputEvent OnHandleInputData;
    public GameInputEvent OnAcceptInput;
    public GameInputEvent OnCancelInput;

    public UnityEvent OnLostFocus;
    public UnityEvent OnFocus;

    public bool isDefault;
    public void HandleInput(GameInputData inputData)
    {
        OnHandleInputData.Invoke(inputData);

        if (inputData.AcceptTrigger) OnAcceptInput?.Invoke(inputData);
        if (inputData.CancelTrigger) OnCancelInput?.Invoke(inputData);
    }

    public void LooseFocus()
    {
        OnLostFocus?.Invoke();
    }

    public void Focus()
    {
        OnFocus?.Invoke();
    }

    private void OnEnable()
    {
        InputManager_SO.AddInputHandler(this);
    }

    private void OnDisable()
    {
        InputManager_SO.RemoveInputHandler(this);
    }
}

