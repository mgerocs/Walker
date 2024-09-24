using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneTracker", menuName = "Scriptable Objects/SceneTracker")]
public class SceneTracker : ScriptableObject
{

    public string CurrentScene { get; private set; }
    public string NextScene { get; private set; }
    public string GatewayName { get; private set; }

    public void ChangeScene(string destination, string gatewayName)
    {
        CurrentScene = NextScene;
        NextScene = destination;
        GatewayName = gatewayName;

        // Debug.Log($"Current: {CurrentScene} | Next: {NextScene} | Gateway: {GatewayName}");
    }

    public void SetScene(string origin, string destination, string gatewayName)
    {
        CurrentScene = origin;
        NextScene = destination;
        GatewayName = gatewayName;
    }

    public IEnumerator LoadSceneAsync(AsyncOperation operation)
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
