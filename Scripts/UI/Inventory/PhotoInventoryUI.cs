using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoInventoryUI : MonoBehaviour
{ 
    [Header("Photo Inventory")]
    [SerializeField] private Transform layoutGroup;
    [SerializeField] private InventoryPhotoSlotUI inventoryPhotoSlotUITemplate;

    [Header("EnlargedPhoto")]
    [SerializeField] private GameObject enlargedPhotoParent;
    [SerializeField] private Image enlargedPhotoImage;
    [SerializeField] private Button closePhotoButton;

    private List<InventoryPhotoSlotUI> photoSlotImages;

    private void Awake()
    {
        photoSlotImages = new List<InventoryPhotoSlotUI>();
    }

    private void Start()
    {
        closePhotoButton.onClick.AddListener(() =>
        {
            enlargedPhotoParent.SetActive(false);
        });
    }

    private void OnEnable()
    {
        enlargedPhotoParent.SetActive(false);
    }

    public void AddPhoto(Sprite photoSprite)
    {
        InventoryPhotoSlotUI inventoryPhotoSlot = Instantiate(inventoryPhotoSlotUITemplate, layoutGroup);
        inventoryPhotoSlot.SetUp(photoSprite);
        inventoryPhotoSlot.OnItemClicked += InventoryPhotoSlot_OnItemClicked;
        photoSlotImages.Add(inventoryPhotoSlot);
    }

    public void RemovePhoto()
    {
        InventoryPhotoSlotUI inventoryPhotoSlot = photoSlotImages[photoSlotImages.Count - 1];
        inventoryPhotoSlot.OnItemClicked -= InventoryPhotoSlot_OnItemClicked;
        photoSlotImages.Remove(inventoryPhotoSlot);
        Destroy(inventoryPhotoSlot.gameObject);
    }

    private void InventoryPhotoSlot_OnItemClicked(Sprite photoSprite)
    {
        enlargedPhotoParent.SetActive(true);
        enlargedPhotoImage.sprite = photoSprite;
    }
}
