using UnityEngine;

[CreateAssetMenu(menuName = "Bow And Arrow/PreMM Data", fileName = "PreMM Data")]
public class PreMMData : ScriptableObject
{
    [SerializeField] float playerStamina;
    [SerializeField] int playerAccuracy;

    public float PlayerStamina { get => playerStamina; }
    public int PlayerAccuracy { get => playerAccuracy; }

    public PreMMData SetPlayerStamina(float stamina)
    {
        playerStamina = stamina;
        return this;
    }

    public PreMMData SetPlayerAccuracy(int accuracy)
    {
        playerAccuracy = accuracy;
        return this;
    }
}
