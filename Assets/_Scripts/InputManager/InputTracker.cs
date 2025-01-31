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
        EventManager.Move?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        EventManager.Look?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.Jump?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            EventManager.CancelJump?.Invoke();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.ToggleSprint?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.Interact?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            EventManager.CancelInteract?.Invoke();
        }
    }

    public void OnOpenPauseMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OpenPauseMenu?.Invoke();
        }
    }

    public void OnHighlightInteractables(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.HighlightInteractables?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            EventManager.CancelHighlightInteractables?.Invoke();
        }
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.Zoom?.Invoke(context.ReadValue<float>());
        }
    }

    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OpenMap?.Invoke();
        }
    }

    public void OnCharacter(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OpenCharacterMenu?.Invoke();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OpenInventory?.Invoke();
        }
    }

    public void OnTime(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.OpenTimeMenu?.Invoke();
        }
    }
    #endregion

    #region UI event handlers
    public void OnCloseMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EventManager.CloseMenu?.Invoke();
        }
    }
    #endregion
}
