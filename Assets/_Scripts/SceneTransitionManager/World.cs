using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    INDOORS,
    OUTDOORS
}

[CreateAssetMenu(fileName = "World", menuName = "Scriptable Objects/World")]
public class World : ScriptableObject
{
    public List<Area> Areas = new();
}