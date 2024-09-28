using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField]
    private SceneTracker _sceneTracker;

    [SerializeField]
    private SceneData _initialScene;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void Start()
    {
        if (_initialScene == null)
        {
            throw new Exception("No Initial Scene set.");
        }

        _sceneTracker.CurrentScene = _initialScene;
        _sceneTracker.LastGatewayName = null;

        Init();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Init();
    }

    public void Init()
    {
        GameMaster.Instance.PlayerManager.SpawnPlayer();
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

    public void ExitScene(SceneData destination, string lastGatewayName)
    {
        _sceneTracker.LastGatewayName = lastGatewayName;

        _sceneTracker.CurrentScene = destination;

        //  SceneManager.LoadScene(_sceneTracker.CurrentScene.SceneField.SceneName);

        LoadScene(_sceneTracker.CurrentScene.SceneField.SceneName);
    }

    public void FastTravel(SceneData scene)
    {
        _sceneTracker.CurrentScene = scene;

        _sceneTracker.LastGatewayName = null;

        //SceneManager.LoadScene(_sceneTracker.CurrentScene.SceneField.SceneName);

        LoadScene(scene.SceneField.SceneName);
    }
}
