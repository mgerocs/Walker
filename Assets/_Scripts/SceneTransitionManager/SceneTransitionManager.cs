using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField]
    private SceneTracker _sceneTracker;

    [SerializeField]
    private SceneName _initialScene;

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

    private void HandleSceneChange(SceneName nextScene, string gateName)
    {
        _sceneTracker.ChangeScene(nextScene, gateName);

        SceneName sceneToLoad = _sceneTracker.NextScene;

        if (sceneToLoad == SceneName.None)
        {
            sceneToLoad = _initialScene;
        }

        Debug.Log("Handle Scene Change");

        // SceneManager.LoadScene(sceneToLoad.ToString());

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad.ToString(), LoadSceneMode.Single);

        StartCoroutine(_sceneTracker.LoadSceneAsync(operation));
    }

    private void Init()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneName currentSceneName = ConvertStringToEnum(currentScene.name);

        _sceneTracker.SetScene(currentSceneName, SceneName.None, null);
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
