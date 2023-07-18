using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenu : GameMenu
{
    string currentScene;
    AsyncOperation asyncOperation;
    [SerializeField] UnityEngine.UI.Slider ProgressBar;

    public override void Close()
    {
        SetMenuActive(false);
    }

    public override void Open()
    {
        SetMenuActive(true);
        LoadScene();
    }
    public void LoadScene()
    {
        asyncOperation = LoadSceneAsync(LoadingData.SceneToLoad);
    }
    public void Update()
    {
        if (asyncOperation != null)
        {
            if (!asyncOperation.isDone)
            {
                ProgressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            }

        }
    }

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public AsyncOperation LoadSceneAsync(string sceneToLoad)
    {
        return SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
