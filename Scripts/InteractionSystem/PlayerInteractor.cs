using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public static PlayerInteractor Instance { get; private set; }

    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private float rayLength = 5f;
    [SerializeField] private LayerMask raycastDetectionLayerMask;

    private InteractableItemDetails currentTarget;
    private Camera _camera;
    private bool canInteract;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple Player Interactor's active");
        }
    }

    private void Start()
    {
        if (!TryGetComponent<Camera>(out _camera))
        {
            Debug.LogError("Camera component not found on the GameObject.");
        }

        PlayerInput.Instance.OnInteractPerformed += PlayerInput_OnInteractPerformed;
    }

    private void PlayerInput_OnInteractPerformed(object sender, System.EventArgs e)
    {
        if (currentTarget != null)
        {
            if (currentTarget != null)
            {
                currentTarget.ShowDetails();
            }
            currentTarget.GetComponent<Interactable>().Interact(playerGameObject);
        }
    }

    private void Update()
    {
        if (!canInteract)
        {
            ClearText();
            return;
        }
        UpdateInteractions();
    }

    private void UpdateInteractions()
    {
        if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayLength, raycastDetectionLayerMask))
        {
            InteractableItemDetails readableItem = hit.collider.GetComponent<InteractableItemDetails>();
            if (readableItem != null)
            {
                currentTarget = readableItem;
                currentTarget.ShowObjectName(true);
                HighlightCrosshair(true);

            }
            else
            {
                ClearText();
            }
        }
        else
        {
            ClearText();
        }
    }

    private void ClearText()
    {
        if (currentTarget != null)
        {
            currentTarget.ShowObjectName(false);
            HighlightCrosshair(false);
            currentTarget = null;
        }
    }

    private void HighlightCrosshair(bool highlight)
    {
        TextInspectUIManager.Instance.HighlightCrosshair(highlight);
    }
}

