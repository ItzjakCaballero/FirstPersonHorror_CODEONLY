using System;
using UnityEngine;

using EventHandler = System.EventHandler;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public event EventHandler OnInteractPerformed;
    public event EventHandler OnRightClickPerformed;
    public event EventHandler OnToggleFlashlightPerformed;
    public event EventHandler OnToggleCameraPerformed;
    public event EventHandler OnClickPerformed;
    public event EventHandler OnToggleInventoryPerformed;
    public event EventHandler OnCancelPerformed;

    [SerializeField] private GameObject player;

    private InputSystem_Actions playerInputActions;
    private bool isPlayerActionsEnabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple PlayerInput instances active");
        }

        playerInputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.UI.Enable();
        playerInputActions.Player.Interact.performed += PlayerInputActions_InteractPerformed;
        playerInputActions.Player.ToggleFlashlight.performed += PlayerInputActions_ToggleFlashlightPerformed;
        playerInputActions.UI.ToggleInventory.performed += PlayerInputActions_ToggleInventoryPerformed;
        playerInputActions.UI.Click.performed += PlayerInputActions_ClickPerformed;
        playerInputActions.UI.RightClick.performed += PlayerInputActions_RightClickPerformed;
        playerInputActions.UI.ToggleCamera.performed += PlayerInputActions_ToggleCameraPerformed;
        playerInputActions.UI.Cancel.performed += PlayerInputActions_CancelPerformed;
    }

    private void PlayerInputActions_CancelPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCancelPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInputActions_ToggleFlashlightPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnToggleFlashlightPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInputActions_ToggleCameraPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnToggleCameraPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInputActions_ClickPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnClickPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInputActions_RightClickPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRightClickPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInputActions_ToggleInventoryPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnToggleInventoryPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInputActions_InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMouseDelta()
    {
        Vector2 mouseDelta;
        mouseDelta = playerInputActions.Player.Look.ReadValue<Vector2>();
        return mouseDelta;
    }

    public int GetScrollWheelMovement()
    {
        int scrollValue = (int)playerInputActions.UI.ScrollWheel.ReadValue<Vector2>().y;
        return scrollValue;
    }

    public void DisablePlayerActions()
    {
        if (isPlayerActionsEnabled)
        {
            Opsive.Shared.Events.EventHandler.ExecuteEvent<bool>(player, "OnEnableGameplayInput", false);
            isPlayerActionsEnabled = false;
        }
    }

    public void EnablePlayerActions()
    {
        if (!isPlayerActionsEnabled)
        {
            Opsive.Shared.Events.EventHandler.ExecuteEvent<bool>(player, "OnEnableGameplayInput", true);
            isPlayerActionsEnabled = true;
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DisablePlayerActions();
        GameManager.Instance.PauseTime();
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EnablePlayerActions();
        GameManager.Instance.ResumeTime();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
