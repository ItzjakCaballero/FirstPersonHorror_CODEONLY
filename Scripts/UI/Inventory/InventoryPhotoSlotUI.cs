using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPhotoSlotUI : MonoBehaviour
{
    public event Action<Sprite> OnItemClicked;

    [SerializeField] private Button button;
    [SerializeField] private Image image;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            OnItemClicked?.Invoke(image.sprite);
        });
    }

    public void SetUp(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
