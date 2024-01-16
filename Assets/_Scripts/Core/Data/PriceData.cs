using UnityEngine;

[CreateAssetMenu(menuName = "Bow And Arrow/Prices", fileName = "Prices")]
public class PriceData : ScriptableObject
{
    [SerializeField] int[] prices;

    public int[] Prices { get => prices; }
}
