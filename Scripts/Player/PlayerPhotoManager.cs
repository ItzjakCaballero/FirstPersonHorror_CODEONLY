 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhotoManager : MonoBehaviour
{
    private const int MAX_PHOTO_COUNT = 12;

    public event EventHandler OnShowPhotoObjects;
    public event EventHandler OnHidePhotoObjects;

    private List<Sprite> photosList;
    private Texture2D screenCapture;
    private PhotoUI photoUI;
    private bool isCameraOpen = false;

    private void Awake()
    {
        photosList = new List<Sprite>();
    }

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photoUI = MenuManager.Instance.GetMenuObject<PhotoUI>(GameMenus.Photo);

        PlayerInput.Instance.OnClickPerformed += PlayerInput_OnClickPerformed;
        PlayerInput.Instance.OnToggleCameraPerformed += PlayerInput_OnToggleCameraPerformed;
        photoUI.OnKeepImage += PhotoUI_OnKeepImage;
    }

    private void PhotoUI_OnKeepImage(Sprite photo)
    {
        AddPhoto(photo);
    }

    private void PlayerInput_OnToggleCameraPerformed(object sender, System.EventArgs e)
    {
        CloseOpenCamera();
    }

    private void PlayerInput_OnClickPerformed(object sender, System.EventArgs e)
    {
        if (isCameraOpen)
        {
            StartCoroutine(CaptureScreenchot());
        }
    }

    private IEnumerator CaptureScreenchot()
    {
        OnShowPhotoObjects?.Invoke(this, EventArgs.Empty);

        isCameraOpen = false;
        MenuManager.Instance.HideActiveMenu();
        MenuManager.Instance.DisableCanvas();

        yield return new WaitForEndOfFrame();
        Rect region = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(region, 0, 0, false);
        screenCapture.Apply();
        photoUI.UpdatePhotoSprite(screenCapture);

        MenuManager.Instance.EnableCanvas();
        MenuManager.Instance.ShowMenu(GameMenus.Photo);

        OnHidePhotoObjects?.Invoke(this, EventArgs.Empty);
    }

    public void AddPhoto(Sprite photo)
    {
        InventoryUI inventory = MenuManager.Instance.GetMenuObject<InventoryUI>(GameMenus.Inventory);
        if (photosList.Count <= MAX_PHOTO_COUNT)
        {
            photosList.Add(photo);
        }
        else
        {
            while (photosList.Count > MAX_PHOTO_COUNT)
            {
                photosList.RemoveAt(MAX_PHOTO_COUNT);
                inventory.RemovePhoto();
            }
            photosList.RemoveAt(MAX_PHOTO_COUNT - 1);
            inventory.RemovePhoto();

            photosList.Add(photo);
        }

        inventory.AddPhoto(photo);
    }

    private void CloseOpenCamera()
    {
        if (MenuManager.Instance.GetActiveMenu() == GameMenus.None)
        {
            isCameraOpen = !isCameraOpen;
            if (isCameraOpen)
            {
                MenuManager.Instance.ShowMenu(GameMenus.Camera);
            }
        }
        else if (MenuManager.Instance.GetActiveMenu() == GameMenus.Camera)
        {
            isCameraOpen = !isCameraOpen;
            MenuManager.Instance.HideActiveMenu();
        }
    }
}
