using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SceneType
{
    INDOORS,
    OUTDOORS
}

[CreateAssetMenu(fileName = "World", menuName = "Scriptable Objects/World")]
public class World : ScriptableObject
{
    public List<AreaNode> Areas = new();

    public SceneNode GetSceneNodeBySceneName(string sceneName)
    {
        return Areas.SelectMany(area => area.Scenes).FirstOrDefault(sceneNode => sceneNode.SceneField.SceneName == sceneName);
    }
}