using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("AnimateButtonScale", 10f, 30f);
    }

    private void AnimateButtonScale()
    {
        transform.DOScale(Vector3.one * 1.2f, 0.25f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            transform.DOScale(Vector3.one * 1f, 0.25f).SetEase(Ease.InQuad).Play();
        }).Play();
    }
}
