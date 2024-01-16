using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ScriptableValueSetter : MonoBehaviour
{
    public List<UpgradeData> staminas;
    public string objectName = "IncomeUpgradeData ";
    public int nameOffset = 12;

    [Header("Start Values")]
    public float startValue;
    public int startPrice;

    [Header("Offsets")]
    public float valueOffset;
    public int priceOffset;


    private void Start()
    {
        for (int i = 0; i < staminas.Count; i++)
        {
            staminas[i].Value = startValue + valueOffset * i;
            staminas[i].Price = startPrice + priceOffset * i;
            staminas[i].name = objectName + (nameOffset + i);
        }
    }
}
