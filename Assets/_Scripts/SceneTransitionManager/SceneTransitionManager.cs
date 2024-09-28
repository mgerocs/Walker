using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField]
    private World _world;

    [SerializeField]
    private SceneTracker _sceneTracker;

    [SerializeField]
    private SceneField _initialScene;

    private void OnEnable()
    {
        EventManager.OnChangeScene += HandleChangeScene;
    }

    private void OnDisable()
    {
        EventManager.OnChangeScene -= HandleChangeScene;
    }

    public void Init()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneNode currentSceneNode = _world.GetSceneNodeBySceneName(currentScene.name);

        if (currentSceneNode != null)
        {
            _sceneTracker.SetScene(currentSceneNode, null, null);
        }
    }

    private void HandleChangeScene(SceneField nextScene, string gateName)

    {
        SceneNode sceneNode = _world.GetSceneNodeBySceneName(nextScene.SceneName);

        _sceneTracker.ChangeScene(sceneNode, gateName);

        string sceneToLoad = _sceneTracker.NextScene.SceneField.SceneName ?? _initialScene.SceneName;

        // SceneManager.LoadScene(sceneToLoad.ToString());

        EventManager.OnLoadingStart?.Invoke();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        StartCoroutine(_sceneTracker.LoadSceneAsync(operation));
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
