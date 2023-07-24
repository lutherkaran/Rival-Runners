using System.Collections;
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
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        while (currentTime > 0f)
        {
            if (GameMenuManager.Instance.playerStarted && PlayerController.Instance.playerStarted && !countdownOver)
            {
                currentTime -= Time.deltaTime;
                countdownText.text = currentTime.ToString("0");

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    countdownOver = true;
                    countdownText.gameObject.SetActive(false);
                }
            }

            yield return null;
        }
    }

    public float GetRemainingTime()
    {
        return currentTime;
    }
}
