using UnityEngine;

public class ExaminableObject : Interactable
{
    [SerializeField] private GameObject displayedObjectPrefab;

    protected override void BaseInteract(GameObject player)
    {
        MenuManager.Instance.ShowMenu(GameMenus.ObjectExamine);
        MenuManager.Instance.GetMenuObject<ObjectExamineUI>(GameMenus.ObjectExamine).ShowObject(displayedObjectPrefab);
    }
}
