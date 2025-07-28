using UnityEngine;

public class InteractableNote : Interactable
{
    [SerializeField] private string textToDisplay;

    protected override void BaseInteract(GameObject player)
    {
        MenuManager.Instance.ShowMenu(GameMenus.Note);
        MenuManager.Instance.GetMenuObject<NoteUI>(GameMenus.Note).SetMessageText(textToDisplay);
    }
}
