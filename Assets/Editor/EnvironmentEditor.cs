using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnvironmentManager))]
public class EnvironmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUI.enabled = Application.isPlaying;

        if (GUILayout.Button("Switch Environmet"))
        {
            var ctx = (EnvironmentManager)target;

            //ctx.SwitchEnvironment();
        }

        if (GUILayout.Button("Change Environmet Angles"))
        {
            var ctx = (EnvironmentManager)target;

            ctx.ChangeEnvironmentAngles();
        }
    }
}
