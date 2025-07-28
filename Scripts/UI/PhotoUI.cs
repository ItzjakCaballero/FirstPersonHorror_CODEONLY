using System;
using UnityEngine;
using UnityEngine.UI;

public class PhotoUI : MonoBehaviour
{
    public event Action<Sprite> OnKeepImage;

    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private Button keepImageButton;
    [SerializeField] private Button discardImageButton;

    private Sprite photoSprite;

    private void Start()
    {
        keepImageButton.onClick.AddListener(() =>
        {
            if (photoSprite != null)
            {
                OnKeepImage?.Invoke(photoSprite);
            }
            MenuManager.Instance.HideActiveMenu();
        });

        discardImageButton.onClick.AddListener(() =>
        {
            MenuManager.Instance.HideActiveMenu();
        });
    }

    public void UpdatePhotoSprite(Texture2D screenCapture)
    {
        photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100f);
        photoDisplayArea.sprite = photoSprite;
    }

    private void OnDisable()
    {
        photoSprite = null;
    }
}
