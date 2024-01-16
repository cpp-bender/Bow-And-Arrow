using SimpleJSON.Util;
using System;

[Serializable]
public class PriceIndexData : ISaveable
{
    public int incomePriceIndex;
    public int staminaPriceIndex;
    public int accuracyPriceIndex;

    public PriceIndexData()
    {

    }

    public PriceIndexData SetIncomePriceIndex(int incomePriceIndex)
    {
        this.incomePriceIndex = incomePriceIndex;
        return this;
    }

    public PriceIndexData SetStaminaPriceIndex(int staminaPriceIndex)
    {
        this.staminaPriceIndex = staminaPriceIndex;
        return this;
    }

    public PriceIndexData SetAccuracyPriceIndex(int accuracyPriceIndex)
    {
        this.accuracyPriceIndex = accuracyPriceIndex;
        return this;
    }
}
