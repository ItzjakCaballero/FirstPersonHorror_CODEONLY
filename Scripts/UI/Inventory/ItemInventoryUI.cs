using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryItemPreviewImageUI itemImagePreview;
    [SerializeField] private InventoryItemSlotUI inventoryItemUITemplate;
    [SerializeField] private Transform itemsLayoutGroup;

    private List<InventoryItemSlotUI> inventoryItems;

    private void Awake()
    {
        inventoryItems = new List<InventoryItemSlotUI>();
    }

    public void AddItem(InventoryItemSO inventoryItem)
    {
        InventoryItemSlotUI newInventoryItemUI = Instantiate(inventoryItemUITemplate, itemsLayoutGroup);
        newInventoryItemUI.Setup(inventoryItem);
        newInventoryItemUI.OnInventoryItemClicked += InventoryItemUI_OnInventoryItemClicked;
        inventoryItems.Add(newInventoryItemUI);
    }

    private void InventoryItemUI_OnInventoryItemClicked(InventoryItemSO inventoryItem)
    {
        itemImagePreview.ChangeItemPreview(inventoryItem);
    }
}
