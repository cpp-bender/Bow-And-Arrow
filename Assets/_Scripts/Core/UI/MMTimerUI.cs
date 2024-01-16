using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class MMTimerUI : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public TextMeshProUGUI timerText;
    public GameEvent MatchEnd;

    public float timerDamper = 0.05f;
    [Tooltip("In Seconds")] public float totalTime = 10f;

    [Header("DEBUG")]
    public bool timerOn;

    public void SetTimerText(float sec)
    {
        TimeSpan time = TimeSpan.FromMinutes(sec / 60f);

        string str = time.ToString(@"mm\:ss");

        timerText.text = str;
    }

    public void StartTimer()
    {
        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        timerOn = true;

        float elapsedTime = totalTime;

        while (elapsedTime > 0f)
        {
            elapsedTime -= Time.unscaledDeltaTime * timerDamper;

            SetTimerText(elapsedTime);

            yield return null;
        }

        timerOn = false;

        SetTimerText(0f);

        MatchEnd.Raise();

        MMUI.Instance.WhoWinTheGame();
    }
}
