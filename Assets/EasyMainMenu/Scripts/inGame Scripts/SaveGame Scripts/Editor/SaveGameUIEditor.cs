using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveGameUI))]
public class SaveGameUIEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Save Game UI", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("You don't have to do anything except " +
                                    "defining the following property :\n\n" +
                                    "Key :\t Define keycode you want to use to Quick Save your game!", MessageType.Info);


            EditorGUILayout.HelpBox("Watch this video to get the overview of the quick save feature.", MessageType.Info);

            if (GUILayout.Button("Quick Save Overview"))
            {
                Application.OpenURL("https://youtu.be/0LRwJ4BpcJA");
            }

            EditorGUILayout.HelpBox("And, watch this video to see the Save System in Action.", MessageType.Info);

            if (GUILayout.Button("Save System"))
            {
                Application.OpenURL("https://youtu.be/3LNNhh7mYoQ");
            }

        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }

}
