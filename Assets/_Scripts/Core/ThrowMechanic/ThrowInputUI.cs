using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class ThrowInputUI : SingletonMonoBehaviour<ThrowInputUI>
{
    [Header("Input Accuracy Impacts")]
    [SerializeField] float perfectShootMatchMode = 1.25f;
    [SerializeField] float perfectShoot = 1f;
    [SerializeField] float normalShoot = 0.5f;
    [SerializeField] float badShoot = 0.33f;

    [Header("Objects"), Space(5f)]
    [SerializeField] GameObject uiModel;
    [SerializeField] Image fillImage;
    [SerializeField] Image perfectShotImage;
    [SerializeField] RectTransform dynamicShotImage;

    [Header("Shot Points"), Space(5f)]
    [SerializeField] Transform fillPoint;

    [Header("Perfect Points"), Space(2f)]
    [SerializeField] RectTransform perfectShotPointCenter;
    [SerializeField] RectTransform perfectShotPointMin;
    [SerializeField] RectTransform perfectShotPointMax;

    [Header("Normal Points"), Space(2f)]
    [SerializeField] RectTransform normalShotPointMin;
    [SerializeField] RectTransform normalShotPointMax;

    private Tween fillImageFillTween;
    private Tween perfectShotImageColorTween;
    private Tween perfectShotImageFillTween;
    private Tween perfectShotImageScaleTween;
    private float throwAccuracy;
    private float fireRatio;

    private const float DYNAMICUI_Y_MAX_POS = 0.75f;
    private const float DYNAMICUI_Y_MIN_POS = 0.25f;

    private float PlayerAccuracy() => PlayerController.Instance.GetAccuracy();
    private bool IsFingerDown() => FingerInput.Instance.IsFingerDown();
    private bool IsFingerUp() => FingerInput.Instance.IsFingerUp();
    private bool IsFingerHold() => FingerInput.Instance.IsFingerHold();
    private ThrowDifficulty Difficult() => StaminaBarUI.Instance.GetThrowDifficult();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (IsFingerDown())
        {
            InputSettings();
        }

        if (IsFingerUp())
        {
            FillPointFollow();

            Taptic.Light();
            
            PerfectShotEffect();

            StartCoroutine(InPutSettingsReset(0.1f));
            PauseFillTween();

            PlayerController.Instance.PlayThrowAnimation(CalculateFireRatio());

            StaminaBarUI.Instance.OnThrow();
        }

        ChangeScale();

        FillPointFollow();

        HandlePlayerHotness();
    }

    private void HandlePlayerHotness()
    {
        transform.parent.GetComponent<PlayerController>().Roast();
    }

    public float CalculateFireRatio()
    {
        var hitChance = Random.Range(0f, 100f);
        
        if (IsShotPerfect()) // Perfect Shot Area (Green)
        {
            throwAccuracy = PlayerAccuracy() * perfectShoot; // 50f

            if (hitChance < throwAccuracy)
            {
                fireRatio = Random.value * 0.2f;
            }
            else
            {
                fireRatio = Random.Range(0f, 0.9f);
            }
        }
        else if (IsShotNormal()) // Normal Shot Area (Yellow)
        {
            throwAccuracy = PlayerAccuracy() * normalShoot; // 25f
            
            if (hitChance < throwAccuracy)
            {
                fireRatio = Random.value;
            }
            else
            {
                fireRatio = 1f;
            }
        }
        else // Bad Shot Area (Gray)
        {
            throwAccuracy = PlayerAccuracy() * badShoot; // 12.5f
            
            if (hitChance < throwAccuracy)
            {
                fireRatio = Random.value;
            }
            else
            {
                fireRatio = 1f;
            }
        }
        
        fireRatio = Mathf.Clamp(fireRatio, 0f, 1f);
        
        return fireRatio;
    }

    public void ResetSlider()
    {
        fillImage.fillAmount = 0f;
        uiModel.SetActive(false);
    }

    public void ChangeDynamicImageYPos()
    {
        dynamicShotImage.localPosition =
            new Vector3(dynamicShotImage.localPosition.x,
            Random.Range(DYNAMICUI_Y_MIN_POS, DYNAMICUI_Y_MAX_POS),
            dynamicShotImage.localPosition.z);
    }

    private void ChangeScale()
    {

        if (Difficult() == ThrowDifficulty.HARD && dynamicShotImage.localScale.y > 0.25f)
        {
            dynamicShotImage.localScale -= new Vector3(0f, Time.deltaTime, 0f);
        }
        else if (Difficult() == ThrowDifficulty.EASY || Difficult() == ThrowDifficulty.MEDIUM)
        {
            dynamicShotImage.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    #region Settings
    private void InputSettings()
    {
        StopAllCoroutines();

        SetUIModelActive(true);
        KillOldFillTween();
        ImageFillEffect();
        AdjustThrowUIScreenPosition();
    }

    private IEnumerator InPutSettingsReset(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SetUIModelActive(false);
    }

    private void AdjustThrowUIScreenPosition()
    {
        WorldToScreen.Instance.AdjustThrowUIScreenPosition();
    }
    #endregion

    #region Visual Issues
    private void FillPointFollow()
    {
        fillPoint.localPosition = Vector3.up * ((fillImage.fillAmount * 2f) - 1f);
    }

    private void SetUIModelActive(bool value)
    {
        uiModel.SetActive(value);
    }

    private void ImageFillEffect()
    {
        float duration = 1f;

        if (StaminaBarUI.Instance.GetThrowDifficult() == ThrowDifficulty.HARD)
        {
            duration = 2f;
        }
        else if (StaminaBarUI.Instance.GetThrowDifficult() == ThrowDifficulty.MEDIUM)
        {
            duration = 1.5f;
        }
        
        fillImageFillTween = DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, 1f, duration)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear)
            .Play();
    }

    private void KillOldFillTween()
    {
        fillImage.fillAmount = 0f;
        fillImageFillTween?.Kill();
    }

    private void PauseFillTween()
    {
        fillImageFillTween?.Pause();
    }

    private void PerfectShotEffect()
    {
        if (IsShotPerfect() && perfectShotImageScaleTween == null && perfectShotImageColorTween == null && perfectShotImageFillTween == null)
        {
            var startColor = perfectShotImage.color;
            var alphaZeroColor = new Color(startColor.r, startColor.g, startColor.b, 0.5f);
            var startScale = perfectShotImage.transform.localScale;

            perfectShotImage.fillAmount = fillImage.fillAmount;

            perfectShotImageScaleTween = perfectShotImage.transform.DOScale(startScale * 1.1f, 0.25f)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => { perfectShotImage.transform.localScale = startScale; perfectShotImageScaleTween = null; });

            perfectShotImageColorTween = perfectShotImage.DOColor(alphaZeroColor, 0.25f)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => { perfectShotImage.fillAmount = 0f; perfectShotImage.color = startColor; perfectShotImageColorTween = null; });

            perfectShotImageFillTween = DOTween.To(() => perfectShotImage.fillAmount, x => perfectShotImage.fillAmount = x, 1f, 0.25f)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => { perfectShotImage.fillAmount = 0f; perfectShotImageFillTween = null; });
        }
    }

    private bool IsShotPerfect()
    {
        //Perfect Shot Area
        if (fillPoint.position.y > perfectShotPointMin.position.y && fillPoint.position.y < perfectShotPointMax.position.y)
        {
            return true;
        }
        return false;
    }

    private bool IsShotNormal()
    {
        //Normal Shot Area
        if (fillPoint.position.y > normalShotPointMin.position.y && fillPoint.position.y < normalShotPointMax.position.y)
        {
            return true;
        }
        return false;
    }

    #endregion
}
