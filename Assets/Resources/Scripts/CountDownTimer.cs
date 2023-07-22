using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startTime = 3f;
    [SerializeField] Text countdownText;
    bool countdownOver = false;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {

        if (GameMenuManager.Instance.gameStart)
        {
            if (!countdownOver)
            {
                currentTime -= 1 * Time.deltaTime;
                countdownText.text = currentTime.ToString("0");

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    countdownOver = true;
                    countdownText.gameObject.SetActive(false);
                }
            }
        }

    }

    public float isCountDownOver()
    {
        return currentTime;
    }
}
