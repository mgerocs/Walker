using UnityEngine;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Input Reader")]
		public InputReader inputReader;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public Vector2 rotate;

		private void OnEnable()
		{
			inputReader.SetGameplay();

			// Subscribe to InputReader events
			inputReader.MoveEvent += HandleMove;

			inputReader.JumpEvent += HandleJump;
			inputReader.JumpCanceledEvent += HandleJumpCanceled;

			inputReader.ToggleSprintEvent += HandleToggleSprint;

			inputReader.RotateCameraEvent += HandleRotateCamera;
		}

		private void OnDisable()
		{
			inputReader.MoveEvent -= HandleMove;

			inputReader.JumpEvent -= HandleJump;
			inputReader.JumpCanceledEvent -= HandleJumpCanceled;

			inputReader.ToggleSprintEvent -= HandleToggleSprint;

			inputReader.RotateCameraEvent -= HandleRotateCamera;
		}

		private void HandleMove(Vector2 direction)
		{
			move = direction;
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

		private void HandleRotateCamera(Vector2 direction)
		{
			rotate = direction;
		}
	}

}