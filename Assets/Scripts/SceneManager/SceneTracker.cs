using UnityEngine;

[CreateAssetMenu(fileName = "SceneTracker", menuName = "Scriptable Objects/SceneTracker")]
public class SceneTracker : ScriptableObject
{

    public SceneName PreviousScene { get; private set; }
    public SceneName NextScene { get; private set; }
    public string GatewayName { get; private set; }

    public void ChangeScene(SceneName origin, SceneName destination, string gatewayName)
    {
        PreviousScene = origin;
        NextScene = destination;
        GatewayName = gatewayName;

        Debug.Log($"Prev: {PreviousScene} | Next: {NextScene} | Gateway: {GatewayName}");

        EventManager.OnSceneChange.Invoke();
    }

    public void Reset () {
        PreviousScene = SceneName.None;
        NextScene = SceneName.None;
        GatewayName = null;
    }


}
