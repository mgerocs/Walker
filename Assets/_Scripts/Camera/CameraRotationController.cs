using Cinemachine;
using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 100f;

    private CinemachineVirtualCamera _vcam;

    private float _rotate;

    private void OnEnable()
    {
        _vcam = gameObject.GetComponent<CinemachineVirtualCamera>();

        EventManager.OnRotateCamera += HandleRotateCamera;

        EventManager.OnPlayerSpawn += HandlePlayerSpawn;
    }

    private void OnDisable()
    {
        EventManager.OnRotateCamera -= HandleRotateCamera;

        EventManager.OnPlayerSpawn -= HandlePlayerSpawn;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void HandleRotateCamera(Vector2 direction)
    {
        _rotate = direction.x;
    }

    private void HandlePlayerSpawn(GameObject player)
    {
        if (player == null) return;

        _vcam.Follow = player.transform;

        Vector3 currentRotation = _vcam.transform.rotation.eulerAngles;

        Vector3 newRotation = player.transform.eulerAngles;

        Quaternion targetRotation = Quaternion.Euler(currentRotation.x, newRotation.y, currentRotation.z);

        _vcam.transform.rotation = targetRotation;
    }

    private void RotateCamera()
    {
        if (_vcam == null) return;

        if (_rotate == 0) return;

        // Get the current Euler angles
        Vector3 currentRotation = _vcam.transform.rotation.eulerAngles;

        // Calculate the new Y rotation
        float newYRotation = currentRotation.y + (_rotate * _rotationSpeed * Time.deltaTime);

        // Create a new Quaternion with the current X and Z rotation, and the updated Y rotation
        Quaternion targetRotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);

        _vcam.transform.rotation = targetRotation;
    }
}
