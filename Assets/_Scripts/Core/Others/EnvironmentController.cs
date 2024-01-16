using UnityEngine;
using DG.Tweening;

public class EnvironmentController : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public EnvironmentData environmentData;

    public void SetPlayerTransform(int index)
    {
        var player = PlayerController.Instance;

        player.transform.position = environmentData.PlayerTransforms[index].position;

        player.transform.rotation = Quaternion.Euler(environmentData.PlayerTransforms[index].rotation);
    }

    public void SetCameraTransform(int index)
    {
        var cam = Camera.main.GetComponent<HyperCamera>();

        Sequence camSeq = DOTween.Sequence();

        Tween moveTween = cam.transform.DOMove(environmentData.CameraTransforms[index].position, .5f);

        Tween rotateTween = cam.transform.DORotate(environmentData.CameraTransforms[index].rotation, .5f);

        camSeq.Append(moveTween).Join(rotateTween);

        camSeq.Play();
    }

    public void SetAITransform(int index)
    {
        var aiController = AIManager.Instance.CurrentAI;

        if (aiController == null)
        {
            return;
        }

        aiController.transform.position = environmentData.AiTransforms[index].position;
        aiController.transform.rotation = Quaternion.Euler(environmentData.AiTransforms[index].rotation);
    }
}
