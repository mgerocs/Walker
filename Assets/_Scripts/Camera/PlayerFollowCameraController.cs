// https://www.youtube.com/watch?v=GAh225QNpm0&ab_channel=KetraGames

using System;
using Cinemachine;
using UnityEngine;

public class PlayerFollowCameraController : MonoBehaviour
{
    [SerializeField]
    private float _rotationX = 10f;
    [SerializeField]
    private float _rotationSpeed = 150f;
    [SerializeField]
    private float _zoomSpeed = 50f;
    [SerializeField]
    private float _minDistance = 1.5f;
    [SerializeField]
    private float _maxDistance = 6f;
    [SerializeField]
    private float _initialDistance = 7f;

    private CinemachineVirtualCamera _vcam;
    private CinemachineFramingTransposer _vcamFramingTransposer;

    private float _rotate;

    private void OnEnable()
    {
        _vcam = gameObject.GetComponent<CinemachineVirtualCamera>();

        if (_vcam != null)
        {
            Transform cameraTransform = _vcam.transform;
            Vector3 currentRotation = cameraTransform.eulerAngles;

            cameraTransform.rotation = Quaternion.Euler(_rotationX, 0, 0);

            _vcamFramingTransposer = _vcam.GetCinemachineComponent<CinemachineFramingTransposer>();

            if (_vcamFramingTransposer != null)
            {
                _vcamFramingTransposer.m_CameraDistance = _initialDistance;
            }
        }

        EventManager.OnRotateCamera += HandleRotateCamera;
        EventManager.OnChangeCameraDistance += HandleChangeCameraDistance;

        EventManager.OnPlayerSpawn += HandlePlayerSpawn;
    }

    private void OnDisable()
    {
        EventManager.OnRotateCamera -= HandleRotateCamera;
        EventManager.OnChangeCameraDistance -= HandleChangeCameraDistance;

        EventManager.OnPlayerSpawn -= HandlePlayerSpawn;
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void HandleRotateCamera(Vector2 direction)
    {
        _rotate = direction.x;
    }

    private void HandleChangeCameraDistance(Vector2 scrollValue)
    {
        float scrollAmount = scrollValue.y;

        if (_vcamFramingTransposer == null) return;

        // Adjust the camera distance from the player by modifying the Camera Distance
        float newDistance = _vcamFramingTransposer.m_CameraDistance - scrollAmount * _zoomSpeed * Time.deltaTime;
        _vcamFramingTransposer.m_CameraDistance = Mathf.Clamp(newDistance, _minDistance, _maxDistance);
    }

    private void HandlePlayerSpawn(GameObject player)
    {
        if (player == null)
        {
            throw new Exception("There is no Player");
        }

        if (_vcam == null)
        {
            throw new Exception("There is no Virtual Camera.");
        }

        CameraRoot cameraRoot = player.GetComponentInChildren<CameraRoot>();

        Debug.Log("Camera Root" + cameraRoot.transform);

        if (cameraRoot == null)
        {
            throw new Exception("There is no Camera Root.");
        }

        _vcam.Follow = cameraRoot.transform;
        _vcam.LookAt = cameraRoot.transform;

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
