using System.Collections.Generic;
using UnityEngine;

public class OutfitDrawer : MonoBehaviour
{
    [Header("DEPENCENDIES")]
    public List<Outfit> outfits;

    [Header("OVERWRITE")]
    public IntVariable outfitData;

    [Header("DEBUG"), Range(0, 6)]
    public int outfitIndex;

    private void OnValidate()
    {
        ChangeCombine();
    }

    private void ChangeCombine()
    {
        for (int i = 0; i < outfits.Count; i++)
        {
            outfits[i].Switch(false);
        }

        outfits[outfitIndex].Switch(true);

        outfitData.ApplyChange(outfitIndex);
    }

    public void ChangeCombine(int index)
    {
        for (int i = 0; i < outfits.Count; i++)
        {
            outfits[i].Switch(false);
        }

        outfits[index].Switch(true);

        outfitData.ApplyChange(index);
    }

    public void SyncCombine()
    {
        for (int i = 0; i < outfits.Count; i++)
        {
            outfits[i].Switch(false);
        }

        outfits[outfitData.Value].Switch(true);
    }
}
