using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectExamineUI : MonoBehaviour, IDragHandler
{
    private bool showingObject = false;

    private void OnDisable()
    {
        if (showingObject)
        {
            showingObject = false;
            UIObjectViewer.Instance.ClearObject();
        }
    }

    public void ShowObject(GameObject objectToShow)
    {
        if (!showingObject)
        {
            UIObjectViewer.Instance.DisplayObject(objectToShow);
            showingObject = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIObjectViewer.Instance.RotateObject(eventData.delta.y, eventData.delta.x);
    }
}
