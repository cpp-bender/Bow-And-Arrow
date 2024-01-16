using UnityEngine;

[CreateAssetMenu(menuName = "Bow And Arrow/Custom Transform Data", fileName = "Custom Transform Data")]
public class EnvironmentData : ScriptableObject
{
    [SerializeField] CustomTransform[] playerTransforms;
    [SerializeField] CustomTransform[] cameraTransforms;
    [SerializeField] CustomTransform[] aiTransforms;
    [SerializeField] Material skyBoxMat;

    public CustomTransform[] PlayerTransforms { get => playerTransforms; }
    public CustomTransform[] CameraTransforms { get => cameraTransforms; }
    public CustomTransform[] AiTransforms { get => aiTransforms; }
    public Material SkyBoxMat { get => skyBoxMat; }
}
