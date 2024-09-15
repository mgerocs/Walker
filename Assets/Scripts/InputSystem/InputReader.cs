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

    #region Gameplay events
    public event Action<Vector2> MoveEvent;

    public event Action JumpEvent;
    public event Action JumpCanceledEvent;

    public event Action ToggleSprintEvent;

    public event Action<Vector2> RotateCameraEvent;

    public event Action InteractEvent;
    public event Action InteractCanceledEvent;

    public event Action OpenMenuEvent;

    public event Action HighlightInteractablesEvent;
    public event Action HighlightInteractablesCanceledEvent;
    #endregion

    #region UI events
    public event Action CloseMenuEvent;
    #endregion

    #region Gameplay event handlers
    public void OnMove(InputAction.CallbackContext context)
    {
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            InteractCanceledEvent?.Invoke();
        }
    }
    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        OpenMenuEvent?.Invoke();
    }

    public void OnHighlightInteractables(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            HighlightInteractablesEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            HighlightInteractablesCanceledEvent?.Invoke();
        }
    }
    #endregion

    #region UI event handlers
    public void OnCloseMenu(InputAction.CallbackContext context)
    {
        CloseMenuEvent?.Invoke();
    }
    #endregion
}
