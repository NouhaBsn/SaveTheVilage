using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EasyAudioUtility))]
public class EasyAudioUtilityEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Easy Audio Utility", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("Define all the AudioClips you have in " +
                "this script and you can simply call them from anywhere " +
                "in your game with single line of code:\n\n" +
                "EasyAudioUtility.instance.Play(Your Clip Name);", MessageType.Info);
        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }

}
