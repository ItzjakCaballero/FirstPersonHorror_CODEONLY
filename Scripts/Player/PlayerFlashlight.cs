using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItemSO flashlightItem;

    private bool isActive;
    private Light flashLight;

    private void Awake()
    {
        flashLight = GetComponent<Light>();
    }

    private void Start()
    {
        isActive = false;
        flashLight.enabled = false;
        PlayerInput.Instance.OnToggleFlashlightPerformed += PlayerInput_OnToggleFlashlightPerformed;
    }

    private void PlayerInput_OnToggleFlashlightPerformed(object sender, System.EventArgs e)
    {
        if (playerInventory.HasItem(flashlightItem))
        {
            isActive = !isActive;
            if (isActive)
            {
                flashLight.enabled = true;
            }
            else
            {
                flashLight.enabled = false;
            }
        }
    }
}
