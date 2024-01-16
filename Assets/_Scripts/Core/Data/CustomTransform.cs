using UnityEngine;
using System;

[Serializable]
public class CustomTransform 
{
    public Vector3 position = Vector3.zero;
    public Vector3 rotation = Vector3.zero;

    public void ApplyChange(GameObject ctx)
    {
        ctx.transform.position = position;
        ctx.transform.rotation = Quaternion.Euler(rotation);
    }
}
