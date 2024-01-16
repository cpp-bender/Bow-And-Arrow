using Random = UnityEngine.Random;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    [SerializeField] float maxYPosValue = 2f;
    [SerializeField] List<Transform> targetPoints;
    [SerializeField] Transform targetPointsParentTransform;

    private float point0LocalStartPosY;
    private float point1LocalStartPosY;
    private float yOffset;
    private Tween hitFeedBackTween = null;

    private void Start()
    {
        point0LocalStartPosY = targetPoints[0].localPosition.y;
        point1LocalStartPosY = targetPoints[1].localPosition.y;
        
    }

    public Vector3 CalculateTargetPoint(float fireRatio)
    {
        ResetLocalPositions();
        ChangePointsRotations();
        CalculateYOffset(fireRatio);
        AddYOffsetsToTargetPoints();
        return FindThrowPosition();
    }

    public float GetHitPricePercent(float fireRatio)
    {
        if (fireRatio >= 1f)
        {
            return 0f; // Miss
        }
        else if (fireRatio > 0.8f)
        {
            return 0.2f; // White
        }
        else if (fireRatio > 0.6f)
        {
            return 0.5f; // Black        
        }
        else if (fireRatio > 0.4f)
        {
            return 0.8f; // Blue (Amazing)
        }
        else if (fireRatio > 0.2f)
        {
            return 1f; // Red (Perfect)
        }
        else
        {
            return 2f; // Yellow (Bullseye)
        }
    }

    public int GetHitPoint(float fireRatio)
    {
        if (fireRatio >= 1f)
        {
            return 0; // Miss
        }
        else if (fireRatio > 0.8f)
        {
            return 1; // White
        }
        else if (fireRatio > 0.6f)
        {
            return 2; // Black        
        }
        else if (fireRatio > 0.4f)
        {
            return 3; // Blue (Amazing)
        }
        else if (fireRatio > 0.2f)
        {
            return 5; // Red (Perfect)
        }
        else
        {
            return 10; // Yellow (Bullseye)
        }
    }

    public bool IsPerfectHit(float fireRatio)
    {
        var roundedFireRatio = (float)Math.Round(fireRatio, 2);
        var hitPercent = 100 - roundedFireRatio * 100;

        if (hitPercent <= 100 && hitPercent >= 81)
        {
            return true;
        }
        return false;
    }

    public void HitFeedBack()
    {
        if (hitFeedBackTween == null)
        {

            hitFeedBackTween = transform.DOShakeRotation(0.25f, 9, 10, 0)
                .OnComplete(() => { hitFeedBackTween = null; })
                .Play();
        }
    }

    private void ChangePointsRotations()
    {
        var randomRot = Random.Range(0f, 360f);

        targetPointsParentTransform.eulerAngles = new Vector3(targetPointsParentTransform.eulerAngles.x,
            targetPointsParentTransform.eulerAngles.y, randomRot);
    }

    private void ResetLocalPositions()
    {
        targetPoints[0].localPosition = new Vector3(targetPoints[0].localPosition.x, point0LocalStartPosY, targetPoints[0].localPosition.z);
        targetPoints[1].localPosition = new Vector3(targetPoints[1].localPosition.x, point1LocalStartPosY, targetPoints[1].localPosition.z);
    }

    private void AddYOffsetsToTargetPoints()
    {
        var coinFlip = (Random.Range(0f, 1f) < 0.5f) ? true : false;

        if (coinFlip)
        {
            targetPoints[0].localPosition += Vector3.up * yOffset;
            targetPoints[1].localPosition += Vector3.up * yOffset;
        }
        else
        {
            targetPoints[0].localPosition -= Vector3.up * yOffset;
            targetPoints[1].localPosition -= Vector3.up * yOffset;
        }
    }

    private void CalculateYOffset(float fireRatio)
    {
        yOffset = fireRatio * maxYPosValue;
    }

    private Vector3 FindThrowPosition()
    {
        return (targetPoints[0].position + targetPoints[1].position) / 2;
    }

}
