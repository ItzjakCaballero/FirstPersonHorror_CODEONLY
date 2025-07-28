using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotUI : MonoBehaviour
{
    public event Action<InventoryItemSO> OnInventoryItemClicked;

    public InventoryItemSO inventoryItem { get; private set; }

    [SerializeField] private Image itemImage;

    private Button itemButton;

    private void Start()
    {
        itemButton = GetComponent<Button>();
        itemButton.onClick.AddListener(() =>
        {
            OnInventoryItemClicked?.Invoke(inventoryItem);
        });
    }

    public void Setup(InventoryItemSO inventoryItem)
    {
        this.inventoryItem = inventoryItem;
        itemImage.sprite = this.inventoryItem.itemSprite;
    }
}
