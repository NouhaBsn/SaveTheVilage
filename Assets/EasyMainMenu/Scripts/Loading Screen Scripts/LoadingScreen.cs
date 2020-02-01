using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    #region Variables

    [Tooltip("Scene retrieved to load. If empty, it will load main menu.")]
    public string sceneToLoad;

    [Header("Loading Bar")]
    
    [Tooltip("Loading Bar")]
    public Slider loadingBar;
    [Tooltip("Show loading bar or circular loading indicator.")]
    public bool showLoadingBar;
    //[Tooltip("Loading Bar fill delay.")]
    //public float fillDelay = 0.5f;
    //[Tooltip("Loading Bar fill speed.")]
    //public float fillSpeed = 0.5f;

    [Header("Circular Indicator")]
    [Tooltip("Circular loading delay.")]
    public GameObject circularIndicator;
    [Tooltip("Scene Load Delay.")]
    public float circularLoadDelay = 6f;
    [Tooltip("Circular Indicator rotation speed.")]
    public float circularIndicatorAnimSpeed = 1f;

    [Header("Loading Screen Image Transition")]
    [Tooltip("Loading Screen image")]
    public Image defaultLoadingScreenImage;
    [Tooltip("If it's true, images will show one after another, else any random image will be shown from below array.")]
    public bool showImageTransition = true;
    [Tooltip("If it's true, RANDOM images will show one after another, else any random image will be shown from below array.")]
    public bool showRandomImageTransition = true;
    [Tooltip("Add 1280x720 res images if it's landscape menu")]
    public Sprite[] LoadingScreenImages;
    [Tooltip("How long an image will be displayed")]
    [Range(3f,10f)]
    public float transitionDuration;
    [Tooltip("Transition Fader")]
    public Animator transitionFader;

    [Header("Continue Text Option")]
    [Tooltip("If true, scene will load after clicking / touching the screen!")]
    public bool showContinueText;
    [Tooltip("Continue Text")]
    public GameObject PressAnyKeyToContinue;
    public GameObject LoadingText;

    [Header("Scene Specific Loading Screen")]
    [Tooltip("If you want to have specific loading screens for specific scene!")]
    public SceneSpecificLoading[] sceneSpecificLoading;

    //Editor Script Variables
    [HideInInspector]
    public int selectedTab1;
    [HideInInspector]
    public int selectedTab2;
    [HideInInspector]
    public int selectedTab3;
    [HideInInspector]
    public string currentTab;

    AsyncOperation asyncOperation;
    #endregion

    // Use this for initialization
    void Start()
    {
        //init loading bar one time and make scene ready to load
        init();

        if (showLoadingBar)
            //using coroutine for performance
            StartCoroutine(LoadSceneProgress());
        //InvokeRepeating("fillLoadingBar", fillDelay, fillSpeed);
       
    }

    void init() {

        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        RetrieveSceneToLoad();

        //load on the basis of loading bar fill amount
        if (showLoadingBar)
        {
            //init loading bar
            loadingBar.minValue = 0f;
            loadingBar.maxValue = 100f;
            loadingBar.value = 0f;

            //enable right object
            loadingBar.gameObject.SetActive(true);
            circularIndicator.SetActive(false);

        }
        //else load after circular laod delay
        else
        {
          //  enable right object
            loadingBar.gameObject.SetActive(false);
            circularIndicator.SetActive(true);

            //set anim speed
            circularIndicator.GetComponent<Animator>().speed = circularIndicatorAnimSpeed;
            Invoke("loadScene", circularLoadDelay);
        }

        //lerp the audio source which is playing to 0
        EasyAudioUtility_SceneManager sm = FindObjectOfType<EasyAudioUtility_SceneManager>();
        if (sm)
        {
            sm.FadeVolume(0);
        }

        //if we have specific loading screen for the scene
        foreach (SceneSpecificLoading l in sceneSpecificLoading)
        {
            if(l.sceneName == sceneToLoad)
            {
                //reset the loading screen image
                defaultLoadingScreenImage.color = Color.white;
                defaultLoadingScreenImage.sprite = l.loadingImage;

                //exit the init code
                return;
            }
        }

        //If we didn't find any specific image, we just continue normally

        //enable loading screen transitions
        if (showImageTransition)
        {
            defaultLoadingScreenImage.color = Color.white;
            //now invoking transitions
            InvokeRepeating("StartImageTransition", 0f, transitionDuration);

        }
        //else set a random image from array to screen image
        else
        {
            //if any image is added
            if(LoadingScreenImages.Length > 0)
            {
                defaultLoadingScreenImage.color = Color.white;
                defaultLoadingScreenImage.sprite = LoadingScreenImages[Random.Range(0, LoadingScreenImages.Length)];
            }
        }

    }

    IEnumerator LoadSceneProgress()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone && asyncOperation.progress < 0.9f)
        {
            fillLoadingBar((int)(asyncOperation.progress*100));
            yield return null;
        }

        fillLoadingBar(100);

        if (!showContinueText)
            loadScene();
        else
            enableContinueText();

    }

    private void Update()
    {
        if(tapInput && Input.anyKeyDown)
                loadScene();

    }

    void RetrieveSceneToLoad() {

        //if using Easy Save,
        //Integration will be done automatically

        #if !EMM_ES2
            
        //retrieve what scene to be loaded
        sceneToLoad = PlayerPrefs.GetString("sceneToLoad");
        //if it's null
        if (sceneToLoad == "")
        {
            sceneToLoad = "MainMenu";
        }

        #else

        if (ES2.Exists("sceneToLoad"))
        {
            string sceneNameToBeLoaded = ES2.Load<string>("sceneToLoad");
            Debug.Log(sceneNameToBeLoaded);

            if (!string.IsNullOrEmpty(sceneNameToBeLoaded))
                sceneToLoad = sceneNameToBeLoaded;
            else
                sceneToLoad = "MainMenu";
        }
        else 
        {
            //the scene to load data haven't been generated even once...
            sceneToLoad = "MainMenu";
        }

        #endif

        //delete key asap
        #if !EMM_ES2
        PlayerPrefs.DeleteKey("sceneToLoad");
        #else
        ES2.Delete("sceneToLoad");
        #endif

    }

    void fillLoadingBar(int val) {

        loadingBar.value = val;

        ////increase it's value by 1,2 or 3
        //loadingBar.value += Random.Range(0,10);

        //if (loadingBar.value == 100) {
        //    Debug.Log("load scene");

        //    if (!showContinueText)
        //        loadScene();
        //    else
        //        enableContinueText();

        //    CancelInvoke("fillLoadingBar");
        //}
    }


    #region Image Transition
    
    int i = 0;
    int lastSprite = 0;
    int cacheSprite = 0;
    /// <summary>
    /// Invoking TransitionFader with the interval of -0.5f
    /// </summary>
    void StartImageTransition()
    {
        if (i < LoadingScreenImages.Length)
        {
            defaultLoadingScreenImage.sprite = LoadingScreenImages[i];

            if(showRandomImageTransition){
            	cacheSprite = lastSprite;
            	lastSprite = Random.Range(0,LoadingScreenImages.Length);

            	if(cacheSprite != lastSprite){
           	 		defaultLoadingScreenImage.sprite = LoadingScreenImages[lastSprite];
            	}else
            	{
            		lastSprite = Random.Range(0,LoadingScreenImages.Length);
            		defaultLoadingScreenImage.sprite = LoadingScreenImages[lastSprite];
            	}
            } 
            
            
            CancelInvoke("TransitionFader");
            Invoke("TransitionFader", transitionDuration - 0.5f);
            i++;
        }else
        {
            i = 0;
            defaultLoadingScreenImage.sprite = LoadingScreenImages[i];

            if(showRandomImageTransition){
            	cacheSprite = lastSprite;
            	lastSprite = Random.Range(0,LoadingScreenImages.Length);

            	if(cacheSprite != lastSprite){
           	 		defaultLoadingScreenImage.sprite = LoadingScreenImages[lastSprite];
            	}else
            	{
            		lastSprite = Random.Range(0,LoadingScreenImages.Length);
            		defaultLoadingScreenImage.sprite = LoadingScreenImages[lastSprite];
            	}
            }


            CancelInvoke("TransitionFader");
            Invoke("TransitionFader", transitionDuration - 0.5f);
            i++;
        }
    }

    void TransitionFader()
    {
       // Debug.Log("TransitionFader");
        transitionFader.Play("Transition");
    }

    #endregion

    bool tapInput = false;
    void enableContinueText()
    {
        tapInput = true;
        PressAnyKeyToContinue.SetActive(true);
        LoadingText.SetActive(false);
    }

    void loadScene() {
        //begin fader
        Animator Fader = GameObject.Find("Fader").GetComponent<Animator>();
        Fader.GetComponent<Animator>().Play("Fader In");
        Invoke("load", 1f);
    }

    void load() {

        //call the event to swap music NOW
        EasyAudioUtility_SceneManager sm = FindObjectOfType<EasyAudioUtility_SceneManager>();
        if (sm)
            sm.onSceneChange(sceneToLoad);


        //finally load scene
        //SceneManager.LoadScene(sceneToLoad);

#if PIXELCRUSHERS_SAVESYSTEM

        if(sceneToLoad == "MainMenu" || PlayerPrefs.GetInt("slotLoaded_") == -1)
            asyncOperation.allowSceneActivation = true;
        else
            PixelCrushers.SaveSystem.LoadFromSlot(PlayerPrefs.GetInt("slotLoaded_"));
        return;
#endif
        asyncOperation.allowSceneActivation = true;

    }

    
}

[System.Serializable]
public class SceneSpecificLoading
{
    public string sceneName;
    public Sprite loadingImage;
}
