using UnityEngine;
using Cinemachine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Camera rotation speed")]
		public float rotationSpeed = 100f;

		public CinemachineVirtualCamera virtualCamera;

		private float horizontalInput;

		void Update()
		{
			if (virtualCamera != null && horizontalInput != 0)
			{

				// Get the current Euler angles
				Vector3 currentRotation = virtualCamera.transform.rotation.eulerAngles;

				// Calculate the new Y rotation
				float newYRotation = currentRotation.y + (horizontalInput * rotationSpeed * Time.deltaTime);

				// Create a new Quaternion with the current X and Z rotation, and the updated Y rotation
				Quaternion targetRotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);

				virtualCamera.transform.rotation = targetRotation;
			}

		}

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(!sprint);
		}

		public void OnRotateCamera(InputValue value)
		{
			RotateCamera(value.Get<Vector2>().x);
		}

#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		private void RotateCamera(float newHorizontalInput)
		{
			horizontalInput = newHorizontalInput;
		}
	}

}