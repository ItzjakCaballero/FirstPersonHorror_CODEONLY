using UnityEngine;

public class InteractableDrawer : Interactable
{
    [SerializeField] private Vector3 openOffset = new Vector3();
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private bool isOpen = false;


    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isMoving = false;
    private float moveProgress = 0f;

    private void Start()
    {
        if (!isOpen)
        {
            closedPosition = transform.localPosition;
            openPosition = closedPosition + openOffset;
        }
        else
        {
            openPosition = transform.localPosition;
            closedPosition = openPosition - openOffset;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            moveProgress += Time.deltaTime * moveSpeed;
            float t = Mathf.Clamp01(moveProgress);

            Vector3 target = isOpen ? openPosition : closedPosition;
            transform.localPosition = Vector3.Lerp(
                isOpen ? closedPosition : openPosition,
                target,
                t
            );

            if (t >= 1f)
                isMoving = false;
        }
    }

    protected override void BaseInteract(GameObject player)
    {
        Toggle();
    }

    public void Toggle()
    {
        if (isMoving) 
            return;

        isOpen = !isOpen;
        moveProgress = 0f;
        isMoving = true;
    }
}
