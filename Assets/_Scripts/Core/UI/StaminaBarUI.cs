using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class StaminaBarUI : SingletonMonoBehaviour<StaminaBarUI>
{
    [SerializeField] GameObject uiModel;
    [SerializeField] public Image fillImage;
    [SerializeField] float fillSpeed;
    [SerializeField] float unFillSpeed;
    [SerializeField] float holdUnfillSpeed = 0.0625f;

    [Header("Colors"), Space(2f)]
    [SerializeField] Color barStartColor;
    [SerializeField] Color barHighColor;
    [SerializeField] Color barMediumColor;
    [SerializeField] Color barLowColor;

    private ThrowDifficulty difficult;
    private bool isBarFillAmountNeedDecrease = false;
    private float newBarValue;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (FingerInput.Instance.IsFingerHold())
        {
            UnFillStaminaBar();
        }
        else if (!isBarFillAmountNeedDecrease)
        {
            FillStaminaBar();
        }

        if (isBarFillAmountNeedDecrease)
        {
            AfterThrowArrowUnFillStaminaBar();
        }

        StaminaBarColor();
    }

    #region Bar Fill
    private float Stamina() => PlayerController.Instance.GetStamina();

    private void CalculateNewStaminaBarFillAmountValue()
    {
        newBarValue = fillImage.fillAmount - (1f / Stamina());
        newBarValue = Mathf.Clamp(newBarValue, 0f, 1f);
    }

    private void UnFillStaminaBar()
    {
        fillImage.fillAmount -= Time.deltaTime * holdUnfillSpeed;
    }

    private void FillStaminaBar()
    {
        fillImage.fillAmount += Time.deltaTime * fillSpeed;
    }

    private void AfterThrowArrowUnFillStaminaBar()
    {
        if (newBarValue < fillImage.fillAmount)
        {
            fillImage.fillAmount -= Time.deltaTime * unFillSpeed;
        }
        else
        {
            isBarFillAmountNeedDecrease = false;
        }
    }

    private void StaminaBarColor()
    {
        Color mixedColor = Color.Lerp(barStartColor, barLowColor, 1f - fillImage.fillAmount);
        fillImage.color = mixedColor;

        /*if (fillImage.fillAmount < 0.25f)
        {
            fillImage.color = barLowColor;
        }
        else if (fillImage.fillAmount < 0.5f)
        {
            fillImage.color = barMediumColor;
        }
        else if (fillImage.fillAmount < 0.75f)
        {
            fillImage.color = barHighColor;
        }
        else
        {
            fillImage.color = barStartColor;
        }*/
    }
    #endregion

    public ThrowDifficulty GetThrowDifficult()
    {
        if (fillImage.fillAmount < 0.25f)
        {
            difficult = ThrowDifficulty.HARD;
        }
        else if (fillImage.fillAmount < 0.75f)
        {
            difficult = ThrowDifficulty.MEDIUM;
        }
        else
        {
            difficult = ThrowDifficulty.EASY;
        }

        return difficult;
    }

    public void OnThrow()
    {
        isBarFillAmountNeedDecrease = true;

        CalculateNewStaminaBarFillAmountValue();
    }
}
