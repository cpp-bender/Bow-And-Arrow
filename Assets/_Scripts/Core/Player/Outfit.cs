using UnityEngine;
using System;

[Serializable]
public class Outfit
{
    public GameObject[] clothes;

    public void Switch(bool on)
    {
        for (int i = 0; i < clothes.Length; i++)
        {
            clothes[i].SetActive(on);
        }
    }
}
