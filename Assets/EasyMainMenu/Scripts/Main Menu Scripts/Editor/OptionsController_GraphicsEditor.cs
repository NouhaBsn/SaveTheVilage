using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OptionsController_Graphics))]
public class OptionsController_GraphicsEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Graphics Settings", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("All the fields under the 'Graphics Options' are the UI References.\n" +
                                    "You can select them and I highly recommend you to " +
                                    "modify them to make them as awesome as possible! ;)", MessageType.Info);

            EditorGUILayout.HelpBox("Please see the video below to see " +
                                    "how these settings affect the game at RUNTIME!", MessageType.Info);

            if (GUILayout.Button("Graphics Settings Video!"))
            {
                Application.OpenURL("https://youtu.be/Qs-jh8G1uhI");
            }
        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }
}
