using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EasyAudioUtility_SceneManager))]
public class eauSceneManagerEditor : Editor
{
    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Audio Scene Manager", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("This script is responsible for changing " +
                "the Audio in different levels.\n" +
                "Add how many Scenes you have here and define these respective fields :\n\n" +
                "Scene Name : Type the scene (which is in the BUILD SETTINGS) whose Audio you want to change.\n\n" +
                "Name : The Audio Name you define above in the EasyAudioUtility class.\n\n" +
                "Clip : Audio Clip which will replace the Audio Clip of the above mentioned 'Name'.\n\n"
                , MessageType.Info);

            EditorGUILayout.HelpBox("Please see the below video tutorial to see it in action and undertsand it more clearly.", MessageType.Info);

            if(GUILayout.Button("Watch Tutorial"))
            {
                Application.OpenURL("https://youtu.be/oz7XzGNDwGE");
            }
        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }

}
