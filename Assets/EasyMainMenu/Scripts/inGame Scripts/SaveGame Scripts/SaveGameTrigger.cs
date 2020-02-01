using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveGameTrigger : MonoBehaviour {

    #region variables

    Animator anim;

    [Header("All other UI")]
    [Tooltip("No Need to Assign")]
    public Canvas[] allUI;

    [Header("References to UI")]
    [Tooltip("No Need to Assign")]
    public GameObject saveUI;
    [Tooltip("No Need to Assign")]
    public Text saveName_Txt;
    [Tooltip("No Need to Assign")]
    public Text savePercentage_Txt;

    [Header("Edit these in Inspector According to your level")]
    [Tooltip("Save Name which will be displayed in respective Save Slots")]
    public string saveName;
    [Tooltip("Game Completion percentage displayed in respective Save Slots")]
    public float savePercentage;
    [Tooltip("Scene to Load")]
    public string sceneName;
    [Tooltip("Player will spawn from this point when level loads")]
    public Transform spawnPoint;
    [Tooltip("Unique trigger ID to see is this the last save point")]
    public int saveTriggerId;
    [Tooltip("Debug to spawn player at this Trigger's spawnPoint")]
    public bool debugSpawn;

    #endregion

    // Use this for initialization
    void Start () {
        saveUI = GameObject.FindObjectOfType<SaveGameUI>().gameObject;
        //saveUI.SetActive(false);

        anim = saveUI.GetComponent<Animator>();

        //checking is this the trigger where the player saved the game

    }

    void OnTriggerStay(Collider col)
    {
        //if player has entered trigger
        if (col.tag == "Player" && Input.GetKeyDown(saveUI.GetComponent<SaveGameUI>().SaveKey))
        {
            //find all UIs
            allUI = GameObject.FindObjectsOfType<Canvas>();
            //setting it in saveUI
            saveUI.GetComponent<SaveGameUI>().allUI = allUI;

            //disable all UI
            for (int i = 0; i < allUI.Length; i++) {
                allUI[i].gameObject.SetActive(false);
            }
            //pause game
            Time.timeScale = 0.0000001f;
            //enable UI
            saveUI.SetActive(true);
            //play UI anim
            anim.Play("saveGameUI_open");
            //retrieve scene name
            sceneName = SceneManager.GetActiveScene().name;

            //sending data to save UI
            sendCurrentSavePointData();

            //CALL ON PAUSE events
            FindObjectOfType<UIController>().onPause.Invoke();
            
        }
    }

    void sendCurrentSavePointData() {
        saveUI.GetComponent<SaveGameUI>().saveName = saveName;
        saveUI.GetComponent<SaveGameUI>().savePercentage = savePercentage;
        saveUI.GetComponent<SaveGameUI>().saveTriggerId = saveTriggerId;
        saveUI.GetComponent<SaveGameUI>().sceneName = sceneName;

    }

    private void Update()
    {
        if (debugSpawn)
        {
            FindObjectOfType<LoadGameManager>().spawnPlayerAtPoint(spawnPoint);
            debugSpawn = false;
        }
    }

}
