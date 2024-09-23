using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneGraph", menuName = "Scriptable Objects/Scene Graph")]
public class SceneGraph : ScriptableObject
{
    [System.Serializable]
    public class SceneNode
    {
        public string sceneName;
        public List<SceneNode> neighbors;
    }

    public List<SceneNode> scenes = new();

    public SceneNode GetSceneNodeByName(string sceneName)
    {
        return scenes.Find(scene => scene.sceneName == sceneName);
    }

}