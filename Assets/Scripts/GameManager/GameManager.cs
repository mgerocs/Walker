using System;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private MenuTracker _menuTracker;

    [SerializeField]
    private SceneTracker _sceneTracker;


    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private SpawnPoint _defaultSpawnPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        EventManager.OnPause += HandlePause;
        EventManager.OnResume += HandleResume;
    }

    private void Start()
    {
        SpawnCharacter();
    }

    private void OnDisable()
    {
        EventManager.OnPause -= HandlePause;
        EventManager.OnResume -= HandleResume;
    }

    public bool IsPaused { get; private set; } = false;

    private void HandlePause(Component component)
    {
        if (!IsPaused)
        {
            IsPaused = true;
            Pause();
        }
    }

    private void HandleResume(Component component)
    {
        if (IsPaused)
        {
            IsPaused = false;
            Resume();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;

        _inputReader.SetUI();
    }

    private void Resume()
    {
        Time.timeScale = 1;

        _inputReader.SetGameplay();
    }

    private void Init()
    {
        Time.timeScale = 1;

        _inputReader.SetGameplay();
    }

    private void SpawnCharacter()
    {
        if (_player == null) return;

        if (_player.tag != "Player") return;

        if (_player.activeInHierarchy) return;

        SpawnPoint spawnPoint = _defaultSpawnPoint;

        string prevGatewayName = _sceneTracker.GatewayName;

        if (prevGatewayName != null)
        {
            Gateway[] gatewaysInScene = FindObjectsByType<Gateway>(FindObjectsSortMode.None);

            if (gatewaysInScene.Length == 0)
            {
                throw new Exception("No Gateways in scene.");
            }

            if (gatewaysInScene.Length > 1)
            {
                Gateway targetGateway = gatewaysInScene.FirstOrDefault(g => g.Name == prevGatewayName);

                if (targetGateway != null)
                {
                    spawnPoint = targetGateway.SpawnPoint;
                }
            }
            else
            {
                spawnPoint = gatewaysInScene[0].SpawnPoint;
            }

            if (spawnPoint == null)
            {
                throw new Exception("No SpawnPoint found.");
            }
        }

        GameObject instance = Instantiate(_player, spawnPoint.gameObject.transform.position, spawnPoint.gameObject.transform.rotation);

        EventManager.OnPlayerSpawn.Invoke(instance);

        Init();
    }
}
