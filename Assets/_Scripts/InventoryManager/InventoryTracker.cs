using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Tracker", menuName = "Inventory Manager/Inventory Tracker")]
public class InventoryTracker : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount)
    {
        bool hasItem = false;

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].Item == item)
            {

                Container[i].AddAmount(amount);

                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Container.Add(new InventorySlot(item, amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject Item;
    public int Amount;

    public InventorySlot(ItemObject item, int amount)
    {
        Item = item;
        Amount = amount;
    }

    public void AddAmount(int value)
    {
        Amount += value;
    }
}