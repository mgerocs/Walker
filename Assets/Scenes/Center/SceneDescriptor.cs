using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneDescriptor", menuName = "Scriptable Objects/SceneDescriptor")]
public class SceneDescriptor : ScriptableObject
{
    [System.Serializable]
    public class SpawnPoint
    {
        public SceneName originScene;
        public Transform spawnPoint;
    }

    public Transform transform;

    public SceneName scene;

    public string areaName;

    public List<SceneName> neighbours;

    public List<SpawnPoint> spawnPoints;

}
