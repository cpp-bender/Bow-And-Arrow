using DG.Tweening;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    [SerializeField] bool isFireArrow = false;
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider goCollider;

    public float Speed { get => speed; }

    public void Throw(Vector3[] movePath, Target target)
    {
        FollowMovePathAnim(movePath)
            .OnStart(() => { transform.parent = target.transform; })
            .OnComplete(() => { if (isFireArrow) ExplosionEffect(); });
    }

    public void Miss(Vector3[] movePath)
    {
        FollowMovePathAnim(movePath)
            .OnComplete(() => MissTheShot());
    }

    private Tween FollowMovePathAnim(Vector3[] movePath)
    {
        return transform.DOPath(movePath, speed, PathType.CatmullRom, PathMode.Full3D)
            .SetLookAt(0.01f)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .Play();
    }

    private void MissTheShot()
    {
        ActivateRigidbody();
        ActivateCollider();
        AddForceToRigidbody();
    }

    private void ExplosionEffect()
    {
        EffectManager.Instance.ShowExplosionEffect(transform.position);
    }

    private void ActivateCollider()
    {
        goCollider.enabled = true;
    }

    private void ActivateRigidbody()
    {
        rb.isKinematic = false;
    }

    private void AddForceToRigidbody()
    {
        rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
    }
}
