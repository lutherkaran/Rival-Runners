using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : NetworkBehaviour
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
        if (!IsHost && !IsOwner) yield return null;
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
