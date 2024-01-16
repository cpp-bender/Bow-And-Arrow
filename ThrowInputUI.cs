using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class ThrowInputUI : MonoBehaviour
{
    [Header("Temp Value")]
    [Range(0, 1f)]
    [SerializeField] float fireRatio;

    [Header("Objects"), Space(5f)]
    [SerializeField] GameObject uiModel;
    [SerializeField] Image fillImage;
    [SerializeField] RectTransform dynamicShotImage;

    [Header("Shot Points"), Space(5f)]
    [SerializeField] RectTransform fillPoint;
    [Header("Perfect Points"), Space(2f)]
    [SerializeField] RectTransform perfectShotPointCenter;
    [SerializeField] RectTransform perfectShotPointMin;
    [SerializeField] RectTransform perfectShotPointMax;
    [Header("Normal Points"), Space(2f)]
    [SerializeField] RectTransform normalShotPointMin;
    [SerializeField] RectTransform normalShotPointMax;

    private Tween fillTween;
    private float throwAccuracy;

    private const float PERFECTSHOTAREAACCURACYOFFSET = 1f;
    private const float NORMALSHOTAREAACCURACYOFFSET = 0.5f;
    private const float BADSHOTAREAACCURACYOFFSET = 0.25f;


    private void Update()
    {
        if (FingerInput.Instance.IsFingerDown())
        {
            InputSettings();
        }
        else if (FingerInput.Instance.IsFingerUp())
        {
            StartCoroutine(InPutSettingsReset(0.25f));
            PauseFillTween();

            //Bütün OnThrow'lar buradan çaðýrýlacak
            Bow.Instance.ThrowArrow(CalculateFireRatio());
            StaminaBarUI.Instance.OnThrow();
        }

        FillPointFollow();
        SetDynamicUIScale();
    }

    #region Dynamic UI Issues
    private void SetDynamicUIScale()
    {
        float scaleOffset = StaminaBarUI.Instance.GetThrowDifficult();
        

        /*if (dynamicShotImage.localScale)
        {
            dynamicShotImage.localScale = new Vector3(dynamicShotImage.localScale.x, 0f, dynamicShotImage.localScale.z);
        }*/


    }
    #endregion

    #region FireRatio
    private float CalculateFireRatio()
    {
        CalculateThrowAccuracy();

        float randomValue = Random.Range(0f, 100f);
        float reverseRandomValue = 100f - randomValue;

        if (randomValue < throwAccuracy) //Hit Shot
        {
            fireRatio = reverseRandomValue / 100f - (throwAccuracy / 100f) / 2f;
            fireRatio = Mathf.Clamp(fireRatio, 0f, 0.725f);
        }
        else //Miss Shot
        {
            fireRatio = 1f;
        }

        return fireRatio;
    }

    private void CalculateThrowAccuracy()
    {
        if (IsInputInPerfectArea()) //Perfect Shot Area
        {
            CalculateInputAccuracy(PERFECTSHOTAREAACCURACYOFFSET);
        }
        else if (IsInputInNormalArea()) //Normal Shot Area
        {
            CalculateInputAccuracy(NORMALSHOTAREAACCURACYOFFSET);
        }
        else //Bad Shot Area
        {
            CalculateInputAccuracy(BADSHOTAREAACCURACYOFFSET);
        }
    }

    private void CalculateInputAccuracy(float offset)
    {
        throwAccuracy = PlayerController.Instance.GetAccuracy() * offset;
    }

    private bool IsInputInPerfectArea()
    {
        if (fillPoint.position.y > perfectShotPointMin.position.y && fillPoint.position.y < perfectShotPointMax.position.y)
            return true;
        return false;
    }

    private bool IsInputInNormalArea()
    {
        if (fillPoint.position.y > normalShotPointMin.position.y && fillPoint.position.y < normalShotPointMax.position.y)
            return true;
        return false;
    }
    #endregion

    #region Settings
    private void InputSettings()
    {
        StopAllCoroutines();

        SetUIModelActive(true);
        KillOldFillTween();
        ImageFillEffect();
    }

    private IEnumerator InPutSettingsReset(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SetUIModelActive(false);
    }
    #endregion

    #region Fill Anim
    private void SetUIModelActive(bool value)
    {
        uiModel.SetActive(value);
    }

    private void FillPointFollow()
    {
        fillPoint.localPosition = -Vector3.up + Vector3.up * fillImage.fillAmount * 2f;
    }

    private void ImageFillEffect()
    {
        fillTween = DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, 1f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .Play();
    }

    private void KillOldFillTween()
    {
        fillImage.fillAmount = 0f;
        fillTween?.Kill();
    }

    private void PauseFillTween()
    {
        fillTween?.Pause();
    }
    #endregion
}
