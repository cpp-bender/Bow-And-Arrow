using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Button buyBtn;

    public void SetBuyButtonInteractable(bool value)
    {
        buyBtn.interactable = value;
    }

    public void SetData(UpgradeData data)
    {
        SetMoneyText(PriceConverter.Convert(data.Price));
        SetValueText(data.Value.ToString());
    }

    public void ReachMaxCount()
    {
        SetMoneyText("MAX");
    }

    private void SetMoneyText(string value)
    {
        moneyText.SetText(value);
    }

    private void SetValueText(string value)
    {
        valueText.SetText(value);
    }
}
