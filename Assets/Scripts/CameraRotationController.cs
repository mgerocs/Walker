using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    [Header("Input Reader")]
    public InputReader inputReader;

    [Header("Camera rotation speed")]
    public float rotationSpeed = 100f;

    private float _rotate;


    private void Start()
    {
        // Subscribe to InputReader events
        inputReader.RotateCameraEvent += HandleRotateCamera;
    }

    // Update is called once per frame
    private void Update()
    {
        RotateCamera();
    }

    private void HandleRotateCamera(Vector2 direction)
    {
        _rotate = direction.x;
    }

    private void RotateCamera()
    {
        if (_rotate != 0)
        {
            // Get the current Euler angles
            Vector3 currentRotation = gameObject.transform.rotation.eulerAngles;

            // Calculate the new Y rotation
            float newYRotation = currentRotation.y + (_rotate * rotationSpeed * Time.deltaTime);

            // Create a new Quaternion with the current X and Z rotation, and the updated Y rotation
            Quaternion targetRotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);

            gameObject.transform.rotation = targetRotation;
        }
    }
}
