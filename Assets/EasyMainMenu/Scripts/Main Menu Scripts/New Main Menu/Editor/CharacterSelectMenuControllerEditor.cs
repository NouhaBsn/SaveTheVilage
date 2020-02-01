using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace EMM
{
    [CustomEditor(typeof(CharacterSelectMenuController))]
    public class CharacterSelectMenuControllerEditor : Editor
    {
        CharacterSelectMenuController _csm;

        private void OnEnable()
        {
            _csm = (CharacterSelectMenuController)target;
        }

        public override void OnInspectorGUI()
        {
            //Controller texture
            Texture t = (Texture)Resources.Load("EditorContent/CharSelectMenu-icon");
            GUILayout.Box(t, GUILayout.ExpandWidth(true));

            EditorGUILayout.BeginVertical("Box");

            DrawDefaultInspector();

            EditorGUILayout.EndVertical();

            DrawCharList();

        }


        void DrawCharList()
        {

            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal("Box");

            if (GUILayout.Button("Add"))
            {
                AllCharactersData acd = new AllCharactersData();
                _csm.AllCharacters.Add(acd);
            }

            if (GUILayout.Button("Clear"))
            {
                _csm.AllCharacters.Clear();
            }

            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < _csm.AllCharacters.Count; i++)
            {

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.BeginVertical("Box");

                EditorGUILayout.BeginHorizontal();
                string CharacterName = EditorGUILayout.TextField("Character Name", _csm.AllCharacters[i].CharacterName);

                if (GUILayout.Button("X", GUILayout.Width(35)))
                {
                    _csm.AllCharacters.RemoveAt(i);
                    break;
                }
                EditorGUILayout.EndHorizontal();

                GameObject CharacterMesh = (GameObject) EditorGUILayout.ObjectField("Character Mesh", _csm.AllCharacters[i].CharacterMesh, typeof(GameObject));

                EditorGUILayout.EndVertical();

                if(EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "All Characters EMM");

                    _csm.AllCharacters[i].CharacterName = CharacterName;
                    _csm.AllCharacters[i].CharacterMesh = CharacterMesh;
                }
            }

            EditorGUILayout.EndVertical();

        }

    }

}