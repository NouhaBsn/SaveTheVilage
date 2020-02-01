using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectItem : MonoBehaviour {

    [Header("UI References")]
    public Image LevelImage;
    public Text levelCount;
    public Text levelName;
    public Text levelSubName;
    public GameObject lockIcon;
    public bool isLocked;
    public string levelToLoad;

	// Use this for initialization
	void Start () {
		
	}

    /// <summary>
    /// Sets the currect level item details
    /// </summary>
    /// <param name="levelImg">Level Image it should display</param>
    /// <param name="levelC">Level Number</param>
    /// <param name="levelNm">Level Name</param>
    /// <param name="hasSubName">if this level has a sub name</param>
    /// <param name="levelSubName">if it is, what is subname</param>
    public void setLevelItem(Sprite levelImg, int levelC,string levelNm, bool hasSubName,
                string levelSubName, string levelToLoad, bool locked) {

        //setting retrieved data
        LevelImage.sprite = levelImg;
        levelCount.text = levelC + "";
        levelName.text = levelNm;
        //if it has a subname, we mntion it
        if (hasSubName)
        {
            this.levelSubName.text = levelSubName;
        }
        //else we disable the object
        else
        {
            this.levelSubName.gameObject.SetActive(false);
        }

        isLocked = locked;

        //finally storing which level this item loads
        this.levelToLoad = levelToLoad;

        //enable/disable lock icon
        if (isLocked)
            lockIcon.SetActive(true);
        else
            lockIcon.SetActive(false);
    }

    public void loadThisLevel()
    {
        if (!isLocked)
        {
            //loads a specific scene
            #if !EMM_ES2
            PlayerPrefs.SetString("sceneToLoad", levelToLoad);
            #else
            ES2.Save(levelToLoad, "sceneToLoad");
            #endif

            //load level via fader
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeIntoLevel("LoadingScreen");
        }

    }
}
