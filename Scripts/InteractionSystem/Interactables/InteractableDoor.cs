using UnityEngine;

public class InteractableDoor : Interactable
{
    private const string DOOR_OPEN_TEXT = "Close door";
    private const string DOOR_CLOSED_TEXT = "Open door";

    [Header("Door Settings")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float closeAngle = 0f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private bool isDoorOpen = false;

    [Header("Lock Settings")]
    [SerializeField] private InventoryItemSO keyItem;
    [SerializeField] private string lockedCharacterDialogue;
    [SerializeField] private bool isLocked = false;

    private InteractableItemDetails textInspectItem;
    private Quaternion targetRotation;
    private bool isAnimating = false;

    protected void Awake()
    {
        textInspectItem = GetComponent<InteractableItemDetails>();
        if (isDoorOpen)
        {
            textInspectItem.ChangeObjectName(DOOR_OPEN_TEXT);
        }
        else
        {
            textInspectItem.ChangeObjectName(DOOR_CLOSED_TEXT);
        }

        targetRotation = Quaternion.Euler(0f, closeAngle, 0f);
    }

    private void Update()
    {
        if (isAnimating)
        {
            doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(doorPivot.localRotation, targetRotation) < .1f)
            {
                doorPivot.localRotation = targetRotation;
                isAnimating = false;
            }
        }
    }

    protected virtual void ToggleDoor(PlayerInventory playerInventory)
    {
        if (isLocked)
        {
            if (playerInventory.HasItem(keyItem))
            {
                isLocked = false;
            }
            else
            {
                if (lockedCharacterDialogue != string.Empty)
                {
                    TextInspectUIManager.Instance.ShowPlayerDialogue(lockedCharacterDialogue);
                }
                return;
            }
        }

        if (!isAnimating)
        {
            isDoorOpen = !isDoorOpen;
            if (isDoorOpen)
            {
                textInspectItem.ChangeObjectName(DOOR_OPEN_TEXT);
            }
            else
            {
                textInspectItem.ChangeObjectName(DOOR_CLOSED_TEXT);
            }

            targetRotation = Quaternion.Euler(0f, isDoorOpen ? openAngle : closeAngle, 0f);
            isAnimating = true;
        }
    }

    protected override void BaseInteract(GameObject player)
    {
        ToggleDoor(player.GetComponent<PlayerInventory>());
    }
}
