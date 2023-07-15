using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : GameMenu
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
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
