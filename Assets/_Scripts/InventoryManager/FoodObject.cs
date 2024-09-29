using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory Manager/Items/Food")]
public class FoodObject : ItemObject
{
    public int RestoreHealthValue;

    private void Awake()
    {
        Type = ItemType.Food;
    }

}