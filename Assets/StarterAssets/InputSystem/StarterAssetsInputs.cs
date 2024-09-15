using UnityEngine;
using Cinemachine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

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
		}

		private void Start()
		{
			// Subscribe to InputReader events
			inputReader.MoveEvent += HandleMove;

			inputReader.JumpEvent += HandleJump;
			inputReader.JumpCanceledEvent += HandleCanceledJump;

			inputReader.ToggleSprintEvent += HandleToggleSprint;

			inputReader.RotateCameraEvent += HandleRotateCamera;
		}

		private void HandleMove(Vector2 direction)
		{
			move = direction;
		}

		private void HandleJump()
		{
			jump = true;
		}

		private void HandleCanceledJump()
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