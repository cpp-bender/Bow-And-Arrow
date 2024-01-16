using UnityEngine;

[CreateAssetMenu(menuName = "Bow And Arrow/MM Transform Data", fileName = "MM Transform Data")]
public class AITransformData : ScriptableObject
{
    [SerializeField] CustomTransform aiTransform;

    public CustomTransform AiTransform { get => aiTransform; }
}
