using SimpleJSON.Util;
using System;

[Serializable]
public class PlayerData : ISaveable
{
    public int income;
    public float stamina;
    public int accuracy;
    public int hitCount;
    public int hitCountIndex;
    public int money;

    public static PlayerData Default { get => new PlayerData().SetIncome(10).SetStamina(8).SetAccuracy(50).SetMoney(100); }

    public PlayerData()
    {

    }

    public PlayerData SetIncome(int income)
    {
        this.income = income;
        return this;
    }

    public PlayerData SetStamina(int stamina)
    {
        this.stamina = stamina;
        return this;
    }

    public PlayerData SetAccuracy(int accuracy)
    {
        this.accuracy = accuracy;
        return this;
    }

    public PlayerData SetMoney(int money)
    {
        this.money = money;
        return this;
    }

    public PlayerData SetHitCount(int hitCount)
    {
        this.hitCount = hitCount;
        return this;
    }

    public PlayerData SetHitCountIndex(int hitCountIndex)
    {
        this.hitCountIndex = hitCountIndex;
        return this;
    }
}
