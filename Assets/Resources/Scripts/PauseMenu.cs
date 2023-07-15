using UnityEngine;

public class PauseMenu : GameMenu
{
    PauseMenu pauseMenu;

    public override void Open()
    {
        OnPaused();
    }

    public override void Close()
    {
        OnResumed();
    }

    public void PausePressed()
    {
        pauseMenu = GameMenuManager.Instance.GetPauseMenu();
        GameMenuManager.Instance.OpenMenu(pauseMenu);
    }

    public void Resume()
    {
        GameMenuManager.Instance.CloseCurrentMenu();
    }

    private void OnPaused()
    {
        SetMenuActive(true);
        GameMenuManager.Instance.pauseButton.SetActive(false);
        Time.timeScale = 0;

    }

    private void OnResumed()
    {
        SetMenuActive(false);
        GameMenuManager.Instance.pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void QuitPressed()
    {
        Application.Quit(0);
    }

    public void ReportPressed()
    {
        Debug.LogWarning("REPORTED");
    }

    public void SettingsPressed()
    {
        GameMenuManager.Instance.OpenMenu(GameMenuManager.Instance.GetSettingsMenu());
    }
}
