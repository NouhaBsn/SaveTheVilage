using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSlotIdentifier : MonoBehaviour {

    [Header("Slot specific Variables")]
    [Tooltip("if true, this Slot will become the quick save slot!")]
    public bool quickSaveSlot;
    [Tooltip("Unique Slot ID used to identify while loading")]
    public int slotId;
    [Tooltip("This Slot's name field")]
    public Text saveName_text;
    [Tooltip("This Slot's percentage field")]
    public Text savePercentage_text;
    [Tooltip("Scene to load")]
    public string sceneToLoad;

    Button button;

    // Use this for initialization
    void Awake () {

        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(LoadSceneSaved);

        Init();

        //if this is the gameplay scene, send data
        UIController controller = FindObjectOfType<UIController>();
        if (controller != null)
        {
            controller.loadSlots.Add(this);
        }
    }

    //retrieve saved data
    public void Init() {
        //first load slot data
        loadSlotData();
        
    }

    void loadSlotData()
    {
        //get key
        #if !EMM_ES2
        quickSaveSlot = PlayerPrefs.GetInt("quickSaveSlot") == slotId ? true : false;

#else
        if(ES2.Exists("quickSaveSlot"))
            quickSaveSlot = ES2.Load<int>("quickSaveSlot") == slotId ? true : false; ;

#endif

        //if it's quick save make it different
        //change color
        if (quickSaveSlot)
        {
            //change color
            Color c = Color.red;
            c.a = 0.25f;
            if (GetComponent<Image>())
                GetComponent<Image>().color = c;
            else if (button)
            {
                Color cyan = Color.cyan;
                cyan.a = 0.25f;
                ColorBlock _cb = button.colors;
                _cb.normalColor = cyan;
                button.colors = _cb;
            }
        }

        //if there's already data present on this slot
#if !EMM_ES2
        if (PlayerPrefs.GetInt("slot_" + slotId) == slotId)
        {
            //then load it and set it at UI
            saveName_text.text = PlayerPrefs.GetString("slot_saveName_" + slotId);
            sceneToLoad = PlayerPrefs.GetString("slot_sceneName_" + slotId);

            if (!quickSaveSlot)
                savePercentage_text.text = PlayerPrefs.GetFloat("slot_savePercentage_" + slotId) + "%";
            else
                savePercentage_text.text = "";

        }
#else
        if (ES2.Exists("slot_" + slotId)) {

            if (ES2.Load<int>("slot_" + slotId) == slotId)
            {
                //then load it and set it at UI
                saveName_text.text = ES2.Load<string>("slot_saveName_" + slotId);
                sceneToLoad = ES2.Load<string>("slot_sceneName_" + slotId);

                if (!quickSaveSlot)
                    savePercentage_text.text = ES2.Load<float>("slot_savePercentage_" + slotId) + "%";
                else
                    savePercentage_text.text = "";

            }
        }
        
#endif

    }

    public void LoadSceneSaved()
    {

#if PIXELCRUSHERS_SAVESYSTEM

        if (PixelCrushers.SaveSystem.HasSavedGameInSlot(slotId))
        {
            Time.timeScale = 1;

            //load level via fader
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeIntoLevel("LoadingScreen");

            //save which slot is loaded
            PlayerPrefs.SetInt("slotLoaded_", slotId);

            //loads a specific scene
            PlayerPrefs.SetString("sceneToLoad", sceneToLoad);

            
            return;
        }

#endif

            //if there's a save game present at this slot
            if (sceneToLoad != "")
        {
            Time.timeScale = 1;

            //delete player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player)
                Destroy(player);

#if !EMM_ES2

            //save which slot is loaded
            PlayerPrefs.SetInt("slotLoaded_", slotId);

            //loads a specific scene
            PlayerPrefs.SetString("sceneToLoad", sceneToLoad);

#else

            //save which slot is loaded
            ES2.Save(slotId, "slotLoaded_");
            
            //loads a specific scene
            ES2.Save(sceneToLoad, "sceneToLoad");

#endif

            //load level via fader
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeIntoLevel("LoadingScreen");

            
        }
    }
}
