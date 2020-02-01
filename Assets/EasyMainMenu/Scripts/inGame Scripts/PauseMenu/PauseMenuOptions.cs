using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenuOptions : MonoBehaviour {


    [Header("UI References")]
    public Text SelectedItemText;
    public Text SelectedItemInfoText;
    public GameObject OptionsContainer;


    // Use this for initialization
    void Start()
    {
    }
	
    public void Init()
    {

        SelectedItemText.text = "Resume";
        //edit info as well
        SelectedItemInfoText.text = "Resumes the Game.";
        OptionsContainer.SetActive(false);
    }

    /// <summary>
    /// As soon as Player hovers, the info changes
    /// </summary>
    /// <param name="name"></param>
    public void OnHoverTextChange(string name)
    {
        //chane the text name to the field selected
        SelectedItemText.text = name;
       

        switch (name)
        {
            case "Resume":
                //edit info as well
                SelectedItemInfoText.text = "Resumes the Game.";
                OptionsContainer.SetActive(false);
                break;

            case "Options":
                //edit info as well
                SelectedItemInfoText.text = "Change graphics Options.";
                OptionsContainer.SetActive(true);
                break;

            case "Main Menu":
                //edit info as well
                SelectedItemInfoText.text = "Go back to Main Menu.";
                OptionsContainer.SetActive(false);
                break;

            case "Load Game":
                //edit info as well
                SelectedItemInfoText.text = "Load previously Saved Game.";
                OptionsContainer.SetActive(false);
                break;
        }
    }

}
