using System.Collections;
using UnityEngine;

public class InteractableDoor : Interactable
{
    private const string DOOR_OPEN_TEXT = "Close door";
    private const string DOOR_CLOSED_TEXT = "Open door";

    [Header("Door Settings")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float rotationAmount = 90f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private bool isOpen = false;

    [Header("Lock Settings")]
    [SerializeField] private InventoryItemSO keyItem;
    [SerializeField] private string lockedCharacterDialogue;
    [SerializeField] private bool isLocked = false;

    private InteractableItemDetails textInspectItem;
    private Coroutine animationCoroutine;
    private Vector3 startRotation;
    private Vector3 forward;

    protected void Awake()
    {
        textInspectItem = GetComponent<InteractableItemDetails>();
        if (isOpen)
        {
            textInspectItem.SetObjectName(DOOR_OPEN_TEXT);
        }
        else
        {
            textInspectItem.SetObjectName(DOOR_CLOSED_TEXT);
        }

        startRotation = transform.rotation.eulerAngles;
        forward = transform.right;
    }

    protected override void BaseInteract(GameObject player)
    {
        ToggleDoor(player.GetComponent<PlayerInventory>());
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

        if (isOpen)
        {
            Close();
        }
        else
        {
            Open(playerInventory.transform.position);
        }
    }

    private void Open(Vector3 playerPosition)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        float dot = Vector3.Dot(forward, (playerPosition - transform.position).normalized);
        Debug.Log(dot);
        animationCoroutine = StartCoroutine(OpenCoroutine(dot));
    }

    private IEnumerator OpenCoroutine(float forwardAmount)
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion endRotation;

        if (forwardAmount >= 0)
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y - rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y + rotationAmount, 0));
        }

        isOpen = true;

        float lerpTime = 0;
        while (lerpTime < 1)
        {
            transform.rotation = Quaternion.Slerp(currentRotation, endRotation, lerpTime);
            yield return null;
            lerpTime += Time.deltaTime * rotationSpeed;
        }
    }

    private void Close()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(CloseCoroutine());
    }

    private IEnumerator CloseCoroutine()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation);

        isOpen = false;

        float lerpTime = 0;
        while (lerpTime < 1)
        {
            transform.rotation = Quaternion.Slerp(currentRotation, endRotation, lerpTime);
            yield return null;
            lerpTime += Time.deltaTime * rotationSpeed;
        }
    }
}
