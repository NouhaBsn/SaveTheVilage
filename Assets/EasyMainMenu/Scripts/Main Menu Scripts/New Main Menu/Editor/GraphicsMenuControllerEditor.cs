using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace EMM
{
    [CustomEditor(typeof(GraphicsMenuController))]
    public class GraphicsMenuControllerEditor : Editor
    {
        GraphicsMenuController _gmc;

        private void OnEnable()
        {
            _gmc = (GraphicsMenuController)target;
        }

        public override void OnInspectorGUI()
        {
            //Controller texture
            Texture t = (Texture)Resources.Load("EditorContent/OptionsMenuController-icon");
            GUILayout.Box(t, GUILayout.ExpandWidth(true));

            DrawOptionsControllerProperties();

            //DrawDefaultInspector();
        }

        void DrawOptionsControllerProperties()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.HelpBox("All the UI References. There's nothing for you to edit in Inspector. The script handles everything automatically.", MessageType.Info);

            Text TextureQualityValue = (Text)EditorGUILayout.ObjectField("Texture Quality Value", _gmc.TextureQualityValue, typeof(Text));
            Text AntiAliasingValue = (Text)EditorGUILayout.ObjectField("Anti Aliasing Value", _gmc.AntiAliasingValue, typeof(Text));
            Text ShadowsValue = (Text)EditorGUILayout.ObjectField("Shadows Value", _gmc.ShadowsValue, typeof(Text));
            Text VSyncValue = (Text)EditorGUILayout.ObjectField("VSync Value", _gmc.VSyncValue, typeof(Text));
            Text ResolutionValue = (Text)EditorGUILayout.ObjectField("Resolution Value", _gmc.ResolutionValue, typeof(Text));

            EditorGUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "_omc Properties");

                _gmc.TextureQualityValue = TextureQualityValue;
                _gmc.AntiAliasingValue = AntiAliasingValue;
                _gmc.ShadowsValue = ShadowsValue;
                _gmc.VSyncValue = VSyncValue;
                _gmc.ResolutionValue = ResolutionValue;
            }
        }
    }
}