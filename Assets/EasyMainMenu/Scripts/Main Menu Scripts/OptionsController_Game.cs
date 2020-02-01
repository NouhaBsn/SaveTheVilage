using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class OptionsController_Game : MonoBehaviour {

    #region Variables

    [Header("_Game Options_")]

    [Space(5)]
    [Tooltip("HUD GameObject name in scene")]
    public string HUD_name;
    public Text HUD_text;
    public int toggleHud = 1;

    [Space(10)]
    public Slider contrast_slider;
    [HideInInspector]
    public float contrastValue;

    [Space(10)]
    [HideInInspector]
    public float brightnessValue;
    public Slider brightness_slider;

    [Space(10)]
    [HideInInspector]
    public float musicValue;
    [Tooltip("Music Source to control")]
    public AudioSource musicSource;
    public Slider music_slider;

    [Space(10)]
    [HideInInspector]
    public float soundValue;
    [Tooltip("Audio Sources to control")]
    public EasyAudioUtility_Helper[] soundSource;
    public Slider sound_slider;

    #endregion

    // Use this for initialization
    IEnumerator Start () {

        yield return new WaitForEndOfFrame();
        //load game settings on start 
        game_setDefaults();

    }

    #region _Game Options_

    /// <summary>
    /// Toggles the HUD on-off
    /// </summary>
    public void game_toggleHUD() {

        //enable
        if(toggleHud == 0)
        {
            toggleHud = 1;
            HUD_text.text = "On";
            //removing HUD name so that it can't be disabled 
            #if !EMM_ES2
            PlayerPrefs.SetString("HUD_name", "");
            #else
            ES2.Save("", "HUD_name");
            #endif

        }
        //disable
        else
        {
            toggleHud = 0;
            HUD_text.text = "Off";
            //saving HUD name so that it can be disabled 
            #if !EMM_ES2
            PlayerPrefs.SetString("HUD_name", HUD_name);
            #else
            ES2.Save(HUD_name, "HUD_name");
            #endif

           
        }

        //override new setting
        #if !EMM_ES2
        PlayerPrefs.SetInt("toggleHud", toggleHud);
        #else
        ES2.Save(toggleHud, "toggleHud");
        #endif
       
        //play click sound
        EasyAudioUtility.instance.Play("Hover");

    }

    /// <summary>
    ///  Only sets the value
    /// </summary>
    void game_setHUD() {
        //disable
        if (toggleHud == 0)
        {
            HUD_text.text = "Off";
            
            //removing HUD name so that it can't be disabled 
            #if !EMM_ES2
            PlayerPrefs.SetString("HUD_name", "");
            #else
            ES2.Save("", "HUD_name");
            #endif

        }
        //enable
        else
        {
            HUD_text.text = "On";
            //saving HUD name so that it can be disabled 
            #if !EMM_ES2
            PlayerPrefs.SetString("HUD_name", HUD_name);
            #else
            ES2.Save("", "HUD_name");
            #endif
            
        }
    }

    #region Brightness

    ///// <summary>
    ///// Sets the contrast
    ///// </summary>
    public void game_Contrast()
    {

        //directly finding and setting the value of the contrast
       // FindObjectOfType<BrightnessEffect>()._Contrast = contrast_slider.value;

        contrastValue = contrast_slider.value;

        //override new setting
        #if !EMM_ES2
        PlayerPrefs.SetFloat("contrastValue", contrastValue);
        #else
        ES2.Save(contrastValue, "contrastValue");
        #endif

    }

    ///// <summary>
    /////  Only sets the value
    ///// </summary>
    void game_setContrast()
    {
        //setting default
        if (contrastValue == 0)
            contrastValue = 1;

        //directly finding and setting the value of the contrast
       // FindObjectOfType<BrightnessEffect>()._Contrast = contrastValue;
        //setting slider value as well
        contrast_slider.value = contrastValue;
    }

    ///// <summary>
    ///// Sets the contrast
    ///// </summary>
    public void game_Brightness()
    {

        //directly finding and setting the value of the contrast
       // FindObjectOfType<BrightnessEffect>()._Brightness = brightness_slider.value;

        brightnessValue = brightness_slider.value;

        //override new setting
        #if !EMM_ES2
        PlayerPrefs.SetFloat("brightnessValue", brightnessValue);
        #else
        ES2.Save(brightnessValue, "brightnessValue");
        #endif

    }

    ///// <summary>
    /////  Only sets the value
    ///// </summary>
    void game_setBrightness()
    {
        //setting default
        if (brightnessValue == 0)
            brightnessValue = 1;

        //directly finding and setting the value of the contrast
       // FindObjectOfType<BrightnessEffect>()._Brightness = brightnessValue;
        //setting slider value as well
        brightness_slider.value = brightnessValue;
    }
    #endregion

    /// <summary>
    /// Sets the Music Volume
    /// </summary>
    public void game_Music()
    {
        musicSource.volume = music_slider.value;
        musicValue = music_slider.value;

        //override new setting
        #if !EMM_ES2
        PlayerPrefs.SetFloat("musicValue", musicValue);
        #else
        ES2.Save(musicValue, "musicValue");
        #endif
    }

    /// <summary>
    ///  Only sets the value
    /// </summary>
    void game_setMusic()
    {

        //finding correct Audio Source
        EasyAudioUtility am = FindObjectOfType<EasyAudioUtility>();
        for (int i = 0; i < am.helper.Length; i++)
        {
            if (am.helper[i].name == "BG")
            {
                musicSource = am.helper[i].source;

                if (!musicSource.isPlaying)
                    musicSource.Play();
            }
        }

        musicSource.volume = musicValue;
        music_slider.value = musicValue;


    }

    /// <summary>
    /// Sets the Music Volume
    /// </summary>
    public void game_Sound()
    {
        
        foreach(EasyAudioUtility_Helper s in soundSource)
        {

            s.volume = sound_slider.value;
            soundValue = sound_slider.value;
        }

        //override new setting
        #if !EMM_ES2
        PlayerPrefs.SetFloat("soundValue", soundValue);
        #else
        ES2.Save(soundValue, "soundValue");
        #endif
    }

    /// <summary>
    ///  Only sets the value
    /// </summary>
    void game_setSound()
    {

        //finding Audio source once
        EasyAudioUtility am = FindObjectOfType<EasyAudioUtility>();
        for(int i = 0; i < am.helper.Length; i++)
        {
            //define all the sounds present in the Easy Audio Utility
            if (am.helper[i].name == "Hover")
                soundSource[i] = am.helper[i];

            if (am.helper[i].name == "Click")
                soundSource[i] = am.helper[i];

        }

        foreach (EasyAudioUtility_Helper s in soundSource)
        {

            s.volume = soundValue;
            sound_slider.value = soundValue;
        }

    }

    /// <summary>
    /// Sets the default values on Start
    /// </summary>
    void game_setDefaults() {
        //retrieve defaults saved

#if !EMM_ES2
        
        toggleHud = PlayerPrefs.GetInt("toggleHud");
        contrastValue = PlayerPrefs.GetFloat("contrastValue",1);
        brightnessValue = PlayerPrefs.GetFloat("brightnessValue",1);
        musicValue = PlayerPrefs.GetFloat("musicValue",1);
        soundValue = PlayerPrefs.GetFloat("soundValue",1);
        
#else
        toggleHud = ES2.Exists("toggleHud") ? ES2.Load<int>("toggleHud") : 0 ;
        contrastValue = ES2.Exists("contrastValue") ? ES2.Load<float>("contrastValue") : 1 ;
        brightnessValue = ES2.Exists("brightnessValue") ? ES2.Load<float>("brightnessValue") : 1 ;
        musicValue = ES2.Exists("musicValue") ? ES2.Load<float>("musicValue") : 1 ;
        soundValue = ES2.Exists("soundValue") ? ES2.Load<float>("soundValue") : 1 ;


        #endif
        //set values accordingly
        game_setHUD();
        game_setMusic();
        game_setSound();

        /// UN-COMMENT THESE METHODS ONLY IF YOU HAVE :
        /// 1. Imported the Brightness Effect from Standard Assets
        /// 2. Extracted the Brightness Scripts from Brightness.rar located in 'Brightness' Folder
        // game_setContrast();
        // game_setBrightness();
    }

#endregion
}
