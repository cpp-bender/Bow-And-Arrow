using System.Collections;
using UnityEngine;

public class AIController : CharacterControl
{
    [Header("DEPENCENDIES")]
    public OutfitDrawer outfitDrawer;

    public bool canShoot;

    [SerializeField] float aiAccuracy = 50;
    [SerializeField] float aiStamina = 8;

    private float lastThrowTime = 0;
    private float nextThrowDuration;
    private const float STARTSTAMINA = 8f;


    private void Update()
    {
        if (CanThrow() && canShoot)
        {
            PlayThrowAnimation(0f);

            lastThrowTime = Time.time;
            nextThrowDuration = Random.Range(1f * STARTSTAMINA / aiStamina, 1.5f);
        }
    }

    public override IEnumerator Throw(float fireRatio, float duration)
    {
        yield return new WaitForSeconds(duration);

        bow.Throw(RandomFireRatio(), this);
    }

    private bool CanThrow()
    {
        if ((lastThrowTime + nextThrowDuration) < Time.time)
            return true;
        return false;
    }

    private float RandomFireRatio()
    {
        float fireRatio = 0f;

        var hitChance = Random.Range(0f, 100f);
        var inputRealize = Random.value;

        if (hitChance < aiAccuracy) // Is He hit
        {
            if(inputRealize < 0.1f) // %15 perfect shoot
            {
                fireRatio = Random.value * 0.2f;
            }
            else // %85 normal shoot
            {
                fireRatio = Random.Range(0f, 0.9f);
            }
        }
        else
        {
            fireRatio = 1f;
        }

        fireRatio = Mathf.Clamp(fireRatio, 0f, 1f);

        return fireRatio;
    }

    public void SetAIData(AIData aiData)
    {
        aiAccuracy = aiData.Accuracy;
        aiStamina = aiData.Stamina;
    }

    public override void GainPoint(int point)
    {
        base.GainPoint(point);
        MMUI.Instance.scoreUI.SetAIScore(currentScore);
    }
}
