using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory Manager/Items/Default")]
public class DefaultObject : ItemObject
{
    private void Awake()
    {
        Type = ItemType.Default;
    }

}