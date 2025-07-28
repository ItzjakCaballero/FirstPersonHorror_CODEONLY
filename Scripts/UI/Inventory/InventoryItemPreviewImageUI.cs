using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemPreviewImageUI : MonoBehaviour, IDragHandler
{
    public void ChangeItemPreview(InventoryItemSO item)
    {
        UIObjectViewer.Instance.ClearObject();
        UIObjectViewer.Instance.DisplayObject(item.itemPrefab);
    }

    private void OnDisable()
    {
        UIObjectViewer.Instance?.ClearObject();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIObjectViewer.Instance.RotateObject(eventData.delta.y, eventData.delta.x);
    }
}
