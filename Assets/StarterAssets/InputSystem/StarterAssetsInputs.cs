using UnityEngine;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public Vector2 rotate;

		private void OnEnable()
		{
			EventManager.OnMove += HandleMove;

			EventManager.OnJump += HandleJump;
			EventManager.OnCancelJump += HandleJumpCanceled;

			EventManager.OnToggleSprint += HandleToggleSprint;
		}

		private void OnDisable()
		{
			EventManager.OnMove -= HandleMove;

			EventManager.OnJump -= HandleJump;
			EventManager.OnCancelJump -= HandleJumpCanceled;

			EventManager.OnToggleSprint -= HandleToggleSprint;
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
	}

}