using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : GameMenu
{
    enum QualityName { LOW, BALANCED, HIGH, ULTRA_HIGH };
    QualityName quality;
    [SerializeField] RenderPipelineAsset[] QualityLevels;
    [SerializeField] Text text;

    private void Start()
    {
        quality = (QualityName)QualitySettings.GetQualityLevel();
        if (text)
        {
            text.text = (quality.ToString());

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

    public void ChangeQuality(int id)
    {
        quality = (QualityName)QualitySettings.GetQualityLevel();

        if (id == 1) { quality = quality + 1; }
        else if (id == -1) { quality = quality - 1; }

        Mathf.Clamp((int)quality, 0, 3);

        switch (quality)
        {
            case QualityName.LOW:
                text.text = (QualityName.LOW.ToString());
                break;

            case QualityName.BALANCED:
                text.text = (QualityName.BALANCED.ToString());
                break;

            case QualityName.HIGH:
                text.text = (QualityName.HIGH.ToString());
                break;

            case QualityName.ULTRA_HIGH:
                text.text = (QualityName.ULTRA_HIGH.ToString());
                break;

        }
        QualitySettings.SetQualityLevel((int)quality, true);
        QualitySettings.renderPipeline = QualityLevels[(int)quality];
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
