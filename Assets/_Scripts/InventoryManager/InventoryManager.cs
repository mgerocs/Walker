using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private InventoryTracker _inventoryTracker;

    private void Awake()
    {
        if (_inventoryTracker == null)
        {
            throw new Exception("No InventoryTracker.");
        }
    }

    private void OnEnable()
    {
        EventManager.OnItemPickedUp += HandleItemPickedUp;
    }

    private void OnDisable()
    {
        EventManager.OnItemPickedUp -= HandleItemPickedUp;
    }

    private void HandleItemPickedUp(ItemObject item)
    {
        _inventoryTracker.AddItem(item, 1);
    }
}
