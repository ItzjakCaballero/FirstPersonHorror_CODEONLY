using UnityEngine;

public enum HouseRooms
{
    LivingRoom,
    DiningRoom,
    Kitchen,
    Hallway,
    Library,
    F1Bathroom,
    F2Pod,
    GuestRoom,
    SmallRoom,
    SmallRoomBathroom,
    MasterRoom,
    MasterRoomBathroom,
    None
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private RoomColliderEvent[] roomColliders;
    
    private HouseRooms playerRoomPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple GameManager's active");
        }
        playerRoomPosition = HouseRooms.None;
    }

    private void Start()
    {
        TaskManager.Instance.StartTask("InvestigateTheHouse");
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time .timeScale = 1f;
    }

    public void SetPlayerRoomPosition(HouseRooms room)
    {
        if(room != playerRoomPosition)
        {
            playerRoomPosition = room;
            Debug.Log($"Player has moved to room: {room}");
        }
    }

    public HouseRooms GetPlayerRoomPosition()
    {
        return playerRoomPosition;
    }
}
