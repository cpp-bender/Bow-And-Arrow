using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUI.enabled = Application.isPlaying;

        var gameEvent = (GameEvent)target;

        if (GUILayout.Button("Raise"))
        {
            gameEvent.Raise();
        }
    }
}