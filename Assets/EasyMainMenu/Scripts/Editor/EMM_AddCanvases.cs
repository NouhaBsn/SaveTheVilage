using UnityEditor;
using UnityEngine;

public class EMM_AddCanvases : EditorWindow{


    [MenuItem("EMM/Add/Loading Canvas &#L", false)]
    public static void AddLoadingCanvas()
    {
        //instantiate ui canvas
        GameObject loadingCanvas = Instantiate(Resources.Load("Prefabs/loadingCanvas")) as GameObject;
        //rename it
        loadingCanvas.name = "Loading Canvas";

        //see if the scene already contains a cam
        Camera cam = FindObjectOfType<Camera>();
        if (!cam)
        {
            //if no cam
            GameObject sceneCam = new GameObject();
            sceneCam.AddComponent<Camera>();
            sceneCam.AddComponent<AudioListener>();
            sceneCam.name = "Scene Camera";

        }

        //instantiate EAU
        GameObject EAU = Instantiate(Resources.Load("Prefabs/EasyAudioUtility")) as GameObject;
        //rename it
        EAU.name = "EasyAudioUtility";

        Debug.Log("Loading Canvas Created!");
    }

    [MenuItem("EMM/Add/Main Menu Canvas  &#M", false)]
    public static void AddMainMenuCanvas()
    {
        //instantiate ui canvas
        GameObject mainMenu = Instantiate(Resources.Load("Prefabs/MainMenu")) as GameObject;
        //rename it
        mainMenu.name = "Main Menu";

        //instantiate EAU
        GameObject EIU = Instantiate(Resources.Load("Prefabs/EasyInputUtility")) as GameObject;
        //rename it
        EIU.name = "EasyInputUtility";

        GameObject bgImg = Instantiate(Resources.Load("Prefabs/BackgroundImageCamera")) as GameObject;
        //rename it
        bgImg.name = "Background Image";

        Debug.Log("Main Menu Created!");
    }

    [MenuItem("EMM/Add/Gameplay UI &#G", false)]
    public static void AddGameplayUI() {

        //instantiate ui canvas
        GameObject gameplayUI = Instantiate(Resources.Load("Prefabs/Gameplay UI")) as GameObject;
        //rename it
        gameplayUI.name = "Gameplay UI";

        Debug.Log("Gameplay UI Created!");

    }

    [MenuItem("EMM/Add/Save Game Trigger &#T", false)]
    public static void AddSaveGameTrigger()
    {
        //instantiate object
        GameObject saveGameTrigger = Instantiate(Resources.Load("Prefabs/SaveGameTrigger")) as GameObject;
        //rename it
        saveGameTrigger.name = "SaveGameTrigger";

        Debug.Log("Save game Trigger Created!");

    }

    [MenuItem("EMM/Demo/Simple Cube Character", false)]
    public static void AddSimpleCube()
    {
        //instantiate object
        GameObject cube = Instantiate(Resources.Load("Prefabs/SimpleCube")) as GameObject;
        //rename it
        cube.name = "Sample Cube";
    
    }

    [MenuItem("EMM/Demo/Sample Maze Scene", false)]
    public static void AddSampleMazeScene()
    {
        //instantiate maze scene
        GameObject MazeScene = Instantiate(Resources.Load("Prefabs/MazeScene")) as GameObject;
        //rename it
        MazeScene.name = "MazeScene";
    }

    [MenuItem("EMM/Clear Game Data &#X", false)]
    public static void ResetPlayerPref()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Game Data Cleared!");
    }
}
