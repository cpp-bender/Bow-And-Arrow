using UnityEngine;
using DG.Tweening;

public class HyperCamera : SingletonMonoBehaviour<HyperCamera>
{
    [Header("DEPENDENCIES")]
    public GameObject camPlaceHolder;
    public EnvironmentManager environment;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SetInitialTransform();
    }

    private void SetInitialTransform()
    {
        transform.position = environment.currentEnv.environmentData.CameraTransforms[1].position;

        transform.rotation = Quaternion.Euler(environment.currentEnv.environmentData.CameraTransforms[1].rotation);
    }

    public void FlyToPreMM()
    {
        Sequence camSeq = DOTween.Sequence();

        Tween moveTween = transform.DOMove(camPlaceHolder.transform.position, .5f);

        Tween rotateTween = transform.DORotate(camPlaceHolder.transform.rotation.eulerAngles, .5f);

        camSeq.Append(rotateTween).Join(moveTween);

        camSeq.Play();
    }

    public void FlyToTraining()
    {
        Sequence camSeq = DOTween.Sequence();

        var currentEnv = environment.currentEnv;

        Vector3 pos = currentEnv.environmentData.CameraTransforms[environment.CurrentEnvAngleIndex].position;

        Vector3 rot = currentEnv.environmentData.CameraTransforms[environment.CurrentEnvAngleIndex].rotation;

        Tween moveTween = transform.DOMove(pos, .5f);

        Tween rotateTween = transform.DORotate(rot, .5f);

        camSeq.Append(moveTween).Join(rotateTween);

        camSeq.Play();
    }

    public void FlyToMM()
    {
        Sequence camSeq = DOTween.Sequence();
        
        var currentEnv = environment.currentEnv;

        Vector3 pos = currentEnv.environmentData.CameraTransforms[environment.CurrentEnvAngleIndex].position;

        Vector3 rot = currentEnv.environmentData.CameraTransforms[environment.CurrentEnvAngleIndex].rotation;

        Tween moveTween = transform.DOMove(pos, .5f);

        Tween rotateTween = transform.DORotate(rot, .5f);

        camSeq.Append(moveTween).Join(rotateTween);

        camSeq.Play();
    }
}
