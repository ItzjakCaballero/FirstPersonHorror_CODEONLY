using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum GameMenus
{
    None,
    Inventory,
    Photo,
    Camera,
    ObjectExamine,
    Note
}

public class MenuManager : SerializedMonoBehaviour
{
    [Serializable]
    private struct MenuData
    {
        public GameObject menu;
        public bool showCursor;
        public bool pauseGame;
    }

    public static MenuManager Instance { get; private set; }

    public event Action<GameMenus> OnMenuOpen;
    public bool isMenusActive { get; private set; }

    [SerializeField] private Dictionary<GameMenus, MenuData> menuDictionary = new Dictionary<GameMenus, MenuData>();

    private GameMenus activeMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple CanvasUI's active");
        }

        activeMenu = GameMenus.None;
        isMenusActive = true;
    }

    private void Start()
    {
        ResetMenus();
        PlayerInput.Instance.OnCancelPerformed += PlayerInput_OnCancelPerformed;
    }

    private void PlayerInput_OnCancelPerformed(object sender, EventArgs e)
    {
        if (activeMenu != GameMenus.None)
        {
            HideActiveMenu();
        }
        else
        {
            //Open pause menu
        }
    }

    private void ResetMenus()
    {
        foreach (MenuData menuData in menuDictionary.Values)
        {
            menuData.menu.SetActive(false);
        }
    }

    public T GetMenuObject<T>(GameMenus menuType) where T : Component
    {
        if (menuDictionary.TryGetValue(menuType, out MenuData menuData))
        {
            return menuData.menu.GetComponent<T>();
        }
        return null;
    }

    public void ShowMenu(GameMenus menu)
    {
        if (activeMenu == GameMenus.None && menuDictionary.TryGetValue(menu, out MenuData menuData))
        {
            if (menuData.pauseGame)
            {
                PlayerInput.Instance.PauseGame();
            }
            if (menuData.showCursor)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            menuData.menu.SetActive(true);
            OnMenuOpen?.Invoke(menu);
            activeMenu = menu;
        }
    }

    public void HideActiveMenu()
    {
        if (activeMenu != GameMenus.None && menuDictionary.TryGetValue(activeMenu, out MenuData menuData))
        {
            if (menuData.pauseGame)
            {
                PlayerInput.Instance.ResumeGame();
            }
            if (menuData.showCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            menuData.menu.SetActive(false);
            activeMenu = GameMenus.None;
        }
    }

    public GameMenus GetActiveMenu()
    {
        return activeMenu;
    }

    public void DisableCanvas()
    {
        isMenusActive = false;
        gameObject.SetActive(false);
    }

    public void EnableCanvas()
    {
        gameObject.SetActive(true);
        isMenusActive = true;
    }
}
