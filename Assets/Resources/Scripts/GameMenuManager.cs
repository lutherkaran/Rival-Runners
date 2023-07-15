using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance { get; private set; }

    public GameObject pauseButton;

    [SerializeField] List<GameMenu> MenuList;

    PauseMenu pauseMenu;
    SettingsMenu settingsMenu;
    MainMenu mainMenu;

    GameMenu currentMenu;
    GameMenu previousMenu;

    public bool paused = false;

    private void Awake()
    {
        InitializeMenus();

        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

    }

    private void InitializeMenus()
    {
        foreach (var menu in MenuList)
        {
            menu.SetMenuActive(true); // Set the menu active

            if (menu is SettingsMenu)
            {
                settingsMenu = (SettingsMenu)menu;
            }
            else if (menu is PauseMenu)
            {
                pauseMenu = (PauseMenu)menu;
            }
            else if (menu is MainMenu)
            {
                mainMenu = (MainMenu)menu;
            }

            menu.SetMenuActive(false);
        }
    }
    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameStart")
        {
            OpenMenu(mainMenu);
        }
    }
    public void OpenMenu(GameMenu menu)
    {
        if (currentMenu != null)
        {
            previousMenu = currentMenu;
            currentMenu.Close();
        }
        currentMenu = menu;
        currentMenu.Open();
    }

    public void CloseCurrentMenu()
    {
        if (currentMenu != null)
        {
            previousMenu = currentMenu;
            currentMenu.Close();
            currentMenu = null;
        }
    }

    public PauseMenu GetPauseMenu()
    {
        return pauseMenu;
    }

    public SettingsMenu GetSettingsMenu()
    {
        return settingsMenu;
    }

    public MainMenu GetMainMenu()
    {
        return mainMenu;
    }
    public GameMenu GetPreviousMenu()
    {
        return previousMenu;
    }
}
