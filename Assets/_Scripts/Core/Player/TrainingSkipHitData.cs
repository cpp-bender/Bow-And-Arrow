using UnityEngine;

[CreateAssetMenu(menuName = "Bow And Arrow/Training Skip Hit Count", fileName = "Hit Count")]
public class TrainingSkipHitData : ScriptableObject
{
    [SerializeField] int hitCount;

    public int HitCount { get => hitCount; }
}
