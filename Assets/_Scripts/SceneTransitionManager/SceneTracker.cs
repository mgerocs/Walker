using UnityEngine;

[CreateAssetMenu(fileName = "SceneTracker", menuName = "Scriptable Objects/SceneTracker")]
public class SceneTracker : ScriptableObject
{
    public SceneData CurrentScene { get; set; }
    public string LastGatewayName { get; set; }
}
