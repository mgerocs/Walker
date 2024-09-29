using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory Manager/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public float AttackBonus;
    public float DefenseBonus;

    private void Awake()
    {
        Type = ItemType.Food;
    }

}