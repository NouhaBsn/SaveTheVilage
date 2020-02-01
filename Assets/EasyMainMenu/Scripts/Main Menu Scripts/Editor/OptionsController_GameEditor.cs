using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OptionsController_Game))]
public class OptionsController_GameEditor : Editor {

    bool showHelp;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Game Settings", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("All the fields under the 'Game Options' are the UI References.\n" +
                                    "However, the HUD option you see is unassigned and you" +
                                    " can use it to Enable/Disable your Game's HUD! All " +
                                    "references are set you just have to make its functionality.\n" +
                                    "If your game doesn't have any HUD you can simply delete this field or " +
                                    "disable this in Inspector.", MessageType.Info);

          
        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }
}
