using UnityEngine;

[RequireComponent(typeof(InteractableItemDetails))]
public abstract class Interactable : MonoBehaviour
{ 
    public void Interact(GameObject player)
    {
        BaseInteract(player);
    }

    protected virtual void BaseInteract(GameObject player)
    {

    }
}
