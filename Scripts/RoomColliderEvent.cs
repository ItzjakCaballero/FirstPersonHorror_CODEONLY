using System;
using UnityEngine;

public class RoomColliderEvent : MonoBehaviour
{
    [SerializeField] private HouseRooms houseRoom;
    [SerializeField] private string colliderTag;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(colliderTag))
        {
            GameManager.Instance.SetPlayerRoomPosition(houseRoom);
        }
    }
}
