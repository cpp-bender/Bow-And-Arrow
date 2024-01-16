using UnityEngine;

[CreateAssetMenu()]
public class IntVariable : ScriptableObject
{
    [SerializeField, Range(0, 6)] int value;

    public int Value { get => value; }

    public void ApplyChange(int value)
    {
        this.value = value;
    }
}
