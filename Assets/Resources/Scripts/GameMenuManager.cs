using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance { get; private set; }
    public bool gameStart = false;

    public GameObject pauseButton;

    [SerializeField] List<GameMenu> MenuList;

    public PauseMenu pauseMenu { get; private set; }
    public SettingsMenu settingsMenu { get; private set; }
    public MainMenu mainMenu { get; private set; }
    public GameOverMenu gameOverMenu { get; private set; }
    public LoadingMenu loadingMenu { get; private set; }

    public GameMenu currentMenu { get; private set; }
    public GameMenu previousMenu { get; private set; }
    public CountDownTimer timer { get; private set; }

    public bool paused = false;

    bool playerAlive = true;

    private void OnEnable()
    {
        PlayerController.OnDied += IsPlayerDied;
    }

    public void IsPlayerDied()
    {
        playerAlive = false;
    }

    private void Awake()
    {
        Initialize();

        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Initialize()
    {
        timer = GetComponentInChildren<CountDownTimer>();

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
            else if (menu is GameOverMenu)
            {
                gameOverMenu = (GameOverMenu)menu;
            }
            else if (menu is LoadingMenu)
            {
                loadingMenu = (LoadingMenu)menu;
            }

            menu.SetMenuActive(false);
        }
    }

    public void Start()
    {
        if (loadingMenu.GetCurrentScene() == "GameStart")
        {
            OpenMenu(mainMenu);
        }
    }

    public void Update()
    {
        if (!playerAlive)
        {
            //gameOverMenu.Open();
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

    public bool PlayerAlive()
    {
        return playerAlive;
    }
}
