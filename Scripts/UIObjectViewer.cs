using UnityEngine;

public class UIObjectViewer : MonoBehaviour
{
    public static UIObjectViewer Instance { get; private set; }

    [SerializeField] private Transform parentTransform;
    [SerializeField] private Vector3 defaultParentPosition;
    [SerializeField, Range(.1f, 10f)] private float rotationSpeed;
    [SerializeField, Range(.05f, 5f)] private float scrollSpeed;

    private Transform currentObjectToRotate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple UIObjectViewer's active");
        }
            gameObject.SetActive(false);
    }

    public void DisplayObject(GameObject objectToDisplay)
    {
        gameObject.SetActive(true);
        parentTransform.localPosition = defaultParentPosition;
        currentObjectToRotate = Instantiate(objectToDisplay, parentTransform).transform;
    }

    public void RotateObject(float rotationX, float rotationY)
    {
        if (currentObjectToRotate != null)
        {
            currentObjectToRotate.eulerAngles += new Vector3(0f, -rotationY * rotationSpeed);
            parentTransform.eulerAngles += new Vector3(-rotationX * rotationSpeed, 0f);
        }
    }

    private void Update()
    {
        int scrollWheelValue = PlayerInput.Instance.GetScrollWheelMovement();

        parentTransform.localPosition += new Vector3(0f, 0f, scrollWheelValue * scrollSpeed);

        float minPos = .25f;
        float maxPos = 3.5f;
        parentTransform.localPosition = new Vector3(parentTransform.localPosition.x, parentTransform.localPosition.y, Mathf.Clamp(parentTransform.localPosition.z, minPos, maxPos));
    }

    public void ClearObject()
    {

        if (isActiveAndEnabled) //Unity error fix
        {
            if (currentObjectToRotate != null)
            {
                Destroy(currentObjectToRotate.gameObject);
                currentObjectToRotate = null;
            }
            parentTransform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }
}
