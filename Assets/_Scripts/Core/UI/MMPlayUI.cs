using UnityEngine;
using TMPro;

public class MMPlayUI : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public MMTimerUI timerPanel;
    public MMScoreUI scorePanel;
    public TextMeshProUGUI mmPlayText;
    public Hand hand;

    public void SwitchScorePanel(bool on)
    {
        scorePanel.gameObject.SetActive(on);

        scorePanel.SetAIScore(0);

        scorePanel.SetPlayerScore(0);
    }

    public void SwitchTimerPanel(bool on)
    {
        timerPanel.gameObject.SetActive(on);
    }

    public void SwitchMMPlayText(bool on)
    {
        mmPlayText.gameObject.SetActive(on);
    }

    public void SwitchHand(bool on)
    {
        hand.gameObject.SetActive(on);
    }
}
