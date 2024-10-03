using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Jump { get; set; }
    public bool Sprint => _playerData.IsSprinting;

    [SerializeField]
    private PlayerData _playerData;

    private Vector2 _dummy = new Vector2(0, 0);

    private void OnEnable()
    {
        EventManager.Move += HandleMove;

        EventManager.Look += HandleLook;

        EventManager.Jump += HandleJump;
        EventManager.CancelJump += HandleJumpCanceled;

        EventManager.ToggleSprint += HandleToggleSprint;
    }

    private void OnDisable()
    {
        EventManager.Move -= HandleMove;

        EventManager.Look -= HandleLook;

        EventManager.Jump -= HandleJump;
        EventManager.CancelJump -= HandleJumpCanceled;

        EventManager.ToggleSprint -= HandleToggleSprint;
    }

    private void HandleMove(Vector2 newDirection)
    {
        Move = newDirection;
    }

    public void HandleLook(Vector2 newLookDirection)
    {
        Look = newLookDirection;
    }

    private void HandleJump()
    {
        Jump = true;
    }

    private void HandleJumpCanceled()
    {
        Jump = false;
    }

    private void HandleToggleSprint()
    {
        _playerData.IsSprinting = !_playerData.IsSprinting;
    }
}

