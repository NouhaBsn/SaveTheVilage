using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIController))]
public class UIControllerEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("UI Controller", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("This script manages which UI needs to be enabled " +
                                    "and which UI has to be disabled!\n" +
                                    "I recommend going through this script for better Understanding!\n\n" +
                                    "Use Blur boolean will enable and disable the blur effect " +
                                    "only if you have attached it to the camera", MessageType.Info);

        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }
}
