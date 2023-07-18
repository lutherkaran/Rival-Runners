
public class GameOverMenu : GameMenu
{

    public override void Open()
    {
        SetMenuActive(true);
    }

    public void Retry(int id)
    {
        if (id == 1)
        {
            LoadingData.SceneToLoad = GameMenuManager.Instance.GetLoadingMenu().GetCurrentScene();
            GameMenuManager.Instance.GetLoadingMenu().LoadSceneAsync(LoadingData.SceneToLoad);
        }
        else
        {
            LoadingData.SceneToLoad = "GameStart";
            GameMenuManager.Instance.GetLoadingMenu().LoadSceneAsync(LoadingData.SceneToLoad);
        }
    }

    public override void Close()
    {
        SetMenuActive(false);
    }

}
