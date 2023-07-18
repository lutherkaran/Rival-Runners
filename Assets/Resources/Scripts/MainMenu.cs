using UnityEngine;

public class MainMenu : GameMenu
{
    public void StartGame()
    {
        LoadingData.SceneToLoad = "Level";
        GameMenuManager.Instance.GetLoadingMenu().Open();
    }

    public void Multiplayer()
    {
        LoadingData.SceneToLoad = "Multiplayer";
        GameMenuManager.Instance.GetLoadingMenu().Open();
    }

    public void QuitPressed()
    {
        Application.Quit(0);
    }

    public void SettingsPressed()
    {
        GameMenuManager.Instance.OpenMenu(GameMenuManager.Instance.GetSettingsMenu());
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
