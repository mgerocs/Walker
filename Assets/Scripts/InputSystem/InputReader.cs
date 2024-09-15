// https://www.youtube.com/watch?v=ZHOWqF-b51k&ab_channel=Paridot

using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
{
    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);
        }
    }

    public void SetGameplay()
    {
        _gameInput.Gameplay.Enable();
        _gameInput.UI.Disable();
    }

    public void SetUI()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.UI.Enable();
    }

    public event Action<Vector2> MoveEvent;

    public event Action JumpEvent;
    public event Action JumpCanceledEvent;

    public event Action ToggleSprintEvent;

    public event Action<Vector2> RotateCameraEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"Phase: {context.phase}, Value: {context.ReadValue<Vector2>()}");
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            JumpCanceledEvent?.Invoke();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ToggleSprintEvent?.Invoke();
        }
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        RotateCameraEvent?.Invoke(context.ReadValue<Vector2>());
    }

    /*  public void OnPause(InputAction.CallbackContext context)
     {
         if (context.phase == InputActionPhase.Performed)
         {
             PauseEvent?.Invoke();
             SetUI();
         }
     }

     public void OnResume(InputAction.CallbackContext context)
     {
         if (context.phase == InputActionPhase.Performed)
         {
             ResumeEvent?.Invoke();
             SetGameplay();
         }
     } */


}
