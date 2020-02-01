using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MainMenuController))]
public class MainMenuControllerEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Main Menu Controller", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("The Controller script which will be controlling the whole menu system behaviour.\n\n" +
                                    "New Game Scene Name : Type the game scene name you want to load, " +
                                    "however this field will be overriden " +
                                    "if you are opting to use Level Select System. \n\n" +
                                    "Quick Save Slot ID : Define which slot you want to use " +
                                    "while quicksaving. Please see the below video for better understanding!", MessageType.Info);

            if (GUILayout.Button("Quick Save Tutorial Video!"))
            {
                Application.OpenURL("https://youtu.be/0LRwJ4BpcJA");
            }

            EditorGUILayout.HelpBox("All the fields under the 'Options Panel' are the UI References.\n" +
                                    "You can select them and I highly recommend you to " +
                                    "modify them to make them as awesome as possible! ;)", MessageType.Info);
        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }

}
