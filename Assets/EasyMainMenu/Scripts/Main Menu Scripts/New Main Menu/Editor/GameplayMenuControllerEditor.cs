using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EMM
{
    [CustomEditor(typeof(GameplayMenuController))]
    public class GameplayMenuControllerEditor : Editor
    {
        GameplayMenuController _gmc;

        private void OnEnable()
        {
            _gmc = (GameplayMenuController)target;
        }

        public override void OnInspectorGUI()
        {
            //Controller texture
            Texture t = (Texture)Resources.Load("EditorContent/OptionsMenuController-icon");
            GUILayout.Box(t, GUILayout.ExpandWidth(true));

            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.HelpBox("All the UI References. There's nothing for you to edit in Inspector. The script handles everything automatically.", MessageType.Info);

            DrawDefaultInspector();

            EditorGUILayout.EndVertical();
        }

    }
}