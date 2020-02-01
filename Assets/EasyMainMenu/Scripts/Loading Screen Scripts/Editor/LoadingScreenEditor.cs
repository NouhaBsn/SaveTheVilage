using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadingScreen))]
public class LoadingScreenEditor : Editor {

    bool showHelp;
    LoadingScreen loadingScreen;
    SerializedObject SO_loadingScreen;

    //Serialized properties of Loading Screen

    //1) Loading Bar Properties
    private SerializedProperty loadingBar;
    private SerializedProperty showLoadingBar;
    private SerializedProperty fillDelay;
    private SerializedProperty fillSpeed;

    //2) Circular Indicator Properties
    private SerializedProperty circularIndicator;
    private SerializedProperty circularLoadDelay;
    private SerializedProperty circularIndicatorAnimSpeed;

    //3) Image Transition Properties
    private SerializedProperty defaultLoadingScreenImage;
    private SerializedProperty showImageTransition;
    private SerializedProperty showRandomImageTransition;
    private SerializedProperty LoadingScreenImages;
    private SerializedProperty transitionDuration;
    private SerializedProperty transitionFader;

    //4) Input Prompt
    private SerializedProperty showContinueText;
    private SerializedProperty PressAnyKeyToContinue;
    private SerializedProperty LoadingText;

    //5) Scene Specific Loading Image Properties
    private SerializedProperty sceneSpecificLoading;

    private void OnEnable()
    {
        loadingScreen = (LoadingScreen) target;
        SO_loadingScreen = new SerializedObject(target);

        FindProperty_LoadingBar();
        FindProperty_CircularIndicator();
        FindProperty_ImageTransition();
        FindProperty_InputPrompt();
        FindProperty_SceneSpecificLoading();
    }

    void FindProperty_LoadingBar()
    {
        loadingBar = SO_loadingScreen.FindProperty("loadingBar");
        showLoadingBar = SO_loadingScreen.FindProperty("showLoadingBar");
        fillDelay = SO_loadingScreen.FindProperty("fillDelay");
        fillSpeed = SO_loadingScreen.FindProperty("fillSpeed");

    }

    void FindProperty_CircularIndicator()
    {
        circularIndicator = SO_loadingScreen.FindProperty("circularIndicator");
        circularLoadDelay = SO_loadingScreen.FindProperty("circularLoadDelay");
        circularIndicatorAnimSpeed = SO_loadingScreen.FindProperty("circularIndicatorAnimSpeed");

    }

    void FindProperty_ImageTransition()
    {
        defaultLoadingScreenImage = SO_loadingScreen.FindProperty("defaultLoadingScreenImage");
        showImageTransition = SO_loadingScreen.FindProperty("showImageTransition");
        showRandomImageTransition = SO_loadingScreen.FindProperty("showRandomImageTransition");
        LoadingScreenImages = SO_loadingScreen.FindProperty("LoadingScreenImages");
        transitionDuration = SO_loadingScreen.FindProperty("transitionDuration");
        transitionFader = SO_loadingScreen.FindProperty("transitionFader");

    }

    void FindProperty_InputPrompt()
    {
        showContinueText = SO_loadingScreen.FindProperty("showContinueText");
        PressAnyKeyToContinue = SO_loadingScreen.FindProperty("PressAnyKeyToContinue");
        LoadingText = SO_loadingScreen.FindProperty("LoadingText");
    }

    void FindProperty_SceneSpecificLoading()
    {
        sceneSpecificLoading = SO_loadingScreen.FindProperty("sceneSpecificLoading");
    }

    public override void OnInspectorGUI()
    {

        DrawTopHelpbox();

        SO_loadingScreen.Update();
        EditorGUI.BeginChangeCheck();

        //<------ Drawing Tabs ------>

        //1st line of Tabs
        DrawFirstTabs();

        //2nd line of Tabs
        DrawSecondTabs();

        //3rd line of Tabs
        DrawThirdTab();


        if (EditorGUI.EndChangeCheck())
        {
            SO_loadingScreen.ApplyModifiedProperties();
            GUI.FocusControl(null);
        }

        EditorGUI.BeginChangeCheck();

        //1st Tabs Properties
        DrawFirstTabsProperties();

        //2nd Tabs Properties
        DrawSecondTabsProperties();

        //3rd Tabs Properties
        DrawThirdTabsProperties();

        if (EditorGUI.EndChangeCheck())
            SO_loadingScreen.ApplyModifiedProperties();

      // DrawDefaultInspector();

        EditorGUILayout.EndVertical();
    }

    void DrawTopHelpbox()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Loading Screen", EditorStyles.helpBox);

        GUIStyle bSKin = new GUIStyle("box");
        bSKin.normal.textColor = Color.green;

        if (GUILayout.Button("[?]", bSKin))
        {
            showHelp = !showHelp;
        }

