using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PreMMUI : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public Button trainingButton;
    public Button startMatchButton;
    public Image staminaPanel;
    public Image accuracyPanel;
    public GameObject youText;
    public TextMeshProUGUI enemyText;
    public PreMMData mmData;

    public void SwitchTrainingButton(bool on)
    {
        trainingButton.gameObject.SetActive(on);
    }

    public void SwitchStartMatchButton(bool on)
    {
        startMatchButton.gameObject.SetActive(on);
    }

    public void SwitchStaminaPanel(bool on)
    {
        staminaPanel.gameObject.SetActive(on);
    }

    public void SwitchAccuracyPanel(bool on)
    {
        accuracyPanel.gameObject.SetActive(on);
    }

    public void SwitchYouText(bool on)
    {
        youText.gameObject.SetActive(on);
    }

    public void SwitchEnemyText(bool on)
    {
        enemyText.gameObject.SetActive(on);
    }

    public void SetOpponentName(string name)
    {
        enemyText.SetText(name);
    }
}
