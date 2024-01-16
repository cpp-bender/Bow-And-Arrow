using UnityEngine;
using TMPro;

public class MMStaminaUI : MonoBehaviour
{
    [Header("Lie Boost")]
    [SerializeField] float staminaBoost = 2f;
    [Header("DEPENDENCIES")]
    public TextMeshProUGUI playerStaminaText;
    public TextMeshProUGUI enemyStaminaText;

    public void SetPlayerStaminaText(float stamina)
    {
        playerStaminaText.text = stamina.ToString();
    }

    public void SetEnemyStaminaText(float stamina)
    {
        enemyStaminaText.text = (stamina + staminaBoost).ToString();
    }
}
