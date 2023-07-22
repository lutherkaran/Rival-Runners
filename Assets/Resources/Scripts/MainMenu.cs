using UnityEngine;

public class MainMenu : GameMenu
{
    public void StartGame()
    {
        LoadingData.SceneToLoad = "Level";
        GameMenuManager.Instance.loadingMenu.Open();
    }

    public void Multiplayer()
    {
        LoadingData.SceneToLoad = "Multiplayer";
        GameMenuManager.Instance.loadingMenu.Open();
    }

    public void QuitPressed()
    {
        Application.Quit(0);
    }

    public void SettingsPressed()
    {
        GameMenuManager.Instance.OpenMenu(GameMenuManager.Instance.settingsMenu);
    }

    public override void Close()
    {
        SetMenuActive(false);
    }

    public override void Open()
    {
        SetMenuActive(true);
    }
}
