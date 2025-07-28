using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemInventoryUI itemInventoryUI;
    [SerializeField] private PhotoInventoryUI photoInventoryUI;

    private void Start()
    {
        PlayerInput.Instance.OnToggleInventoryPerformed += PlayerInput_OnToggleInventoryPerformed;
    }

    private void PlayerInput_OnToggleInventoryPerformed(object sender, System.EventArgs e)
    {
        if (MenuManager.Instance.GetActiveMenu() == GameMenus.None)
        {
            MenuManager.Instance.ShowMenu(GameMenus.Inventory);
        }
        else if (MenuManager.Instance.GetActiveMenu() == GameMenus.Inventory)
        {
            MenuManager.Instance.HideActiveMenu();
        }
    }

    public void AddItem(InventoryItemSO inventoryItem)
    {
        itemInventoryUI.AddItem(inventoryItem);
    }

    public void AddPhoto(Sprite photo)
    {
        photoInventoryUI.AddPhoto(photo);
    }

    public void RemovePhoto()
    {
        photoInventoryUI.RemovePhoto();
    }
}
