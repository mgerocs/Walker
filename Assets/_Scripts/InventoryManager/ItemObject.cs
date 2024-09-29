using UnityEngine;public enum ItemType
{
    Food,
    Equipment,
    Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]
public abstract class ItemObject : ScriptableObject
{
    public GameObject Prefab;
    public ItemType Type;
    [TextArea(15, 20)]
    public string Description;
}
