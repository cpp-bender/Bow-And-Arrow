using UnityEngine;

[CreateAssetMenu(menuName = "Bow And Arrow/Environment Picker Data", fileName = "Environment Picker Data")]
public class EnvironmentQueueData : ScriptableObject
{
    [SerializeField] int[] environmentQueues;

    public int[] EnvironmentQueues { get => environmentQueues; }
}
