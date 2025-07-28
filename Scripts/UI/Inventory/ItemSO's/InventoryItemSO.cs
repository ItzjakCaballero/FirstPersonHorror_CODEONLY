using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/Inventory Items/Standard Item")]
public class InventoryItemSO : ScriptableObject
{
    public Sprite itemSprite;
    public GameObject itemPrefab;
    public string itemName;
}
