using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MenuBase
{
    [SerializeField]
    private InventoryTracker _inventoryTracker;

    [Header("Inventory UI Settings")]
    [SerializeField]
    private int _maxSlots = 20; // Maximum number of slots
    [SerializeField]
    private int _columns = 4;    // Number of columns
    [SerializeField]
    private float _gap = 5f;     // Gap between slots

    [Header("Prefabs and References")]
    [SerializeField]
    private GameObject _slotPrefab; // The slot prefab
    [SerializeField]
    private RectTransform _inventoryPanelPrefab; // The parent panel where slots are instantiated

    private int _rows; // Calculated number of rows based on columns and max slots

    private void OnEnable()
    {
        GenerateUI();
    }

    private void GenerateUI()
    {

        _inventoryPanelPrefab.pivot = new Vector2(0, 1);

        _rows = Mathf.CeilToInt((float)_maxSlots / _columns);

        // Clear existing slots if any
        foreach (Transform child in _inventoryPanelPrefab)
        {
            Destroy(child.gameObject);
        }

        // Calculate the size of each slot based on the panel width and columns (to make slots square)
        Vector2 panelSize = _inventoryPanelPrefab.rect.size;
        float slotSize = (panelSize.x - (_gap * (_columns - 1))) / _columns; // Width and Height (square)

        // Calculate the required height for the inventory panel
        float requiredHeight = _rows * slotSize + (_rows - 1) * _gap;

        // Set the height of the InventoryPanel to fit all rows
        RectTransform inventoryPanelRect = _inventoryPanelPrefab.GetComponent<RectTransform>();
        inventoryPanelRect.sizeDelta = new Vector2(inventoryPanelRect.sizeDelta.x, requiredHeight);

        // Calculate starting position (top-left corner)
        float startX = 30;
        float startY = -30;

        // Create the grid of slots
        for (int i = 0; i < _maxSlots; i++)
        {
            // Instantiate slot prefab
            GameObject slot = Instantiate(_slotPrefab, _inventoryPanelPrefab);

            // Calculate the position of the slot
            int row = i / _columns;
            int col = i % _columns;

            float xPos = startX + (slotSize + _gap) * col;
            float yPos = startY - (slotSize + _gap) * row; // Negative y to go downward

            // Set the slot's size and position
            RectTransform slotRect = slot.GetComponent<RectTransform>();
            slotRect.pivot = new Vector2(0, 1);
            slotRect.sizeDelta = new Vector2(slotSize, slotSize);
            slotRect.anchoredPosition = new Vector2(xPos, yPos);

            if (_inventoryTracker.Container.Count > 0 && i < _inventoryTracker.Container.Count)
            {

                InventorySlot inventorySlot = _inventoryTracker.Container[i];

                GameObject item = Instantiate(inventorySlot.Item.Prefab, slot.transform);

                // Get RectTransform of the item prefab and adjust its size/position
                RectTransform itemRect = item.GetComponent<RectTransform>();
                itemRect.anchoredPosition = Vector2.zero; // Center the prefab in the slot
                itemRect.sizeDelta = new Vector2(slotSize, slotSize); // Ensure it's square and fits the slot
                itemRect.localScale = Vector3.one; // Reset scale in case the prefab has a different scale

                item.transform.SetSiblingIndex(0); // Move item prefab to the bottom

                TextMeshProUGUI tmpro = slot.GetComponentInChildren<TextMeshProUGUI>();
                tmpro.text = inventorySlot.Amount.ToString();
            }
        }
    }
}
