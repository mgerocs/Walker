using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField]
    private SceneTracker _sceneTracker;

    [SerializeField]
    private SceneField _initialScene;

    private void OnEnable()
    {
        EventManager.OnSceneChange += HandleSceneChange;
    }

    private void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        EventManager.OnSceneChange -= HandleSceneChange;
    }

    private void HandleSceneChange(SceneField nextScene, string gateName)
    {
        _sceneTracker.ChangeScene(nextScene.SceneName, gateName);

        string sceneToLoad = _sceneTracker.NextScene ?? _initialScene.SceneName;

        // SceneManager.LoadScene(sceneToLoad.ToString());

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        StartCoroutine(_sceneTracker.LoadSceneAsync(operation));
    }

    private void Init()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;

        _sceneTracker.SetScene(currentSceneName, null, null);
    }

    private SceneName ConvertStringToEnum(string nameToParse)
    {
        if (Enum.TryParse(nameToParse, out SceneName sceneName))
        {
            return sceneName;
        }
        else
        {
            throw new Exception("Scene is not in list of scenes.");
        }
    }
}
