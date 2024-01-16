using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    [SerializeField] bool showEffect = false;
    [SerializeField] float minHitRatioValue = 0.75f;
    [SerializeField] float preDestinationYOffset;
    [SerializeField] Arrow arrowPrefab;
    [SerializeField] Arrow fireArrowPrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] CharacterControl character;

    private Vector3 targetPoint;
    private Vector3 preDestinationPos;
    private Arrow thrownArrow;
    private bool isPerfectHit = false;
    private int comboCount;

    private string[] comboWords = { "", "", "Awesome", "Great", "Amazing", "Fantastic", "Phenomenal", "Unbelievable", "Incredible", "Magnificent", "Legendary" };

    public void Throw(float fireRatio, IPriceGainer priceGainer)
    {
        CreateArrow(fireRatio);
        FindTargetPoint(fireRatio);
        CalculatePreDestinationPosition();

        if (IsWillArrowHitTarget(fireRatio)) //Hit Shot
        {
            Taptic.Light();

            IncreaseComboCount(fireRatio);

            //StartCoroutine(ShowArrowHitEffect(fireRatio));

            ThrowArrow();

            StartCoroutine(GainPrice(priceGainer, fireRatio, FindHitDuration())); //Gain Price
        }
        else //Miss Shot
        {
            ResetComboCount();
            MissArrow();
        }
    }

    public void Throw(float fireRatio, IPointGainer pointGainer)
    {
        CreateArrow(fireRatio);
        FindTargetPoint(fireRatio);
        CalculatePreDestinationPosition();

        if (IsWillArrowHitTarget(fireRatio)) //Hit Shot
        {
            Taptic.Light();

            IncreaseComboCount(fireRatio);

            //StartCoroutine(ShowArrowHitEffect(fireRatio));

            ThrowArrow();

            StartCoroutine(GainPoint(pointGainer, fireRatio, FindHitDuration())); //Gain Point
        }
        else //Miss Shot
        {
            ResetComboCount();
            MissArrow();
        }
    }

    #region Path Issues
    private Vector3[] CreatePath()
    {
        Vector3[] path = { preDestinationPos, targetPoint };

        return path;
    }

    private void CalculatePreDestinationPosition()
    {
        var middleX = (thrownArrow.transform.position.x + targetPoint.x) / 2f;
        var middleZ = (thrownArrow.transform.position.z + targetPoint.z) / 2f;
        var middleY = (thrownArrow.transform.position.y + targetPoint.y) / 2f + preDestinationYOffset;

        preDestinationPos = Vector3.right * middleX + Vector3.up * middleY + Vector3.forward * middleZ;
    }

    private void FindTargetPoint(float fireRatio)
    {
        targetPoint = character.target.CalculateTargetPoint(fireRatio);
    }
    #endregion

    #region Arrow Issues
    private void CreateArrow(float fireRatio)
    {
        if (IsPerfectHit(fireRatio)) //Fire Arrow created
        {
            thrownArrow = Instantiate(fireArrowPrefab, throwPoint.position, throwPoint.rotation);
        }
        else //Normal Arrow created
        {
            thrownArrow = Instantiate(arrowPrefab, throwPoint.position, throwPoint.rotation);
        }
    }

    private void ThrowArrow()
    {
        thrownArrow.Throw(CreatePath(), character.target);
    }

    private void MissArrow()
    {
        thrownArrow.Miss(CreatePath());
    }

    private bool IsWillArrowHitTarget(float fireRatio)
    {
        if (fireRatio > minHitRatioValue)
            return false;
        return true;
    }

    private bool IsPerfectHit(float fireRatio)
    {
        isPerfectHit = character.target.IsPerfectHit(fireRatio);
        return isPerfectHit;
    }

    private float FindHitDuration()
    {
        return Vector3.Distance(throwPoint.position, targetPoint) / thrownArrow.Speed;
    }

    private void ResetComboCount()
    {
        comboCount = 0;
    }

    private void IncreaseComboCount(float fireRatio)
    {
        if(showEffect)
        {
            comboCount++;

            if (comboCount == 1)
            {
                StartCoroutine(ShowComboCountEffect(0, FindComboWord(comboCount), GetHitWord(fireRatio)));
            }
            else
            {
                StartCoroutine(ShowComboCountEffect(comboCount, FindComboWord(comboCount), ""));
            }
        }

    }

    private string FindComboWord(int comboCount)
    {
        if (comboCount < comboWords.Length)
        {
            return comboWords[comboCount];
        }
        else
        {
            return comboWords[comboWords.Length - 1];
        }
    }

    private IEnumerator ShowComboCountEffect(int comboCount, string comboWord, string hitWord)
    {
        yield return new WaitForSeconds(FindHitDuration());
        //var pos = PlayerController.Instance.transform.position + Vector3.right * 1.25f + Vector3.up * -0.5f;
        var pos = character.target.transform.position + Vector3.up * 1.75f - Vector3.right;

        EffectManager.Instance.ShowComboEffect(comboCount, comboWord, hitWord, pos);
    }

    private IEnumerator ShowArrowMissEffect()
    {
        yield return new WaitForSeconds(FindHitDuration());
        EffectManager.Instance.ShowArrowMissEffect(PlayerController.Instance.transform.position + Vector3.right * 1.25f + Vector3.up * -0.5f);
    }

    private string GetHitWord(float fireRatio)
    {
        if (fireRatio < 0.2f)
        {
            return "Bullseye";
        }
        else if (fireRatio < 0.40f)
        {
            return "Perfect";
        }
        else if (fireRatio < 0.57f)
        {
            return "Amazing";
        }
        else
            return "";
    }
    #endregion

    #region Price Gain
    private IEnumerator GainPrice(IPriceGainer priceGainer, float fireRatio, float waitTime)
    {
        float pricePercent = FindPricePercent(fireRatio);

        yield return new WaitForSeconds(waitTime);

        priceGainer.GainPrice(pricePercent);

        character.target.HitFeedBack();

        if (showEffect)
            ShowPriceGainEffect(priceGainer, pricePercent);
    }

    private float FindPricePercent(float fireRatio)
    {
        return character.target.GetHitPricePercent(fireRatio);
    }

    private void ShowPriceGainEffect(IPriceGainer gainer, float pricePercent)
    {
        var pos = character.target.transform.position + character.target.transform.up * 2.5f + character.target.transform.right * 1.25f;

        EffectManager.Instance.ShowPriceGainEffect(gainer.GetGainPriceValue(pricePercent), pos);
    }
    #endregion

    #region Point Gain
    private IEnumerator GainPoint(IPointGainer pointGainer, float fireRatio, float waitTime)
    {
        int point = character.target.GetHitPoint(fireRatio);

        yield return new WaitForSeconds(waitTime);

        pointGainer.GainPoint(point);

        character.target.HitFeedBack();

        if (showEffect)
            ShowPointGainEffect(point);
    }

    private void ShowPointGainEffect(int point)
    {
        var pos = character.target.transform.position + character.target.transform.up * 2.5f + character.target.transform.right * 1.25f;
        EffectManager.Instance.ShowPointGainEffect(point, pos);
    }
    #endregion
}
