using UnityEngine;

public class InvestigateTheHouseTaskStep1 : TaskStep
{
    [SerializeField] private InventoryItemSO flashlightItem;
    [SerializeField] private InventoryItemSO houseKeyItem;

    public override void CheckToFinishTaskStep()
    {
        if(PlayerInventory.Instance.HasItem(flashlightItem) && PlayerInventory.Instance.HasItem(houseKeyItem))
        {
            FinishTaskStep();
        }
    }
}
