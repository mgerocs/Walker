using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private SceneTracker _sceneTracker;

    [SerializeField]
    private float[] _zoomLevels = new float[] { 1.5f, 3f, 4.5f, 6f, 7.5f };

    [SerializeField]
    private float _zoomSpeed = 50f;

    CinemachineThirdPersonFollow _cinemachine3rdPersonFollow;

    private float _zoomDirection;
    private int _currentZoomIndex;
    private float _currentZoomDistance;
    private float _targetZoomDistance;
    private float _zoomVelocity = 0f;

    private const int INDOORS_ZOOM_INDEX = 0;
    private const int OUTDOORS_ZOOM_INDEX = 4;

    private void Awake()
    {
        _cinemachine3rdPersonFollow = gameObject.GetComponent<CinemachineThirdPersonFollow>();

        if (_cinemachine3rdPersonFollow == null)
        {
            Debug.LogError("No CinemachineThirdPersonFollow component on Player Camera.");
        }

        if (_sceneTracker == null)
        {
            Debug.LogError("Missing SceneTracker reference.");
        }
    }

    private void OnEnable()
    {
        EventManager.Zoom += HandleZoom;

        EventManager.OnPlayerSpawned += HandlePlayerSpawned;
    }

    private void Update()
    {
        if (_zoomDirection == 0) return;

        SmoothZoom();
    }

    private void OnDisable()
    {
        EventManager.Zoom -= HandleZoom;

        EventManager.OnPlayerSpawned -= HandlePlayerSpawned;
    }

    private void HandleZoom(float newZoomDirection)
    {
        _zoomDirection = newZoomDirection;

        SetZoomLevel();
    }

    private void HandlePlayerSpawned()
    {
        UpdateZoomDistance();
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
        if (_cinemachine3rdPersonFollow == null) return;

        // Smoothly transition between the current zoom distance and the target zoom distance
        _currentZoomDistance = Mathf.SmoothDamp(_currentZoomDistance, _targetZoomDistance, ref _zoomVelocity, _zoomSpeed * Time.unscaledDeltaTime);
        _cinemachine3rdPersonFollow.CameraDistance = _currentZoomDistance;
    }

    private void UpdateZoomDistance()
    {
        if (_sceneTracker == null) return;

        SceneData currentScene = _sceneTracker.CurrentScene;

        if (currentScene == null) return;

        _currentZoomIndex = currentScene.SceneType == SceneType.INDOORS ? INDOORS_ZOOM_INDEX : OUTDOORS_ZOOM_INDEX;

        _currentZoomDistance = _zoomLevels[_currentZoomIndex];
        _targetZoomDistance = _currentZoomDistance;

        if (_cinemachine3rdPersonFollow != null)
        {
            _cinemachine3rdPersonFollow.CameraDistance = _currentZoomDistance;
        }
    }
}