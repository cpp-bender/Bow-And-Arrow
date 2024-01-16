using UnityEngine;

[CreateAssetMenu(menuName = "Bow And Arrow/Upgrade Data", fileName = "UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] float value;
    [SerializeField] int price;

    public float Value { get => value; set => this.value = value; }
    public int Price { get => price; set => price = value; }
}
