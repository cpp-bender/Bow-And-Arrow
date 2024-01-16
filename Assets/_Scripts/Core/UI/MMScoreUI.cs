using UnityEngine;
using TMPro;

public class MMScoreUI : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    public TextMeshProUGUI opponentText;

    public void SetPlayerScore(int score)
    {
        playerScoreText.text = score.ToString();
    }

    public void SetAIScore(int score)
    {
        aiScoreText.text = score.ToString();
    }

    public void SetOpponentName(string name)
    {
        opponentText.SetText(name);
    }
}
