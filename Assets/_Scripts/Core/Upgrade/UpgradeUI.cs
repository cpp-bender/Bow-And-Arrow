using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class UpgradeUI : SingletonMonoBehaviour<UpgradeUI>
{
    [Header("Buttons")]
    [SerializeField] UpgradeButton incomeBuyBtn;
    [SerializeField] UpgradeButton staminaBuyBtn;
    [SerializeField] UpgradeButton accuracyBuyBtn;
    [Header("Datas"), Space(5f)]
    [SerializeField] List<UpgradeData> incomeDatas;
    [SerializeField] List<UpgradeData> staminaDatas;
    [SerializeField] List<UpgradeData> accuracyDatas;

    private Upgrade upgrade;

    private PlayerController Player() => PlayerController.Instance;
    private UpgradeData CurrentIncomeData() => incomeDatas[upgrade.IncomeIndex];
    private UpgradeData CurrentStaminaData() => staminaDatas[upgrade.StaminaIndex];
    private UpgradeData CurrentAccuracyaData() => accuracyDatas[upgrade.AccuracyIndex];

    private void Start()
    {
        incomeBuyBtn.SetData(incomeDatas[upgrade.IncomeIndex]);
        staminaBuyBtn.SetData(staminaDatas[upgrade.StaminaIndex]);
        accuracyBuyBtn.SetData(accuracyDatas[upgrade.AccuracyIndex]);
        CheckBuyButtonsClickable();
    }

    public void CheckBuyButtonsClickable()
    {
        //Check income buy button
        if (!IsPlayerHasEnoughMoney(CurrentIncomeData().Price) || IsIndexReachMaxCount(upgrade.IncomeIndex, incomeDatas.Count))
        {
            incomeBuyBtn.SetBuyButtonInteractable(false);

            if (IsIndexReachMaxCount(upgrade.IncomeIndex, incomeDatas.Count))
                incomeBuyBtn.ReachMaxCount();
        }
        else
        {
            incomeBuyBtn.SetBuyButtonInteractable(true);
        }

        //Check stamina buy button
        if (!IsPlayerHasEnoughMoney(CurrentStaminaData().Price) || IsIndexReachMaxCount(upgrade.StaminaIndex, staminaDatas.Count))
        {
            staminaBuyBtn.SetBuyButtonInteractable(false);

            if (IsIndexReachMaxCount(upgrade.StaminaIndex, staminaDatas.Count))
                staminaBuyBtn.ReachMaxCount();
        }
        else
        {
            staminaBuyBtn.SetBuyButtonInteractable(true);
        }

        //Check accuracy buy button
        if (!IsPlayerHasEnoughMoney(CurrentAccuracyaData().Price) || IsIndexReachMaxCount(upgrade.AccuracyIndex, accuracyDatas.Count))
        {
            accuracyBuyBtn.SetBuyButtonInteractable(false);

            if (IsIndexReachMaxCount(upgrade.AccuracyIndex, accuracyDatas.Count))
                accuracyBuyBtn.ReachMaxCount();
        }
        else
        {
            accuracyBuyBtn.SetBuyButtonInteractable(true);
        }
    }

    public void BuyIncome()
    {
        var buyPrice = CurrentIncomeData().Price;

        if (IsPlayerHasEnoughMoney(buyPrice))
        {
            Player().BuyUpgrade(buyPrice);

            if (!IsIndexReachMaxCount(upgrade.IncomeIndex, incomeDatas.Count))
            {
                upgrade.IncomeIndex++;

                Player().SetIncome((int)CurrentIncomeData().Value);
                incomeBuyBtn.SetData(CurrentIncomeData());
            }
        }

        SaveUpgradeIndexDatas();

        PlayerController.Instance.GainPrice(0);

        CheckBuyButtonsClickable();

        EffectManager.Instance.ShowLevelUpEffect();
    }

    public void BuyStamina()
    {
        var buyPrice = CurrentStaminaData().Price;

        if (IsPlayerHasEnoughMoney(buyPrice))
        {
            Player().BuyUpgrade(buyPrice);

            if (!IsIndexReachMaxCount(upgrade.StaminaIndex, staminaDatas.Count))
            {
                upgrade.StaminaIndex++;

                Player().SetStamina(CurrentStaminaData().Value);
                staminaBuyBtn.SetData(CurrentStaminaData());
            }
        }

        SaveUpgradeIndexDatas();

        PlayerController.Instance.GainPrice(0);

        CheckBuyButtonsClickable();

        EffectManager.Instance.ShowLevelUpEffect();
    }

    public void BuyAccuracy()
    {
        var buyPrice = CurrentAccuracyaData().Price;

        if (IsPlayerHasEnoughMoney(buyPrice))
        {
            Player().BuyUpgrade(buyPrice);

            if (!IsIndexReachMaxCount(upgrade.AccuracyIndex, accuracyDatas.Count))
            {
                upgrade.AccuracyIndex++;

                Player().SetAccuracy((int)CurrentAccuracyaData().Value);
                accuracyBuyBtn.SetData(CurrentAccuracyaData());
            }
        }

        SaveUpgradeIndexDatas();

        PlayerController.Instance.GainPrice(0);

        CheckBuyButtonsClickable();

        EffectManager.Instance.ShowLevelUpEffect();
    }

    private bool IsPlayerHasEnoughMoney(float buyPrice)
    {
        if (buyPrice < Player().GetMoney())
            return true;
        else
            return false;
    }

    private bool IsIndexReachMaxCount(int index, int maxIndex)
    {
        if (index < maxIndex - 1)
            return false;
        return true;
    }

    public void LoadUpgradeIndexDatas()
    {
        upgrade = SaveManager.Load(new Upgrade(), SaveFiles.UpgradeSaveFile);
    }

    public void SaveUpgradeIndexDatas()
    {
        SaveManager.Save(upgrade, SaveFiles.UpgradeSaveFile);
    }

    private void OnEnable()
    {
        LoadUpgradeIndexDatas();
    }
}
