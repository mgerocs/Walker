// https://www.youtube.com/watch?v=GAh225QNpm0&ab_channel=KetraGames

using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraMaster : MonoBehaviour
{
    [SerializeField]
    private string _playerCameraTag = "PlayerCamera";

    [SerializeField]
    private CameraFade _cameraFade;

    CinemachineCamera _playerCamera;

    // the dialog camera of the NPC
    private CinemachineCamera _dialogCamera;
    // the position of the player while having a dialog with the NPC
    private Transform _playerDialogTransform;

    public static CameraMaster Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (_cameraFade == null)
        {
            Debug.LogError("Missing CameraFade reference.");
        }
    }

    private void OnEnable()
    {
        EventManager.OnPlayerSpawned += HandlePlayerSpawned;

        EventManager.StartDialog += HandleStartDialog;
        EventManager.FinishDialog += HandleFinishDialog;
    }

    private void Start()
    {
        if (_cameraFade != null)
        {
            _cameraFade.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        EventManager.OnPlayerSpawned -= HandlePlayerSpawned;

        EventManager.StartDialog -= HandleStartDialog;
        EventManager.FinishDialog -= HandleFinishDialog;
    }

    private void HandlePlayerSpawned()
    {
        GameObject cameraObject = GameObject.FindWithTag(_playerCameraTag);

        if (cameraObject != null)
        {
            if (!cameraObject.TryGetComponent<CinemachineCamera>(out var playerCamera))
            {
                Debug.LogError("Couldn't find PlayerCamera.");
                return;
            }

            _playerCamera = playerCamera;

            _playerCamera.Priority = 10;
        }
    }

    private void HandleStartDialog(NPCBase npc)
    {
        PlayerDialogPosition pdp = npc.GetComponentInChildren<PlayerDialogPosition>();

        if (pdp == null)
        {
            Debug.LogError("Couldn't find the PlayerDialogPosition component on the NPC.");
            return;
        }

        _playerDialogTransform = pdp.transform;

        CinemachineCamera dialogCamera = npc.GetComponentInChildren<CinemachineCamera>();

        if (dialogCamera == null)
        {
            Debug.LogError("Couldn't find the CinemachineCamera component on the NPC.");
            return;
        }

        _dialogCamera = dialogCamera;

        StartCoroutine(StartDialog());
    }

    private void HandleFinishDialog()
    {
        StartCoroutine(FinishDialog());
    }

    private IEnumerator StartDialog()
    {

        if (_cameraFade != null)
        {
            yield return StartCoroutine(FadeOut());
        }

        if (_playerDialogTransform != null)
        {
            // teleport player to the dialog position
            EventManager.TeleportPlayer.Invoke(_playerDialogTransform);
        }

        if (_playerCamera != null && _dialogCamera != null)
        {
            // switch to dialog camera
            SwitchCamera(_playerCamera, _dialogCamera);
        }

        if (_cameraFade != null)
        {
            yield return StartCoroutine(FadeIn());
        }

        EventManager.OnDialogStarted?.Invoke();
    }

    private IEnumerator FinishDialog()
    {
        if (_cameraFade != null)
        {
            yield return StartCoroutine(FadeOut());
        }

        if (_playerCamera != null && _dialogCamera != null)
        {
            // switch to player camera
            SwitchCamera(_dialogCamera, _playerCamera);
        }

        if (_cameraFade != null)
        {
            yield return StartCoroutine(FadeIn());
        }

        EventManager.OnDialogFinished?.Invoke();
    }

    private IEnumerator FadeOut()
    {
        _cameraFade.gameObject.SetActive(true);
        yield return StartCoroutine(_cameraFade.FadeOut());
    }

    private IEnumerator FadeIn()
    {
        yield return StartCoroutine(_cameraFade.FadeIn());
        _cameraFade.gameObject.SetActive(false);
    }

    private void SwitchCamera(CinemachineCamera prevCamera, CinemachineCamera nextCamera)
    {
        // Switch cameras
        nextCamera.Priority = 10;
        prevCamera.Priority = 0;
    }
}
