using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelSelectManager))]
public class LevelSelectManagerEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Level Select Screen Manager", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("The Primary thing this script does is to Override " +
                                    "the 'New Game Scene Name' of Main Menu Controller Script ONLY IF the" +
                                    "'Has Level Selection' bool is checked!\n\n" +
                                    "If this bool is unchecked this Level Select System Script " +
                                    "will be removed from the scene at RUNTIME!", MessageType.Info);
           
            EditorGUILayout.HelpBox("Under the 'Level Items Configuration' you have to fill the " +
                                    "details about your scenes which you want to load from the Level Select Screen!", MessageType.Info);

            EditorGUILayout.HelpBox("Please see this video to get started " +
                                   "with Level Select System in no time!", MessageType.Info);

            if (GUILayout.Button("Level Select System Tutorial"))
            {
                Application.OpenURL("https://youtu.be/WgavWMepQT0");
            }


        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }
}
