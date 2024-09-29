using System;
using UnityEngine;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
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
			EventManager.Move += HandleMove;

			EventManager.Look += HandleLook;

			EventManager.Zoom += HandleZoom;

			EventManager.Jump += HandleJump;
			EventManager.CancelJump += HandleJumpCanceled;

			EventManager.ToggleSprint += HandleToggleSprint;
		}

		private void OnDisable()
		{
			EventManager.Move -= HandleMove;

			EventManager.Look -= HandleLook;

			EventManager.Zoom -= HandleZoom;

			EventManager.Jump -= HandleJump;
			EventManager.CancelJump -= HandleJumpCanceled;

			EventManager.ToggleSprint -= HandleToggleSprint;
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

}