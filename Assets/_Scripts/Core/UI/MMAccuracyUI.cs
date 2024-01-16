using UnityEngine;
using TMPro;

public class MMAccuracyUI : MonoBehaviour
{
    [Header("Lie Boost")]
    [SerializeField] int accuracyBoost = 10;
    [Header("DEPENDENCIES")]
    public TextMeshProUGUI playerAccuracyText;
    public TextMeshProUGUI enemyAccuracyText;


    public void SetPlayerAccuracyText(int accuracy)
    {
        playerAccuracyText.text = accuracy.ToString();
    }

    public void SetEnemyAccuracyText(int accuracy)
    {
        enemyAccuracyText.text = (accuracy + accuracyBoost).ToString();
    }
}
