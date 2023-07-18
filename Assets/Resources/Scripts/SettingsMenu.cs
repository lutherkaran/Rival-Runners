using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : GameMenu
{
    enum QualityOption { LOW, BALANCED, HIGH, ULTRA_HIGH }
    QualityOption quality;

    enum FPSOption { FPS_30, FPS_60, FPS_90, FPS_120 }
    FPSOption FPS;

    [SerializeField] RenderPipelineAsset[] QualityLevels;
    [SerializeField] Text qualityText;
    [SerializeField] Text fpsText;

    private void Start()
    {
        if (fpsText)
        {
            FPS = (FPSOption)Application.targetFrameRate;
            fpsText.text = FPS.ToString();
        }
        quality = (QualityOption)QualitySettings.GetQualityLevel();
        if (qualityText)
        {
            qualityText.text = (quality.ToString());

            // ToDo Need to set this as last saved;
            //QualitySettings.SetQualityLevel((int)quality, true);
            //QualitySettings.renderPipeline = QualityLevels[(int)quality];
        }
    }

    public override void Open()
    {
        Time.timeScale = 0;
        SetMenuActive(true);
    }

    public void ChangeFPS(int id)
    {
        int currentFPS = Application.targetFrameRate;

        currentFPS += id * 30; // Increment or decrement by 30 FPS

        currentFPS = Mathf.Clamp(currentFPS, 30, 120); // Clamp the FPS value between 30 and 120

        Application.targetFrameRate = currentFPS;

        if (fpsText != null)
        {
            FPSOption currentOption = (FPSOption)currentFPS;
            fpsText.text = currentOption.ToString();
        }
    }

    public void ChangeQuality(int id)
    {
        QualityOption quality = (QualityOption)QualitySettings.GetQualityLevel();

        quality += id;
        quality = (QualityOption)Mathf.Clamp((int)quality, 0, 3);

        switch (quality)
        {
            case QualityOption.LOW:
                qualityText.text = QualityOption.LOW.ToString();
                break;

            case QualityOption.BALANCED:
                qualityText.text = QualityOption.BALANCED.ToString();
                break;

            case QualityOption.HIGH:
                qualityText.text = QualityOption.HIGH.ToString();
                break;

            case QualityOption.ULTRA_HIGH:
                qualityText.text = QualityOption.ULTRA_HIGH.ToString();
                break;
        }

        if (QualityLevels.Length > (int)quality && QualityLevels[(int)quality] != null)
        {
            QualitySettings.SetQualityLevel((int)quality, true);
            QualitySettings.renderPipeline = QualityLevels[(int)quality];
        }
    }

    public void BackPressed()
    {
        GameMenu currentMenu = GameMenuManager.Instance.GetPreviousMenu();

        if (currentMenu == GameMenuManager.Instance.GetPauseMenu())
        {
            GameMenuManager.Instance.OpenMenu(GameMenuManager.Instance.GetPauseMenu());
        }
        else if (currentMenu == GameMenuManager.Instance.GetMainMenu())
        {
            GameMenuManager.Instance.OpenMenu(GameMenuManager.Instance.GetMainMenu());
            Time.timeScale = 1;
        }
    }

    public void FeedbackPressed()
    {
        Debug.LogWarning("FEEDBACK");
    }

    public void TermsPrivacyPressed()
    {
        Debug.LogWarning("TERMS & CONDITIONS");
    }

    public void CreditsPressed()
    {
        Debug.LogWarning("CREDITS");
    }

    public override void Close()
    {
        SetMenuActive(false);
    }
}
