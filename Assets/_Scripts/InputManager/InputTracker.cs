// https://www.youtube.com/watch?v=ZHOWqF-b51k&ab_channel=Paridot

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputTracker", menuName = "Scriptable Objects/InputTracker")]
public class InputTracker : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
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

    private void OnDisable()
    {
        if (_gameInput != null)
        {
            _gameInput.Gameplay.Disable();
            _gameInput.UI.Disable();
        }
    }

    public void SetGameplay()
    {
        Debug.Log("SET GAMEPLAY");
        _gameInput.Gameplay.Enable();
        _gameInput.UI.Disable();
    }

    public void SetUI()
    {
        Debug.Log("SET UI");
        _gameInput.Gameplay.Disable();
        _gameInput.UI.Enable();
    }

    #region Gameplay event handlers
    public void OnMove(InputAction.CallbackContext context)
    {
        EventManager.OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OnJump?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            EventManager.OnCancelJump?.Invoke();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OnToggleSprint?.Invoke();
        }
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        EventManager.OnRotateCamera?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OnInteract?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            EventManager.OnCancelInteract?.Invoke();
        }
    }

    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OnOpenMenu?.Invoke();
        }
    }

    public void OnHighlightInteractables(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OnHighlightInteractables?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            EventManager.OnCancelHighlightInteractables?.Invoke();
        }
    }

    public void OnChangeCameraDistance(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OnChangeCameraDistance?.Invoke(context.ReadValue<Vector2>());
        }
    }
    #endregion

    #region UI event handlers
    public void OnCloseMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OnCloseMenu?.Invoke();
        }
    }
    #endregion
}
