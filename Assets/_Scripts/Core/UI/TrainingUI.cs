using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingUI : SingletonMonoBehaviour<TrainingUI>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI trainingText;
    [SerializeField] Image trainingProgressBar;

    public void SetMoneyText(float money)
    {
        moneyText.SetText(PriceConverter.Convert(money));
    }

    public void SetTrainingProgress(int currentHitCount, int stageSkipHitCount)
    {
        SetTrainingText(currentHitCount, stageSkipHitCount);
        SetProgressBarFillAmount(CalculateProgressBarFillAmount(currentHitCount, stageSkipHitCount));
    }

    private void SetTrainingText(int currentHitCount, int stageSkipHitCount)
    {
        trainingText.SetText(currentHitCount + "/" + stageSkipHitCount);
    }

    private void SetProgressBarFillAmount(float fillAmount)
    {
        trainingProgressBar.fillAmount = fillAmount;
    }

    private float CalculateProgressBarFillAmount(int currentHitCount, int stageSkipHitCount)
    {
        return (float)currentHitCount / (float)stageSkipHitCount;
    }
}
