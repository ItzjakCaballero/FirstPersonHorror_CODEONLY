using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private List<InventoryItemSO> inventoryItems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple PlayerInventory's active");
        }
        inventoryItems = new List<InventoryItemSO>();
    }

    public void AddInventoryItem(InventoryItemSO item)
    {
        inventoryItems.Add(item);
        MenuManager.Instance.GetMenuObject<InventoryUI>(GameMenus.Inventory).AddItem(item);
    }

    public bool HasItem(InventoryItemSO item)
    {
        if (inventoryItems.Contains(item))
        {
            return true;
        }
        return false;
    }
}
