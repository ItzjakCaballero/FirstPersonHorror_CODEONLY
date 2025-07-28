using UnityEngine;

public class PhotoOnlyObject : MonoBehaviour
{
    private PlayerPhotoManager playerPhotoManager;

    private void Awake()
    {
        playerPhotoManager = PlayerInventory.Instance.gameObject.GetComponent<PlayerPhotoManager>();

        playerPhotoManager.OnShowPhotoObjects += PlayerPhotoManager_OnShowPhotoObjects;
        playerPhotoManager.OnHidePhotoObjects += PlayerPhotoManager_OnHidePhotoObjects;

        Hide();
    }

    private void PlayerPhotoManager_OnHidePhotoObjects(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void PlayerPhotoManager_OnShowPhotoObjects(object sender, System.EventArgs e)
    {
        Show();
    }

    private void OnDestroy()
    {
        playerPhotoManager.OnShowPhotoObjects -= PlayerPhotoManager_OnShowPhotoObjects;
        playerPhotoManager.OnHidePhotoObjects -= PlayerPhotoManager_OnHidePhotoObjects;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
