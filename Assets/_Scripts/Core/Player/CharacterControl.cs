using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class CharacterControl : MonoBehaviour, IPointGainer
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Bow bow;
    public Target target;

    protected int currentScore = 0;
    protected Coroutine firstThrow = null;
    protected float firstThrowFireRatio = -1f;
    protected float secondThrowFireRatio = -1f;

    public abstract IEnumerator Throw(float fireRatio, float duration);

    public void PlayThrowAnimation(float fireRatio)
    {
        if (firstThrowFireRatio < 0f)
        {
            firstThrowFireRatio = fireRatio;
        }
        else
        {
            secondThrowFireRatio = fireRatio;
        }

        HandleThrow();
    }

    public void HandleThrow()
    {
        var currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        int hash = Animator.StringToHash("Throw");

        if (currentStateInfo.IsName("Throw"))
        {
            animator.Play(hash, 0, .55f);

            StopAllCoroutines();
            StartCoroutine(Throw(firstThrowFireRatio, 0.0f));

            if (secondThrowFireRatio >= 0f)
            {
                StartCoroutine(Throw(secondThrowFireRatio, 0.1f));
            }

            firstThrowFireRatio = -1f;
            secondThrowFireRatio = -1f;
        }
        else
        {
            animator.Play(hash, 0, 0.2f);

            firstThrow = StartCoroutine(Throw(firstThrowFireRatio, 0.65f));
            StartCoroutine(ResetFirstThrowFireRatio());
        }
    }

    private IEnumerator ResetFirstThrowFireRatio()
    {
        yield return new WaitForSeconds(0.65f);

        firstThrowFireRatio = -1f;
    }

    public virtual void GainPoint(int point)
    {
        currentScore += point;
    }

    public int GetPoint()
    {
        return currentScore;
    }
}
