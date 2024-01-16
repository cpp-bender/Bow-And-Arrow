using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    [Header("Effect Prefabs")]
    [SerializeField] PriceGainEffect priceGainEffectPrefab;
    [SerializeField] PriceGainEffect pointGainEffectPrefab;
    [SerializeField] PriceGainEffect comboEffectPrefab;
    [SerializeField] PriceGainEffect missEffectPrefab;
    [SerializeField] GameObject levelUpFXPrefab;

    [Header("Hit Effects")]
    [SerializeField] PriceGainEffect hitYellowEffectPrefab;
    [SerializeField] PriceGainEffect hitRedEffectPrefab;
    [SerializeField] PriceGainEffect hitBlueEffectPrefab;
    [SerializeField] GameObject explosionEffectPrefab;

    private Vector3 levelUpFXPos;

    private PriceGainEffect gainPriceEffect;
    private PriceGainEffect gainPointEffect;
    private PriceGainEffect comboEffect;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ShowPriceGainEffect(float price, Vector3 pos)
    {
        if (gainPriceEffect != null)
        {
            DestroyImmediate(gainPriceEffect.gameObject);
        }

        gainPriceEffect = Instantiate(priceGainEffectPrefab, pos, priceGainEffectPrefab.transform.rotation);

        gainPriceEffect.SetPrice(price);
    }

    public void ShowPointGainEffect(int point, Vector3 pos)
    {
        if (gainPointEffect != null)
        {
            DestroyImmediate(gainPointEffect.gameObject);
        }

        gainPointEffect = Instantiate(pointGainEffectPrefab, pos, pointGainEffectPrefab.transform.rotation);
        gainPointEffect.SetPoint(point);
    }

    public void ShowExplosionEffect(Vector3 pos)
    {
        var explosionEffect = Instantiate(explosionEffectPrefab, pos, explosionEffectPrefab.transform.rotation);
    }

    public void ShowComboEffect(int comboCount, string comboWord, string hitWord, Vector3 pos)
    {
        if (comboEffect != null)
        {
            DestroyImmediate(comboEffect.gameObject);
        }

        comboEffect = Instantiate(comboEffectPrefab, pos, comboEffectPrefab.transform.rotation);
        comboEffect.SetCombo(comboCount, comboWord, hitWord);
    }

    public void ShowArrowMissEffect(Vector3 pos)
    {
        var gainEffect = Instantiate(missEffectPrefab, pos, missEffectPrefab.transform.rotation);
        gainEffect.SetMissText();
    }

    /*
    public void ShowArrowHitYellowEffect(Vector3 pos)
    {
        var hitYellowEffect = Instantiate(hitYellowEffectPrefab, pos, hitYellowEffectPrefab.transform.rotation);
        hitYellowEffect.SetHitYellowText();
    }

    public void ShowArrowHitRedEffect(Vector3 pos)
    {
        var hitRedEffect = Instantiate(hitRedEffectPrefab, pos, hitRedEffectPrefab.transform.rotation);
        hitRedEffect.SetHitRedText();
    }

    public void ShowArrowHitBlueEffect(Vector3 pos)
    {
        var hitBlueEffect = Instantiate(hitBlueEffectPrefab, pos, hitBlueEffectPrefab.transform.rotation);
        hitBlueEffect.SetHitBlueText();
    }*/

    public void ShowLevelUpEffect()
    {
        levelUpFXPos = Vector3.back * .5f;

        var lookRot = Quaternion.LookRotation(Camera.main.transform.up);

        Instantiate(levelUpFXPrefab, PlayerController.Instance.transform.TransformPoint(levelUpFXPos), lookRot);
    }
}
