// https://www.youtube.com/watch?v=GAh225QNpm0&ab_channel=KetraGames

using System;
using Cinemachine;
using UnityEngine;

public class PlayerFollowCameraController : MonoBehaviour
{
    [SerializeField]
    private float[] _zoomLevels = new float[] { 1.5f, 3f, 4.5f, 6f, 7.5f };
    [SerializeField]
    private float _zoomSpeed = 50f;

    [SerializeField]
    private SceneTracker _sceneTracker;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private Cinemachine3rdPersonFollow _cinemachine3rdPersonFollow;

    private CameraRoot _target;

    private float _zoomDirection;

    private int _currentZoomIndex;
    private float _currentZoomDistance;
    private float _targetZoomDistance;
    private float _zoomVelocity = 0f;

    private const int INDOORS_ZOOM_INDEX = 0;
    private const int OUTDOORS_ZOOM_INDEX = 4;

    private void Awake()
    {
        _cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();

        if (_cinemachineVirtualCamera != null)
        {
            _cinemachine3rdPersonFollow = _cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
    }

    private void OnEnable()
    {
        EventManager.OnSpawnPlayer += HandlePlayerSpawn;

        EventManager.OnZoom += HandleZoom;
    }

    private void Update()
    {
        if (_zoomDirection == 0) return;

        SmoothZoom();
    }

    private void OnDisable()
    {
        EventManager.OnSpawnPlayer -= HandlePlayerSpawn;

        EventManager.OnZoom -= HandleZoom;
    }

    private void HandlePlayerSpawn(GameObject player)
    {
        if (player == null)
        {
            throw new Exception("There is no Player");
        }

        if (_cinemachineVirtualCamera == null)
        {
            throw new Exception("There is no Virtual Camera.");
        }

        CameraRoot cameraRoot = player.GetComponentInChildren<CameraRoot>();

        if (cameraRoot == null)
        {
            throw new Exception("There is no Camera Root.");
        }

        _target = cameraRoot;

        _cinemachineVirtualCamera.Follow = _target.transform;
        _cinemachineVirtualCamera.LookAt = _target.transform;

        Vector3 currentRotation = _cinemachineVirtualCamera.transform.rotation.eulerAngles;

        Vector3 newRotation = player.transform.eulerAngles;

        _cinemachineVirtualCamera.transform.rotation = Quaternion.Euler(currentRotation.x, newRotation.y, currentRotation.z);

        if (_cinemachine3rdPersonFollow != null)
        {
            SceneNode currentSceneNode = _sceneTracker.CurrentScene;

            if (currentSceneNode != null)
            {
                _currentZoomIndex = currentSceneNode.SceneType == SceneType.INDOORS ? INDOORS_ZOOM_INDEX : OUTDOORS_ZOOM_INDEX;

                _currentZoomDistance = _zoomLevels[_currentZoomIndex];
                _targetZoomDistance = _currentZoomDistance;

                _cinemachine3rdPersonFollow.CameraDistance = _currentZoomDistance;
            }

        }
    }

    private void HandleZoom(float newZoomDirection)
    {
        _zoomDirection = newZoomDirection;

        SetZoomLevel();
    }

    private void SetZoomLevel()
    {
        if (_zoomDirection > 0f && _currentZoomIndex > 0)
        {
            _currentZoomIndex--;
            _targetZoomDistance = _zoomLevels[_currentZoomIndex];
        }
        else if (_zoomDirection < 0f && _currentZoomIndex < _zoomLevels.Length - 1)
        {
            _currentZoomIndex++;
            _targetZoomDistance = _zoomLevels[_currentZoomIndex];
        }
    }

    private void SmoothZoom()
    {
        // Smoothly transition between the current zoom distance and the target zoom distance
        _currentZoomDistance = Mathf.SmoothDamp(_currentZoomDistance, _targetZoomDistance, ref _zoomVelocity, _zoomSpeed * Time.unscaledDeltaTime);
        _cinemachine3rdPersonFollow.CameraDistance = _currentZoomDistance;
    }
}
