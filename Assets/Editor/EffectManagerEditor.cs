using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EffectManager))]
public class EffectManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUI.enabled = Application.isPlaying;

        if (GUILayout.Button("Play Level Up FX"))
        {
            var ctx = (EffectManager)target;

            ctx.ShowLevelUpEffect();
        }
    }
}
