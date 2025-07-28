using System.Collections.Generic;
using UnityEngine;

public class InvestigateTheHouseTaskStep2 : TaskStep
{
    private Dictionary<HouseRooms, bool> houseRoomToPictureTakenDictionary;

    private void Awake()
    {
        houseRoomToPictureTakenDictionary = new Dictionary<HouseRooms, bool>();
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        houseRoomToPictureTakenDictionary.Add(HouseRooms.LivingRoom, false);
        houseRoomToPictureTakenDictionary.Add(HouseRooms.DiningRoom, false);
        houseRoomToPictureTakenDictionary.Add(HouseRooms.Kitchen, false);
        houseRoomToPictureTakenDictionary.Add(HouseRooms.Library, false);
        houseRoomToPictureTakenDictionary.Add(HouseRooms.F1Bathroom, false);
    }

    private void Start()
    {
        MenuManager.Instance.GetMenuObject<PhotoUI>(GameMenus.Photo).OnKeepImage += PhotoUI_OnKeepImage;
    }

    private void PhotoUI_OnKeepImage(Sprite obj)
    {
        HouseRooms playerPosition = GameManager.Instance.GetPlayerRoomPosition();
        if(!houseRoomToPictureTakenDictionary.ContainsKey(playerPosition))
        {
            return;
        }

        if(!houseRoomToPictureTakenDictionary[playerPosition])
        {
            houseRoomToPictureTakenDictionary[playerPosition] = true;
            CheckToFinishTaskStep();
        }
    }

    public override void CheckToFinishTaskStep()
    {
        List<bool> takenPictureValues = new List<bool>(houseRoomToPictureTakenDictionary.Values);

        if(!takenPictureValues.Contains(false))
        {
            FinishTaskStep();
        }
    }
}
