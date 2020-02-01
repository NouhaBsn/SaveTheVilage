using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (EIU.EIU_ControlsMenu))]
public class EIU_ControlsMenuEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Easy Input Utility's Controls Menu", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("This script holds the main functinality of the Input Utility!\n" +
                                    "I have made it in such a way that you don't have to assign anything! " +
                                    "All the UI references are already assigned and everything is ready to be used!", MessageType.Info);


        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }
}
