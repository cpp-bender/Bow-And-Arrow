using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    private void OnEnable()
    {
        DOHandAnimation();
    }

    private void DOHandAnimation()
    {
        Sequence handSeq = DOTween.Sequence();

        var rectTr = GetComponent<RectTransform>();

        Tween rotateTween = rectTr.DORotate(new Vector3(30f, 0f, 0f), .5f);

        handSeq.Append(rotateTween)
            .SetAutoKill(true)
            .SetRecyclable(true)
            .SetLoops(-1, LoopType.Yoyo);

        handSeq.Play();
    }
}
