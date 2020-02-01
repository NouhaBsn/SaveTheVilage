using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveGameTrigger))]
public class SaveGameTriggerEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Save Game Trigger", EditorStyles.helpBox);

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
                                    "defining the following properties :\n\n" +
                                    "Save Name :\t Save Name which will be displayed in the respective Save Slot.\n\n" +
                                    "Save Percentage :\t Game Completion percentage which will be shown in respective Save Slot. \n\n" +
                                    "Spawn Point :\t Transform position where you want to spawn player if he loads the game saved at this Trigger.\n\n" +
                                    "Save Trigger ID :\t The Most Important property. It must be unique and MUST NOT be similar to any other save game trigger present inside this scene.\n\n" +
                                    "Debug Spawn :\t If true, player will be spawned at this Save Trigger's Spawn point when this scene loads!", MessageType.Info);


            EditorGUILayout.HelpBox("I recommend watching this step by step tutorial video for " +
                                    "clear understanding!", MessageType.Info);

            if (GUILayout.Button("Save Trigger Setup"))
            {
                Application.OpenURL("https://youtu.be/w3BMF94zF6Q");
            }


        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }
}
