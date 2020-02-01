using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadGameManager))]
public class LoadGameManagerEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Load Game Manager", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("This script handles all the operations required " +
                                    "whenever the game is loaded like from : \n\n" +
                                    "- From which slot the game is loaded.\n" +
                                    "- What is the save trigger ID where the game is saved. \n" +
                                    "etc.", MessageType.Info);

        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }

}
