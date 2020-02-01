using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EIU.EasyInputUtility))]
public class EasyInputUtilityEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Easy Input Utility", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("You just have to define all the possible Inputs here " +
                                    "if you are using Easy Input Utility.\n" +
                                    "Please see the SimpleCube script to see how to use this simple " +
                                    "Input Module.", MessageType.Info);


            EditorGUILayout.HelpBox("I recommend watching this overview video to get things " +
                                    "clear if you haven't watched it already.", MessageType.Info);

            if (GUILayout.Button("Easy Input Utility Overview"))
            {
                Application.OpenURL("https://youtu.be/o3oD4C1-2wg");
            }


        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }
}
