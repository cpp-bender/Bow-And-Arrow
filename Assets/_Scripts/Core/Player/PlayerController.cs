using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using SimpleJSON;

public class PlayerController : CharacterControl, IPriceGainer
{
    #region Singleton
    public static PlayerController Instance { get => instance; }
    private static PlayerController instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("DEPENDENCIES")]
    [SerializeField] ThrowInputUI inputUI;
    [Header("Tired Effect")]
    [SerializeField] SkinnedMeshRenderer[] skinRenderers;
    [SerializeField] List<GameObject> tiredEffects;
    [SerializeField] EnvironmentManager environment;
    [SerializeField] OutfitDrawer outfitDrawer;

    [SerializeField] List<TrainingSkipHitData> hitCountDatas;

    [Header("DEBUG")]
    [SerializeField] PlayerData playerData;

    [Header("OVERWRITE")]
    [SerializeField] PreMMData preMMData;

    private int prevShootAngleIndex;
    private bool isInMatchMode = false;
    private Color baseSkinColor;
    private float roastTimer;

    public int PrevShootAngleIndex { get => prevShootAngleIndex; }
    public bool IsInMatchMode { get => isInMatchMode; }

    private int CurrentTrainingSkipHitCount() => hitCountDatas[playerData.hitCountIndex].HitCount;

    private void Start()
    {
        SetMoneyText();

        SetTrainingText();

        outfitDrawer.ChangeCombine(0);

        transform.position = environment.currentEnv.environmentData.PlayerTransforms[1].position;
        transform.rotation = Quaternion.Euler(environment.currentEnv.environmentData.PlayerTransforms[1].rotation);

        baseSkinColor = skinRenderers[0].material.color;
    }

    public override IEnumerator Throw(float fireRatio, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (isInMatchMode)
        {
            bow.Throw(fireRatio, this as IPointGainer);
        }
        else
        {
            bow.Throw(fireRatio, this as IPriceGainer);
        }
    }

    public void Roast()
    {
        float fillAmount = StaminaBarUI.Instance.fillImage.fillAmount;

        if (fillAmount < 0.25f)
        {
            roastTimer = (0.25f - fillAmount) * 4f;

            foreach (var tiredEffectGO in tiredEffects)
            {
                tiredEffectGO.SetActive(true);
            }
        }
        else
        {
            roastTimer = 0f;

            foreach (var tiredEffectGO in tiredEffects)
            {
                tiredEffectGO.SetActive(false);
            }
        }

        roastTimer = Mathf.Clamp01(roastTimer);

        skinRenderers[0].material.color = Color.Lerp(baseSkinColor, Color.red, roastTimer);
        skinRenderers[1].material.color = Color.Lerp(baseSkinColor, Color.red, roastTimer);
    }

    public void SwitchModeToMM()
    {
        currentScore = 0;
        isInMatchMode = true;
        outfitDrawer.ChangeCombine(1);
    }

    public void SwitchModeToTraining()
    {
        isInMatchMode = false;
        outfitDrawer.ChangeCombine(0);
    }

    public void GainPrice(float pricePercent)
    {
        SetMoneyToPlayerData(GetGainPriceValue(pricePercent));
        SetMoneyText();

        IncreaseHitCount();
        SetHtCountToPlayerData();
        SetTrainingText();

        UpgradeUI.Instance.CheckBuyButtonsClickable();

        SavePlayerData();
    }

    public void WinPrice(int price)
    {
        BuyUpgrade(-price);
        SavePlayerData();
        UpgradeUI.Instance.CheckBuyButtonsClickable();
    }

    #region Money Issues
    public int GetGainPriceValue(float pricePercent)
    {
        return Mathf.FloorToInt(playerData.income * pricePercent);
    }

    public float GetMoney()
    {
        return playerData.money;
    }

    public void BuyUpgrade(int price)
    {
        SetMoneyToPlayerData(-price);

        SetMoneyText();
    }

    private void SetMoneyText()
    {
        TrainingUI.Instance.SetMoneyText(playerData.money);
    }

    private void SetMoneyToPlayerData(int gainedMoney)
    {
        playerData.SetMoney(playerData.money + gainedMoney);
    }
    #endregion

    #region HitCount Issues
    private void SetTrainingText()
    {
        TrainingUI.Instance.SetTrainingProgress(playerData.hitCount, CurrentTrainingSkipHitCount());
    }

    private void IncreaseHitCount()
    {
        playerData.hitCount++;

        CheckHitCount();
    }

    private void SetHtCountToPlayerData()
    {
        playerData.SetHitCount(playerData.hitCount);
    }

    private void CheckHitCount()
    {
        if (playerData.hitCount == CurrentTrainingSkipHitCount())
        {
            playerData.hitCount = 0;

            if (playerData.hitCountIndex < hitCountDatas.Count - 1)
            {
                playerData.hitCountIndex = Random.Range(1, hitCountDatas.Count);
            }

            SetTrainingText();

            StartCoroutine(environment.ChangeEnvironmentAngles());
        }
    }

    #endregion

    #region Save & Load
    private void SavePlayerData()
    {
        SaveManager.Save(playerData, SaveFiles.PlayerDataSaveFile);
    }

    private void LoadPlayerData()
    {
        playerData = SaveManager.Load(PlayerData.Default, SaveFiles.PlayerDataSaveFile);
    }

    private void OnEnable()
    {
        LoadPlayerData();
        PlayerMMDataRefresh();
    }
    #endregion

    private void PlayerMMDataRefresh()
    {
        preMMData.SetPlayerStamina(playerData.stamina).SetPlayerAccuracy(playerData.accuracy);
    }

    #region PlayerData Gets&Sets
    public int GetIncome()
    {
        return playerData.income;
    }

    public void SetIncome(int value)
    {
        playerData.income = value;
    }

    public float GetStamina()
    {
        return playerData.stamina;
    }

    public void SetStamina(float value)
    {
        playerData.stamina = value;

        PlayerMMDataRefresh();
    }

    public int GetAccuracy()
    {
        return playerData.accuracy;
    }

    public void SetAccuracy(int value)
    {
        playerData.accuracy = value;

        PlayerMMDataRefresh();
    }
    #endregion


    public override void GainPoint(int point)
    {
        base.GainPoint(point);
        MMUI.Instance.scoreUI.SetPlayerScore(currentScore);
    }
}
