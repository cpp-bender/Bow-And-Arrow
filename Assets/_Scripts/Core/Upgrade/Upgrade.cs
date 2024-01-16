using SimpleJSON.Util;
using UnityEngine;
using System;

[Serializable]
public class Upgrade : ISaveable
{
    public int incomeIndex = 0;
    public int staminaIndex = 0;
    public int accuracyIndex = 0;

    public int IncomeIndex { get => incomeIndex; set => incomeIndex = value; }
    public int StaminaIndex { get => staminaIndex; set => staminaIndex = value; }
    public int AccuracyIndex { get => accuracyIndex; set => accuracyIndex = value; }

    public Upgrade()
    {

    }
}
