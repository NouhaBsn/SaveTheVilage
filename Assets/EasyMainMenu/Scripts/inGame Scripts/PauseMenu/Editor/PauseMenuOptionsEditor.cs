using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PauseMenuOptions))]
public class PauseMenuOptionsEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Pause Menu Options", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("This script simply stops the Time and show you all the " +
                                    "contents of the Pause Menu available for you to choose from like :\n" +
                                    "- Resume\n" +
                                    "- Load\n" +
                                    "- Main Menu\n" +
                                    "etc.\n\n" +
                                    "Please go through this script to understand the functionality clearly.", MessageType.Info);

        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }

}
