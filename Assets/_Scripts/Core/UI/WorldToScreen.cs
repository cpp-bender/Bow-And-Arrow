using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreen : SingletonMonoBehaviour<WorldToScreen>
{
    private Camera cam;
    public Transform worldPos;
    private RectTransform rectTransform;
    public RectTransform perfectEffectUI;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AdjustThrowUIScreenPosition()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos.position);

        rectTransform.anchoredPosition = new Vector2(screenPos.x, screenPos.y);
        perfectEffectUI.anchoredPosition = new Vector2(screenPos.x, screenPos.y);
    }
}
