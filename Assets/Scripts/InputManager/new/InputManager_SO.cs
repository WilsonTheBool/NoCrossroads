using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "GameObjects/Input Manager SO")]
public class InputManager_SO : ScriptableObject
{
    
    public List<InputHandler> inputHandlers;
    public List<InputHandler> defaultHandlers;

    private InputListener inputListener;

    public InputHandler curentHandler;

    public void AddInputHandler(InputHandler handler)
    {
        if (handler.isDefault)
        {
            defaultHandlers.Add(handler);
        }
        else
        {
            inputHandlers.Insert(0, handler);
            ChangeCurentHandler();
        }
        
        
    }

    public void RemoveInputHandler(InputHandler handler)
    {
        if (handler.isDefault)
        {
            defaultHandlers.Remove(handler);
        }
        else
        {
            inputHandlers.Remove(handler);
            ChangeCurentHandler();
        }

    }

    public void SetInputListener(InputListener newListener)
    {
        if(inputListener != null)
        inputListener.OnUpdate.RemoveListener(OnUpdate);
        inputListener = newListener;
        inputListener.OnUpdate.AddListener(OnUpdate);
    }
   
    private void ChangeCurentHandler()
    {
        if(inputHandlers.Count > 0)
        {
            curentHandler?.LooseFocus();
            curentHandler = inputHandlers[0];
            curentHandler.Focus();
        }
        else
        {
            curentHandler = null;
        }
    }

    private void OnUpdate(GameInputData inputData)
    {
        foreach(InputHandler handler in defaultHandlers)
        {
            handler.HandleInput(inputData);
        }

        curentHandler?.HandleInput(inputData);
    }

}

[System.Serializable]
public class GameInputData
{
    public Vector3Int tileMousePosition;
    public Vector3Int oldTileMousePosition;

    public Vector3 worldMousePosition;
    public Vector3 mouseMositionScreen;

    public bool AcceptTrigger;
    public bool CancelTrigger;

    public bool isOverUI;
}

[System.Serializable]
public class GameInputEvent: UnityEvent<GameInputData> { }
