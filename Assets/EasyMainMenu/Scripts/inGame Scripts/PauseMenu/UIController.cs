using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    [Tooltip("Usee Blur in Pause Menu?")]
    public bool useBlur;

    public bool forceCursorOnWhilePause;

    [Header("Both UI Panels")]
    public GameObject saveMenu;
    public GameObject pauseMenu;
    Fader fader;
    [HideInInspector]
    public bool isOpen;
    Canvas[] allUI;

    [Header("Pause Game and Resume Game Events")]
    public UnityEngine.Events.UnityEvent onPause = new UnityEngine.Events.UnityEvent();
    public UnityEngine.Events.UnityEvent onUnpause = new UnityEngine.Events.UnityEvent();

    [HideInInspector]
    public List<LoadSlotIdentifier> loadSlots;

    [HideInInspector]
    public bool usingUFPS = false;

    // Use this for initialization
    IEnumerator Start () {

        //find fader
        fader = FindObjectOfType<Fader>();

        yield return new WaitForSeconds(0.5f);

    }

    // Update is called once per frame
    void Update () {

        //if using UFPS
        if (usingUFPS)
            return; //exit

        if ((forceCursorOnWhilePause) && (isOpen || saveMenu.active))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        //if save menu is not opened
        if (!saveMenu.active && canOpen())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isOpen)
                    openPauseMenu();
                else
                    closePauseMenu();
            }
        }
	}

    public void openPauseMenu() {

        allUI = FindObjectsOfType<Canvas>();
        //disable all UI
        for (int i = 0; i < allUI.Length; i++)
        {
            if (allUI[i].name != "Fader")
                allUI[i].gameObject.SetActive(false);
        }

        saveMenu.SetActive(false);
        pauseMenu.SetActive(true);

        //play sound
        GetComponent<SaveGameUI>().playClickSound();

        //play anim
        GetComponent<Animator>().Play("OpenPauseMenu");

        //time = almost 0
        if(!usingUFPS)
            Time.timeScale = 0.0001f;

        isOpen = true;

        //init pause menu options
        GetComponent<PauseMenuOptions>().Init();

        //enable blur
        if (useBlur)
        {
            if (Camera.main.GetComponent<Animator>())
                Camera.main.GetComponent<Animator>().Play("BlurOff");

        }

        onPause.Invoke();

    }


    public void closePauseMenu() {

        //enable all UI
        for (int i = 0; i < allUI.Length; i++)
        {
           allUI[i].gameObject.SetActive(true);
        }

        //time = 1
        if(!usingUFPS)
            Time.timeScale = 1;

        //play sound
        GetComponent<SaveGameUI>().playClickSound();

        //play anim
        GetComponent<Animator>().Play("ClosePauseMenu");
        // hideMenus();

        isOpen = false;

        //enable blur
        if (useBlur)
        {
            if(Camera.main.GetComponent<Animator>())
            Camera.main.GetComponent<Animator>().Play("BlurOff");

        }

        onUnpause.Invoke();

    }

    public void hideMenus() {
        saveMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void goToMainMenu() {
        //delete player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);

        //restore time scale (important)
        Time.timeScale = 1f;

        //load main menu
        #if !EMM_ES2
        PlayerPrefs.SetString("sceneToLoad", "");
        #else
        PlayerPrefs.SetString("sceneToLoad", "");
        ES2.Save("", "sceneToLoad");
        #endif

        //hide all menus
        hideMenus();

        //load level via fader
        fader.FadeIntoLevel("LoadingScreen");
    }

    public void openLoadGame() {
        GetComponent<Animator>().Play("loadGameOpen");
        initLoadGameMenu();
    }

    public void closeLoadGame() {
        GetComponent<Animator>().Play("loadGameClose");
    }

    void initLoadGameMenu() {
        if (loadSlots.Count > 0)
        {
            foreach (LoadSlotIdentifier lsi in loadSlots)
            {
                lsi.Init();
            }
        }

    }

    [HideInInspector]
    public bool openPMenu = true;
    public bool canOpen()
    {
        return openPMenu;
    }


}