        EditorGUILayout.EndHorizontal();

        if (showHelp)
        {
            EditorGUILayout.HelpBox("This script handles all the loading / unloading tasks.\nIf you have to go to " +
                "a scene or go back to main menu from your scene " +
                "or you have to move in between different scenes " +
                "just simply save the SceneName you want to load :\n\n" +
                "PlayerPrefs.SetString(sceneToLoad, yourSceneName);\n\n" +
                "and then load the loading scene like this :\n\n" +
                "Fader fader = FindObjectOfType<Fader>(); \n" +
                "fader.FadeIntoLevel(LoadingScreen); \n\n" +
                "That's it! \n" +
                "For more info please see the 'newGame()' method of " +
                "MainMenuController script!", MessageType.Info);
        }
    }

    #region First Line of Tabs

    void DrawFirstTabs()
    {
        loadingScreen.selectedTab1 = GUILayout.Toolbar(loadingScreen.selectedTab1, new string[] { "Loading Bar", "Circular Indicator" });

        switch (loadingScreen.selectedTab1)
        {
            case 0:

                loadingScreen.selectedTab2 = 555;
                loadingScreen.selectedTab3 = 555;

                loadingScreen.currentTab = "Loading Bar";
                break;
            case 1:

                loadingScreen.selectedTab2 = 555;
                loadingScreen.selectedTab3 = 555;

                loadingScreen.currentTab = "Circular Indicator";
                break;
        }

        
    }

    void DrawFirstTabsProperties()
    {
        switch (loadingScreen.currentTab)
        {
            case "Loading Bar":
                ShowProperty_LoadingBar();
                break;
            case "Circular Indicator":
                ShowProperty_CircularIndicator();
                break;
        }

    }

    void ShowProperty_LoadingBar()
    {
        EditorGUILayout.PropertyField(loadingBar);
        EditorGUILayout.PropertyField(showLoadingBar);
        EditorGUILayout.PropertyField(fillDelay);
        EditorGUILayout.PropertyField(fillSpeed);
    }

    void ShowProperty_CircularIndicator()
    {
        EditorGUILayout.PropertyField(circularIndicator);
        EditorGUILayout.PropertyField(circularLoadDelay);
        EditorGUILayout.PropertyField(circularIndicatorAnimSpeed);
    }

    #endregion

    #region Second Line of Tabs

    void DrawSecondTabs()
    {
        loadingScreen.selectedTab2 = GUILayout.Toolbar(loadingScreen.selectedTab2, new string[] { "Image Transition", "Input Prompt" });

        switch (loadingScreen.selectedTab2)
        {
            case 0:

                loadingScreen.selectedTab1 = 555;
                loadingScreen.selectedTab3 = 555;

                loadingScreen.currentTab = "Image Transition";
                break;
            case 1:

                loadingScreen.selectedTab1 = 555;
                loadingScreen.selectedTab3 = 555;

                loadingScreen.currentTab = "Input Prompt";
                break;
        }
    }

    void DrawSecondTabsProperties()
    {
        switch (loadingScreen.currentTab)
        {
            case "Image Transition":
                ShowProperty_ImageTransition();
                break;
            case "Input Prompt":
                ShowProperty_InputPrompt();
                break;
        }
    }

    void ShowProperty_ImageTransition()
    {
        EditorGUILayout.PropertyField(defaultLoadingScreenImage);
        EditorGUILayout.PropertyField(showImageTransition);
        EditorGUILayout.PropertyField(showRandomImageTransition);
        EditorGUILayout.PropertyField(LoadingScreenImages,true,null);
        EditorGUILayout.PropertyField(transitionDuration);
        EditorGUILayout.PropertyField(transitionFader);
    }

    void ShowProperty_InputPrompt()
    {
        EditorGUILayout.PropertyField(showContinueText);
        EditorGUILayout.PropertyField(PressAnyKeyToContinue);
        EditorGUILayout.PropertyField(LoadingText);
    }

    #endregion

    #region Third Line of Tabs

    void DrawThirdTab()
    {
        loadingScreen.selectedTab3 = GUILayout.Toolbar(loadingScreen.selectedTab3, new string[] { "Scene Specific Loading Image" });

        switch (loadingScreen.selectedTab3)
        {
            case 0:

                loadingScreen.selectedTab1 = 555;
                loadingScreen.selectedTab2 = 555;

                loadingScreen.currentTab = "Scene Specific Loading Image";
                break;
          
        }
    }

    void DrawThirdTabsProperties()
    {
        switch (loadingScreen.currentTab)
        {
            case "Scene Specific Loading Image":
                ShowProperty_SceneSpecific();
                break;
           
        }
    }

    void ShowProperty_SceneSpecific()
    {
        EditorGUILayout.PropertyField(sceneSpecificLoading,true,null);
    }

    #endregion


}
