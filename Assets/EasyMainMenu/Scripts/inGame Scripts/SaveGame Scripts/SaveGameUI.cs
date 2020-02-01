using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveGameUI : MonoBehaviour {

    [Header("Quick Save Key")]
    public KeyCode key;

    [Header("Save Trigger Key")]
    public KeyCode SaveKey = KeyCode.RightAlt;

    [Header("Save Trigger Data")]
    [Tooltip("No Need to Assign")]
    public string saveName;
    [Tooltip("No Need to Assign")]
    public float savePercentage;

    [Header("Save Slots UI")]
    [Tooltip("No Need to Assign")]
    public Text saveName_text;
    [Tooltip("No Need to Assign")]
    public Text savePercentage_text;

    [Header("Slot Specific Data ")]
    [Tooltip("ID WILL BE ASSIGNED AUTOMATICALLY")]
    public int slotId;
    [Tooltip("No Need to Assign")]
    public int saveTriggerId;
    [Tooltip("No Need to Assign")]
    public string sceneName;

    [HideInInspector]
    public Canvas[] allUI;

    #region Quick Save Vars
    public Text QsaveName_text;
    public Text QsavePercentage_text;
    public int QslotId;

    #endregion

    private void Awake()
    {
        if (!EasyAudioUtility.instance)
            Instantiate(Resources.Load("Prefabs/EasyAudioUtility"));
    }

    // Use this for initialization
    IEnumerator Start () {

        //if (Camera.main.GetComponent<AudioListener>())
        //    GetComponent<AudioListener>().enabled = false;

        yield return new WaitForSeconds(0.1f);
        loadQuickSaveData();
    }

    void Update()
    {
        if (Input.GetKeyDown(key)) {
            //Save Game
            SaveData_quickSlot();
            //Debug.Log("Save Game");
        }
    }

    #region Public Methods

    public void quickSaveSlotData(Text saveName, Text savePercentage, int slotID)
    {
        QsaveName_text = saveName;
        QsavePercentage_text = savePercentage;
        QslotId = slotID;

    }

    public void openConfirmation() {
        GetComponent<Animator>().Play("confirmUI_open");

        //play sound
        playClickSound();
    }

    //if yes, then save to current slot
    public void confirmation_yes()
    {
        saveData();

        //play anim
        GetComponent<Animator>().Play("confirmUI_close");

        //play sound
        playClickSound();
    }

    //get's back to save slot selection
    public void confirmation_no()
    {
        GetComponent<Animator>().Play("confirmUI_close");

        //play sound
        playClickSound();
    }

    public void closeSaveUI() {

        //disable all UI
        for (int i = 0; i < allUI.Length; i++)
        {
            allUI[i].gameObject.SetActive(true);
        }
        //un-pause game
        Time.timeScale = 1;
       
        //play UI anim
        GetComponent<Animator>().Play("saveGameUI_close");
        Invoke("disableUI", 0.2f);

        //play sfx
        playClickSound();

        GetComponent<UIController>().onUnpause.Invoke();
    }

    void disableUI() {
        GetComponent<UIController>().hideMenus();
    }
    #endregion

    void saveData() {

#if PIXELCRUSHERS_SAVESYSTEM
        PixelCrushers.SaveSystem.SaveToSlot(slotId);

#endif

        //set data in UI
        saveName_text.text = saveName;
        savePercentage_text.text = savePercentage + "%";
        //transform as well

        //saving 
        #if !EMM_ES2 
        
        PlayerPrefs.SetInt("slot_" + slotId, slotId);
        PlayerPrefs.SetString("slot_saveName_" + slotId, saveName);
        PlayerPrefs.SetFloat("slot_savePercentage_" + slotId, savePercentage);
        PlayerPrefs.SetInt("saveTriggerId_" + slotId, saveTriggerId);
        PlayerPrefs.SetString("slot_sceneName_" + slotId, sceneName);
       
        #else

        ES2.Save(slotId, "slot_" + slotId);
        ES2.Save(saveName, "slot_saveName_" + slotId);
        ES2.Save(savePercentage, "slot_savePercentage_" + slotId);
        ES2.Save(saveTriggerId, "saveTriggerId_" + slotId);
        ES2.Save(sceneName, "slot_sceneName_" + slotId);

        #endif
    }

    void SaveData_quickSlot()
    {
        //set data in UI
        QsaveName_text.text = "Quicksave : " + SceneManager.GetActiveScene().name;
        QsavePercentage_text.text = "";

#if PIXELCRUSHERS_SAVESYSTEM
        PixelCrushers.SaveSystem.SaveToSlot(QslotId);
#endif

        //saving
#if !EMM_ES2

        PlayerPrefs.SetInt("QuickSaveDataIsPresent", 1);
        PlayerPrefs.SetInt("slot_" + QslotId, QslotId);
        PlayerPrefs.SetString("slot_saveName_" + QslotId, QsaveName_text.text);
        PlayerPrefs.SetString("slot_sceneName_" + QslotId, SceneManager.GetActiveScene().name);

        #else

        ES2.Save(1, "QuickSaveDataIsPresent");
        ES2.Save(QslotId, "slot_" + QslotId);
        ES2.Save(QsaveName_text.text, "slot_saveName_" + QslotId);
        ES2.Save(SceneManager.GetActiveScene().name, "slot_sceneName_" + QslotId);

        #endif

        SavePositions();

        //play HUD animation
        Animator SaveText = GameObject.Find("SaveText").GetComponent<Animator>();
        if (!SaveText.IsInTransition(0)) {
            SaveText.Play("SaveTextHUD");
        }

    }

    /// <summary>
    /// Save Exact Positions of the player transform
    /// </summary>
    void SavePositions() {

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float x = player.position.x;
        float y = player.position.y;
        float z = player.position.z;

        #if !EMM_ES2

        PlayerPrefs.SetFloat("player.position.x", x);
        PlayerPrefs.SetFloat("player.position.y", y);
        PlayerPrefs.SetFloat("player.position.z", z);
        
        #else

        ES2.Save(x, "player.position.x");
        ES2.Save(y, "player.position.y");
        ES2.Save(z, "player.position.z");

        #endif
    }


    /// <summary>
    /// We will load the quick save data if it's present 
    /// </summary>
    void loadQuickSaveData()
    {
        //if there's data present in the QuickSaveSlot

        #if !EMM_ES2

        if (PlayerPrefs.GetInt("slotLoaded_") == QslotId && QslotId != 0)
        {
            //Load Positions
            LoadnSetPosition();
        }

        #else

        if (ES2.Exists("slotLoaded_")) {
            if (ES2.Load<int>("slotLoaded_") == QslotId && QslotId != 0)
            {
                //Load Positions
                LoadnSetPosition();
            }
        }

        #endif

    }


    /// <summary>
    /// Load the position
    /// AND
    /// Set the position
    /// </summary>
    void LoadnSetPosition()
    {

        #if !EMM_ES2

        float x = PlayerPrefs.GetFloat("player.position.x");
        float y = PlayerPrefs.GetFloat("player.position.y");
        float z = PlayerPrefs.GetFloat("player.position.z");

        #else

        float x = ES2.Load<float>("player.position.x");
        float y = ES2.Load<float>("player.position.y");
        float z = ES2.Load<float>("player.position.z");

        #endif

        Vector3 loadedPosition = new Vector3(x, y, z);

        //now find player!
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = loadedPosition;

    }

    #region Sounds
    public void playHoverClip()
    {
        EasyAudioUtility.instance.Play("Hover");
       
    }

   public void playClickSound()
    {
        EasyAudioUtility.instance.Play("Click");
    }

#endregion

}
