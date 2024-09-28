using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData")]
public class SceneData : ScriptableObject
{
    public string SceneTitle;
    public string SceneDescription;
    public SceneType SceneType;
    public SceneField SceneField;
}
