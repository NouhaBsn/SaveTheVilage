using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EMM
{
    [CustomEditor(typeof(MainMenuController))]
    public class MainMenuControllerEditor : Editor
    {
        MainMenuController _mmc;

        private void OnEnable()
        {
            _mmc = (MainMenuController)target;
        }

        public override void OnInspectorGUI()
        {
            Texture t = (Texture)Resources.Load("EditorContent/MMController-icon");
            GUILayout.Box(t, GUILayout.ExpandWidth(true));

            DrawCustomInspector();

            //DrawDefaultInspector();
        }

        void DrawCustomInspector()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.HelpBox("General Settings.", MessageType.Info);

            bool UseLevelSelectMenu = EditorGUILayout.Toggle("Use Level Select Menu", _mmc.UseLevelSelectMenu);

            string newGameSceneName =  _mmc.newGameSceneName;

            if (!UseLevelSelectMenu)
                 newGameSceneName = EditorGUILayout.TextField("New Game Scene Name", _mmc.newGameSceneName);

            int quickSaveSlotID = EditorGUILayout.IntField("Quick Save Slot ID", _mmc.quickSaveSlotID);

            EditorGUILayout.HelpBox("Click and Main Menu BG SFX.", MessageType.None);
            string ButtonClickSFX = EditorGUILayout.TextField("Button Click SFX", _mmc.ButtonClickSFX);
            string MainMenuSFX = EditorGUILayout.TextField("Main Menu SFX", _mmc.MainMenuSFX);

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.HelpBox("Animator and All the Animations.", MessageType.Info);
            Animator MenuButtonsAnimator = (Animator)EditorGUILayout.ObjectField("Menu Buttons Animator", _mmc.MenuButtonsAnimator, typeof(Animator));

            EditorGUILayout.Space();
            string MainMenuToStartMenu = EditorGUILayout.TextField("Main Menu To Start Menu", _mmc.MainMenuToStartMenu); 
            string StartMenuToMainMenu = EditorGUILayout.TextField("Start Menu To Main Menu", _mmc.StartMenuToMainMenu);

            EditorGUILayout.Space();
            string StartMenuToLevelSelectMenu = EditorGUILayout.TextField("Start Menu To Level Select Menu", _mmc.StartMenuToLevelSelectMenu);
            string LevelSelectMenuToStartMenu = EditorGUILayout.TextField("Level Select Menu To Start Menu", _mmc.LevelSelectMenuToStartMenu);

            EditorGUILayout.Space();
            string StartMenuToLoadSlotMenu = EditorGUILayout.TextField("Start Menu To Load Slot Menu", _mmc.StartMenuToLoadSlotMenu);
            string LoadSlotMenuToStartMenu = EditorGUILayout.TextField("Load Slot Menu To Start Menu", _mmc.LoadSlotMenuToStartMenu);

            EditorGUILayout.Space();
            string MainMenuToCharMenu = EditorGUILayout.TextField("Main Menu To Char Menu", _mmc.MainMenuToCharMenu);
            string CharMenuToMainMenu = EditorGUILayout.TextField("Char Menu To Main Menu", _mmc.CharMenuToMainMenu);

            EditorGUILayout.Space();
            string MainMenuToOptionsMenu = EditorGUILayout.TextField("Main Menu To Options Menu", _mmc.MainMenuToOptionsMenu);
            string OptionsMenuToMainMenu = EditorGUILayout.TextField("Options Menu To Main Menu", _mmc.OptionsMenuToMainMenu);

            EditorGUILayout.Space();
            string OptionsMenuToGameplayMenu = EditorGUILayout.TextField("Options Menu To Gameplay Menu", _mmc.OptionsMenuToGameplayMenu);
            string GameplayMenuToOptionsMenu = EditorGUILayout.TextField("Gameplay Menu To Options Menu", _mmc.GameplayMenuToOptionsMenu);

            EditorGUILayout.Space();
            string OptionsMenuToGraphicsMenu = EditorGUILayout.TextField("Options Menu To Graphics Menu", _mmc.OptionsMenuToGraphicsMenu);
            string GraphicsMenuToOptionsMenu = EditorGUILayout.TextField("Graphics Menu To Options Menu", _mmc.GraphicsMenuToOptionsMenu);

            EditorGUILayout.EndVertical();


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "New Main Menu Controller");
                _mmc.MenuButtonsAnimator = MenuButtonsAnimator;
                _mmc.UseLevelSelectMenu = UseLevelSelectMenu;
                _mmc.newGameSceneName = newGameSceneName;
                _mmc.quickSaveSlotID = quickSaveSlotID;
                _mmc.ButtonClickSFX = ButtonClickSFX;
                _mmc.MainMenuSFX = MainMenuSFX;

                _mmc.MainMenuToStartMenu = MainMenuToStartMenu;
                _mmc.StartMenuToMainMenu = StartMenuToMainMenu;

                _mmc.StartMenuToLevelSelectMenu = StartMenuToLevelSelectMenu;
                _mmc.LevelSelectMenuToStartMenu = LevelSelectMenuToStartMenu;

                _mmc.StartMenuToLoadSlotMenu = StartMenuToLoadSlotMenu;
                _mmc.LoadSlotMenuToStartMenu = LoadSlotMenuToStartMenu;

                _mmc.MainMenuToCharMenu = MainMenuToCharMenu;
                _mmc.CharMenuToMainMenu = CharMenuToMainMenu;

                _mmc.MainMenuToOptionsMenu = MainMenuToOptionsMenu;
                _mmc.OptionsMenuToMainMenu = OptionsMenuToMainMenu;

                _mmc.OptionsMenuToGameplayMenu = OptionsMenuToGameplayMenu;
                _mmc.GameplayMenuToOptionsMenu = GameplayMenuToOptionsMenu;

                _mmc.OptionsMenuToGraphicsMenu = OptionsMenuToGraphicsMenu;
                _mmc.GraphicsMenuToOptionsMenu = GraphicsMenuToOptionsMenu;
            }
        }
    }
}