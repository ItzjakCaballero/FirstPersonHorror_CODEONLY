using UnityEngine;

public class InteractableInventoryItemPickUp : Interactable
{
    [SerializeField] private InventoryItemSO inventoryItem;

    protected override void BaseInteract(GameObject player)
    {
        PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        playerInventory.AddInventoryItem(inventoryItem);
        gameObject.SetActive(false);
//        Destroy(gameObject);
    }
}
