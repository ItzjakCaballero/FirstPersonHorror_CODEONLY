using UnityEngine;
using Sirenix.OdinInspector;

public class InteractableItemDetails : MonoBehaviour
{
    [Header("Show Config")]
    [SerializeField] private bool showObjectName;
    [SerializeField] private bool showPickupDialogue;

    [Header("Text")]
    [ShowIf("showObjectName")][SerializeField] private string objectName;
    [ShowIf("showPickupDialogue")][TextArea] [SerializeField] private string pickupDialogue;

    public void ChangeObjectName(string name)
    {
        objectName = name;
    }

    public void ShowObjectName(bool showName)
    {
        if (showObjectName)
        {
            TextInspectUIManager.Instance.ShowName(objectName, showName);
        }
    }

    public void ShowDetails()
    {
        if (showPickupDialogue)
        {
            TextInspectUIManager.Instance.ShowPlayerDialogue(pickupDialogue);
        }
    }
}
