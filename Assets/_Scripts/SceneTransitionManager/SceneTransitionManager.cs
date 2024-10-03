using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField]
    private SceneTracker _sceneTracker;

    [SerializeField]
    private SceneData _initialScene;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private string _playerTag = "Player";

    private void Awake()
    {
        if (_sceneTracker == null)
        {
            Debug.LogError("Missing SceneTracker reference.");
        }

        if (_initialScene == null)
        {
            Debug.LogError("Missing SceneData referenece (Initial Scene).");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;

        EventManager.OnExitScene += HandleExitScene;
        EventManager.FastTravel += HandleFastTravel;
    }

    private void Start()
    {
        if (_sceneTracker != null && _initialScene != null)
        {
            _sceneTracker.CurrentScene = _initialScene;
            _sceneTracker.LastGatewayName = null;
        }

        Init();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;

        EventManager.OnExitScene -= HandleExitScene;
        EventManager.FastTravel -= HandleFastTravel;
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Init();
    }

    private void HandleExitScene(SceneData sceneData, string gateName)
    {
        ExitScene(sceneData, gateName);
    }

    private void HandleFastTravel(SceneData sceneData)
    {
        FastTravel(sceneData);
    }

    public void Init()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (_playerPrefab == null)
        {
            Debug.LogError("No Player Prefab.");
            return;
        }

        if (!_playerPrefab.CompareTag(_playerTag))
        {
            Debug.LogError($"Player object should have the tag '{_playerTag}'");
            return;
        }

        if (_playerPrefab.activeInHierarchy)
        {
            Debug.LogError("There is a Player already in the scene.");
            return;
        }

        SpawnPoint spawnPoint = FindSpawnPoint();

        if (spawnPoint == null)
        {
            Debug.LogError("No designated SpawnPoints found.");
            return;
        }

        GameObject instance = Instantiate(_playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

        EventManager.OnPlayerSpawned?.Invoke();
    }

    private SpawnPoint FindSpawnPoint()
    {
        Gateway[] gatewaysInScene = FindObjectsByType<Gateway>(FindObjectsSortMode.None);

        if (gatewaysInScene.Length == 0)
        {
            return FindDefaultSpawnPoint();
        }

        if (gatewaysInScene.Length == 1)
        {
            return gatewaysInScene[0].SpawnPoint;
        }

        if (_sceneTracker != null)
        {
            string lastGatewayName = _sceneTracker.LastGatewayName;

            if (lastGatewayName != null)
            {
                Gateway targetGateway = gatewaysInScene.FirstOrDefault(gw => gw.GatewayName == lastGatewayName);

                if (targetGateway != null)
                {
                    return targetGateway.SpawnPoint;
                }
            }
        }

        return FindDefaultSpawnPoint();
    }

    private SpawnPoint FindDefaultSpawnPoint()
    {
        SpawnPoint[] spawnPointsInScene = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);

        if (spawnPointsInScene.Length == 0) return null;

        return spawnPointsInScene.FirstOrDefault(sp => sp.IsDefault);
    }

    public void ExitScene(SceneData destination, string lastGatewayName)
    {
        if (_sceneTracker == null) return;

        _sceneTracker.LastGatewayName = lastGatewayName;

        _sceneTracker.CurrentScene = destination;

        //  SceneManager.LoadScene(_sceneTracker.CurrentScene.SceneField.SceneName);

        LoadScene(_sceneTracker.CurrentScene.SceneField.SceneName);
    }

    public void FastTravel(SceneData scene)
    {
        if (_sceneTracker == null) return;

        _sceneTracker.CurrentScene = scene;

        _sceneTracker.LastGatewayName = null;

        //SceneManager.LoadScene(_sceneTracker.CurrentScene.SceneField.SceneName);

        LoadScene(scene.SceneField.SceneName);
    }

    private void LoadScene(string sceneToLoad)
    {
        EventManager.OnLoadingStart?.Invoke();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        StartCoroutine(LoadSceneAsync(operation));
    }

    private IEnumerator LoadSceneAsync(AsyncOperation operation)
    {
        EventManager.OnLoadingStart?.Invoke();

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            EventManager.OnLoadingProgress?.Invoke(progress);

            yield return null;
        }

        if (operation.isDone)
        {
            EventManager.OnLoadingFinish?.Invoke();
        }
    }
}
