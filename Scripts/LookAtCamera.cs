using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [Header("Rotation Offsets")]
    [SerializeField] private float XRotationOffset;
    [SerializeField] private float YRotationOffset;
    [SerializeField] private float ZRotationOffset;

    private void Start()
    {
        if(cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(cameraTransform);
        transform.Rotate(new Vector3(XRotationOffset, YRotationOffset, ZRotationOffset));
    }
}
