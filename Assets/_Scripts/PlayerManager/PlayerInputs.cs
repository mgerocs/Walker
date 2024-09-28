using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public float zoom;
    public bool jump;
    public bool sprint;
    public Vector2 rotate;

    private void OnEnable()
    {
        EventManager.OnMove += HandleMove;

        EventManager.OnLook += HandleLook;

        EventManager.OnZoom += HandleZoom;

        EventManager.OnJump += HandleJump;
        EventManager.OnCancelJump += HandleJumpCanceled;

        EventManager.OnToggleSprint += HandleToggleSprint;
    }

    private void OnDisable()
    {
        EventManager.OnMove -= HandleMove;

        EventManager.OnLook -= HandleLook;

        EventManager.OnZoom -= HandleZoom;

        EventManager.OnJump -= HandleJump;
        EventManager.OnCancelJump -= HandleJumpCanceled;

        EventManager.OnToggleSprint -= HandleToggleSprint;
    }

    private void HandleMove(Vector2 newDirection)
    {
        move = newDirection;
    }

    public void HandleLook(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    private void HandleZoom(float newScrollDirection)
    {
        zoom = newScrollDirection;
    }

    private void HandleJump()
    {
        jump = true;
    }

    private void HandleJumpCanceled()
    {
        jump = false;
    }

    private void HandleToggleSprint()
    {
        sprint = !sprint;
    }
}

