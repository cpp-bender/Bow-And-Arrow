using UnityEngine;

[CreateAssetMenu(menuName = "Bow And Arrow/AI Data", fileName = "AI Data")]
public class AIData : ScriptableObject
{
    [SerializeField] float stamina;
    [SerializeField] float accuracy;
    [SerializeField] string characterName;
    [SerializeField] int winPrice;
    [Range(0, 6)]
    [SerializeField] int characterClothIndex;

    public float Accuracy { get => accuracy; }
    public float Stamina { get => stamina; }
    public string Name { get => characterName; }
    public int CharacterClothIndex { get => characterClothIndex; }
    public int WinPrice { get => winPrice; }
}
