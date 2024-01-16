using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PriceGainEffect : MonoBehaviour
{
    [Header("Look At Camera")]
    [SerializeField] bool isEffectLookAtCamera = false;
    [Header("Animation Colors")]
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [Header("Animation Values")]
    [SerializeField] float yPosition;
    [SerializeField] float initYPosition;
    [SerializeField] float animationDuration;
    [Header("Dependies")]
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI comboWordText;

    private void Start()
    {
        if (isEffectLookAtCamera)
        {
            Vector3 cameraPos = HyperCamera.Instance.transform.position;

            var lookPos = cameraPos - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            transform.rotation = rotation;
            transform.eulerAngles += new Vector3(0f, 180f, 0f);
        }

        transform.position += Vector3.up * initYPosition;

        SetStartColor();
        Animation();
    }

    private void SetStartColor()
    {
        text.color = startColor;
        comboWordText.color = startColor;
    }

    private void Animation()
    {
        transform.DOMoveY(transform.position.y + yPosition, animationDuration).Play();

        text.DOColor(endColor, animationDuration * 0.25f).SetEase(Ease.InQuad).SetDelay(animationDuration * 0.75f).Play();
        comboWordText.DOColor(endColor, animationDuration * 0.25f).SetEase(Ease.InQuad).SetDelay(animationDuration * 0.75f).Play();
    }

    public void SetPrice(float count)
    {
        text.SetText("$" + count.ToString());
    }

    public void SetPrice(int count)
    {
        text.SetText("$" + count.ToString());
    }

    public void SetPoint(int point)
    {
        text.SetText("+" + point.ToString());
    }

    public void SetCombo(int comboCount, string comboWord, string hitWord)
    {
        if (comboCount == 0)
        {
            text.SetText("");
            comboWordText.SetText(hitWord);
        }
        else
        {
            text.SetText("X" + comboCount);
            comboWordText.SetText(comboWord);
        }
    }

    public void SetMissText()
    {
        text.SetText("Miss");
    }

    public void SetHitYellowText()
    {
        text.SetText("Bullseye");
    }

    public void SetHitRedText()
    {
        text.SetText("Perfect");
    }

    public void SetHitBlueText()
    {
        text.SetText("Amazing");
    }

    public void SetScale(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }
}