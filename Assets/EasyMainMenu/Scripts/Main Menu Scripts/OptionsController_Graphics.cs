using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController_Graphics : MonoBehaviour {

    #region Variables

    [Header("_Graphics Options_")]
    [Space(5)]
    public Text toggleFullscreen_text;
    [HideInInspector]
    public int toggleFullscreen;
    [Space(10)]

    [HideInInspector]
    public int toggleAnisoFilt;
    public Text AnisoFiltering_text;
    [Space(10)]

    [HideInInspector]
    public int toggleAntiAlias;
    public Text AntiAlias_text;
    [Space(10)]

    [HideInInspector]
    public int toggleVsync;
    public Text toggleVsync_text;
    [Space(10)]

    [HideInInspector]
    public int toggleShadows;
    public Text toggleShadows_text;
    [Space(10)]

    [HideInInspector]
    public int toggleTextureQuality;
    public Text toggleTextureQuality_text;
    [Space(10)]

    [HideInInspector]
    public int currentScreenResolutionCount;
    public Text currentScreenResolution_text;
    Resolution[] allScreenResolutions;
    #endregion

    // Use this for initialization
    void Start () {

        //load graphics settings on start 
        gfx_setDefaults();
	}

    #region _Graphics Options_

    /// <summary>
    /// Toggle Fullscreen
    /// </summary>
    public void gfx_fullScreen()
    {
        //perform toggle
        if (toggleFullscreen == 0)
        {
            toggleFullscreen = 1;
            toggleFullscreen_text.text = "Yes";
        }
        else
        {
            toggleFullscreen = 0;
            toggleFullscreen_text.text = "No";
        }

        //set values
        Screen.SetResolution(Screen.width, Screen.height, toggleFullscreen == 1 ? true : false);

        //override new setting
        #if !EMM_ES2
        PlayerPrefs.SetInt("toggleFullscreen", toggleFullscreen);
        #else
        ES2.Save(toggleFullscreen, "toggleFullscreen");
        #endif
       

        //play click sound
        EasyAudioUtility.instance.Play("Hover");
    }

    /// <summary>
    /// only set the value
    /// </summary>
    void gfx_setFullScreen()
    {
        //set values
        Screen.SetResolution(Screen.width, Screen.height, toggleFullscreen == 1 ? true : false);

        //set text
        toggleFullscreen_text.text = toggleFullscreen == 1 ? "Yes" : "No";
    }

    /// <summary>
    /// Toggle AnisoFiltering
    /// </summary>
    public void gfx_AnisoFiltering()
    {

        //disable
        if (toggleAnisoFilt == 0) {
            toggleAnisoFilt = 1;
            AnisoFiltering_text.text = "On";
        }
        //enable
        else {
            toggleAnisoFilt = 0;
            AnisoFiltering_text.text = "Off";
        }

        //setting value
        QualitySettings.anisotropicFiltering = toggleAnisoFilt == 1 ? AnisotropicFiltering.Enable : AnisotropicFiltering.Disable;

        //override new setting
        #if !EMM_ES2
         PlayerPrefs.SetInt("toggleAnisoFilt", toggleAnisoFilt);
        #else
        ES2.Save(toggleAnisoFilt, "toggleAnisoFilt");
        #endif

        //play click sound
        EasyAudioUtility.instance.Play("Hover");
    }

    /// <summary>
    /// Only set the value
    /// </summary>
    void gfx_setAnisoFiltering() {
    
        //if 1 = enable : disable
        QualitySettings.anisotropicFiltering = toggleAnisoFilt == 1 ? AnisotropicFiltering.Enable : AnisotropicFiltering.Disable;

        //set text
        AnisoFiltering_text.text = toggleAnisoFilt == 1 ? "On" : "Off" ;
    }

    /// <summary>
    /// Toggle AntiAliasing
    /// </summary>
    public void gfx_AntiAlias()
    {
       
        if (toggleAntiAlias == 0)
        {
            QualitySettings.antiAliasing = 2;
            toggleAntiAlias = 1;
            AntiAlias_text.text = "2x";
        }
        else if (toggleAntiAlias == 1)
        {
            QualitySettings.antiAliasing = 4;
            toggleAntiAlias = 2;
            AntiAlias_text.text = "4x";
        }
        else if (toggleAntiAlias == 2)
        {
            QualitySettings.antiAliasing = 8;
            toggleAntiAlias = 3;
            AntiAlias_text.text = "8x";
        }
        else if (toggleAntiAlias == 3)
        {
            QualitySettings.antiAliasing = 0;
            toggleAntiAlias = 0;
            AntiAlias_text.text = "Off";
        }

        //override new setting
        #if !EMM_ES2
         PlayerPrefs.SetInt("toggleAntiAlias", toggleAntiAlias);
        #else
        ES2.Save(toggleAntiAlias, "toggleAntiAlias");
        #endif

        //play click sound
        EasyAudioUtility.instance.Play("Hover");
    }

    /// <summary>
    /// Only set the value
    /// </summary>
    void gfx_setAntiAlias() {


        if (toggleAntiAlias == 0)
        {
            QualitySettings.antiAliasing = 0;
            AntiAlias_text.text = "Off";
        }
        else if (toggleAntiAlias == 1)
        {
            QualitySettings.antiAliasing = 2;
            AntiAlias_text.text = "2x";
        }
        else if (toggleAntiAlias == 2)
        {
            QualitySettings.antiAliasing = 4;
            AntiAlias_text.text = "4x";
        }
        else if (toggleAntiAlias == 3)
        {
            QualitySettings.antiAliasing = 8;
            AntiAlias_text.text = "8x";
        }
    }

    /// <summary>
    /// Toggle vSync
    /// </summary>
    public void gfx_Vsync()
    {
        if (toggleVsync == 0)
        {
            QualitySettings.vSyncCount = 1;
            toggleVsync = 1;
            toggleVsync_text.text = "On";
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            toggleVsync = 0;
            toggleVsync_text.text = "Off";
        }

        //override new setting
        #if !EMM_ES2
        PlayerPrefs.SetInt("toggleVsync", toggleVsync);
        #else
        ES2.Save(toggleVsync, "toggleVsync");
        #endif

        //play click sound
        EasyAudioUtility.instance.Play("Hover");
    }

    /// <summary>
    /// Only set the value
    /// </summary>
    void gfx_setVsync()
    {
        QualitySettings.vSyncCount = toggleVsync == 1 ? 1 : 0;

        toggleVsync_text.text = toggleVsync == 1 ? "On" : "Off";

    }

    /// <summary>
    /// Toggle Shadows
    /// </summary>
    public void gfx_shadows()
    {

        //disable shadows
        if (toggleShadows == 0) {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            toggleShadows = 1;
            toggleShadows_text.text = "Hard";
        }
        //Hard shadows
        else if (toggleShadows == 1)
        {
            QualitySettings.shadows = ShadowQuality.All;
            toggleShadows = 2;
            toggleShadows_text.text = "Soft";
        }
        //Soft shadows
        else if (toggleShadows == 2)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            toggleShadows = 0;
            toggleShadows_text.text = "Off";
        }
        //override new setting

        #if !EMM_ES2
        PlayerPrefs.SetInt("toggleShadows", toggleShadows);
        #else
        ES2.Save(toggleShadows, "toggleShadows");
        #endif

        //play click sound
        EasyAudioUtility.instance.Play("Hover");

    }

    /// <summary>
    /// Only sets the value
    /// </summary>
    void gfx_setShadows() {

        if (toggleShadows == 0)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            toggleShadows_text.text = "Off";
        }
        //Hard shadows
        else if (toggleShadows == 1)
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            toggleShadows_text.text = "Hard";
        }
        //Soft shadows
        else if (toggleShadows == 2)
        {
            QualitySettings.shadows = ShadowQuality.All;
            toggleShadows_text.text = "Soft";
        }
    }

    /// <summary>
    /// Toggle Different Texture Resolutions
    /// </summary>
    public void gfx_textureQuality()
    {
        //0 = full 
        //1 = half
        //2 = quarter

        if (toggleTextureQuality == 0)
        {
            QualitySettings.masterTextureLimit = 1;
            toggleTextureQuality_text.text = "Half";
            toggleTextureQuality = 1;
        }
        else if (toggleTextureQuality == 1)
        {
            QualitySettings.masterTextureLimit = 2;
            toggleTextureQuality_text.text = "Quarter";
            toggleTextureQuality = 2;
        }
        else if (toggleTextureQuality == 2)
        {
            QualitySettings.masterTextureLimit = 0;
            toggleTextureQuality_text.text = "Full";
            toggleTextureQuality = 0;
        }

        //override new setting
        #if !EMM_ES2
        PlayerPrefs.SetInt("toggleTextureQuality", toggleTextureQuality);
        #else
        ES2.Save(toggleTextureQuality, "toggleTextureQuality");
        #endif

        //play click sound
        EasyAudioUtility.instance.Play("Hover");

    }

   /// <summary>
    /// Only sets the value
    /// </summary>
    void gfx_setTextureQuality() {
        //0 = full
        if (toggleTextureQuality == 0)
        {
            QualitySettings.masterTextureLimit = 0;
            toggleTextureQuality_text.text = "Full";
        }
        //1 = half
        else if (toggleTextureQuality == 1)
        {
            QualitySettings.masterTextureLimit = 1;
            toggleTextureQuality_text.text = "Half";
        }
        //2 = quarter
        else if (toggleTextureQuality == 2)
        {
            QualitySettings.masterTextureLimit = 2;
            toggleTextureQuality_text.text = "Quarter";
        }
    }

    /// <summary>
    /// Scroll through various screen resolutions
    /// </summary>
    public void gfx_ScreenResolution()
    {
        //if the count is less, it means we can increase more resolution
       if(currentScreenResolutionCount < allScreenResolutions.Length)
        {
            Screen.SetResolution(allScreenResolutions[currentScreenResolutionCount].width,
               allScreenResolutions[currentScreenResolutionCount].height, toggleFullscreen == 1 ? true : false);
            
            //increment so that we increase it next time
            currentScreenResolutionCount++;
        }
        else
        {
            //start the count from zero
            currentScreenResolutionCount = 0;
            Screen.SetResolution(allScreenResolutions[currentScreenResolutionCount].width,
              allScreenResolutions[currentScreenResolutionCount].height, toggleFullscreen == 1 ? true : false);
        }

        //set text finally
        currentScreenResolution_text.text = Screen.currentResolution.width + " x " + Screen.currentResolution.height;

        //save finally
        #if !EMM_ES2
        PlayerPrefs.SetInt("currentScreenResolutionCount", currentScreenResolutionCount);
        #else
        ES2.Save(currentScreenResolutionCount, "currentScreenResolutionCount");
        #endif


    }

    /// <summary>
    /// Set the value of last saved screen resolution
    /// </summary>
    void gfx_setScreenResolution()
    {
        //retrieve all resolutions on start
        allScreenResolutions = Screen.resolutions;
        
        //set resolution
        // if the resolution has been set before
        if (PlayerPrefs.HasKey("currentScreenResolutionCount"))
        {
            Screen.SetResolution(allScreenResolutions[currentScreenResolutionCount].width,
                allScreenResolutions[currentScreenResolutionCount].height, toggleFullscreen == 1 ? true : false);
           
            //set text finally
            currentScreenResolution_text.text = allScreenResolutions[currentScreenResolutionCount].width + " x " 
                        + allScreenResolutions[currentScreenResolutionCount].height;
        }
        else
        {

            //set current resolution of the system
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height,
                                toggleFullscreen == 1 ? true : false);
        }

        //set text finally
        currentScreenResolution_text.text = Screen.currentResolution.width + " x "  + Screen.currentResolution.height;
    }

    /// <summary>
    /// sets Default gfx settings 
    /// </summary>
    void gfx_setDefaults()
    {
        //retrieve previous setting
        #if !EMM_ES2

        toggleFullscreen = PlayerPrefs.GetInt("toggleFullscreen",1); 
        toggleAnisoFilt = PlayerPrefs.GetInt("toggleAnisoFilt",1); 
        toggleVsync = PlayerPrefs.GetInt("toggleVsync",1); 
        toggleShadows = PlayerPrefs.GetInt("toggleShadows",1);
        toggleTextureQuality = PlayerPrefs.GetInt("toggleTextureQuality",1);
        toggleAntiAlias = PlayerPrefs.GetInt("toggleAntiAlias",1);
        currentScreenResolutionCount = PlayerPrefs.GetInt("currentScreenResolutionCount", currentScreenResolutionCount);

        #else

        toggleFullscreen = ES2.Exists("toggleFullscreen") ? ES2.Load<int>("toggleFullscreen") : 1;
        toggleAnisoFilt = ES2.Exists("toggleAnisoFilt") ? ES2.Load<int>("toggleAnisoFilt") : 1;
        toggleVsync = ES2.Exists("toggleVsync") ? ES2.Load<int>("toggleVsync") : 1;
        toggleShadows = ES2.Exists("toggleShadows") ? ES2.Load<int>("toggleShadows") : 1;
        toggleTextureQuality = ES2.Exists("toggleTextureQuality") ? ES2.Load<int>("toggleTextureQuality") : 1;
        toggleAntiAlias = ES2.Exists("toggleAntiAlias") ? ES2.Load<int>("toggleAntiAlias") : 1;
        currentScreenResolutionCount = ES2.Exists("currentScreenResolutionCount") ? ES2.Load<int>("currentScreenResolutionCount") : 1;

        #endif


        //set values accordingly
        gfx_setFullScreen();
        gfx_setAnisoFiltering();
        gfx_setAntiAlias();
        gfx_setVsync();
        gfx_setShadows();
        gfx_setTextureQuality();
        gfx_setScreenResolution();
    }
    #endregion

}
