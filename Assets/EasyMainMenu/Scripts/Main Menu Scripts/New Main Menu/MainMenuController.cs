using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EMM
{
    public class MainMenuController : MonoBehaviour
    {
        public Animator MenuButtonsAnimator;
        public string newGameSceneName;
        public int quickSaveSlotID;
        public bool UseLevelSelectMenu;

        [Header("Anims")]
        public string MainMenuToStartMenu;
        public string StartMenuToMainMenu;

        [Space]
        public string StartMenuToLevelSelectMenu;
        public string LevelSelectMenuToStartMenu;

        [Space]
        public string StartMenuToLoadSlotMenu;
        public string LoadSlotMenuToStartMenu;

        [Space]
        public string MainMenuToCharMenu;
        public string CharMenuToMainMenu;

        [Space]
        public string MainMenuToOptionsMenu;
        public string OptionsMenuToMainMenu;

        [Space]
        public string OptionsMenuToGameplayMenu;
        public string GameplayMenuToOptionsMenu;

        [Space]
        public string OptionsMenuToGraphicsMenu;
        public string GraphicsMenuToOptionsMenu;

        [Header("SFX")]
        public string ButtonClickSFX;
        public string MainMenuSFX;

        UnityEngine.EventSystems.EventSystem eventSystem;

        // Use this for initialization
        void Start()
        {
            if (EasyAudioUtility.instance == null)
                Instantiate(Resources.Load("Prefabs/EasyAudioUtility"));

            PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);

            //play main menu sfx
            EasyAudioUtility.instance.Play(MainMenuSFX);

            eventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
        }

        //-------------------------
        //From x menu to y menu
        //-------------------------

        public void FromMainMenuToStartMenu()
        {
            MenuButtonsAnimator.Play(MainMenuToStartMenu);
            PlayClickSound();
        }

        public void FromStartMenuToMainMenu()
        {
            MenuButtonsAnimator.Play(StartMenuToMainMenu);
            PlayClickSound();
        }

        public void FromStartMenuToNewGame()
        {
            //either open level select
            if(UseLevelSelectMenu)
                MenuButtonsAnimator.Play(StartMenuToLevelSelectMenu);
            else
            {
                //or load just as it is the new game scene
                PlayerPrefs.SetString("sceneToLoad", newGameSceneName);

                //load level via fader
                Fader fader = FindObjectOfType<Fader>();
                fader.FadeIntoLevel("LoadingScreen");
            }

            PlayClickSound();
        }

        public void FromLevelSelectMenuToStartMenu()
        {
            MenuButtonsAnimator.Play(LevelSelectMenuToStartMenu);
            PlayClickSound();
        }

        public void FromStartMenuToLoadSlotMenu()
        {
            MenuButtonsAnimator.Play(StartMenuToLoadSlotMenu);
            PlayClickSound();
        }

        public void FromLoadSlotMenuToStartMenu()
        {
            MenuButtonsAnimator.Play(LoadSlotMenuToStartMenu);
            PlayClickSound();
        }

        public void FromMainMenuToCharMenu()
        {
            MenuButtonsAnimator.Play(MainMenuToCharMenu);
            PlayClickSound();
        }

        public void FromCharMenuToMainMenu()
        {
            MenuButtonsAnimator.Play(CharMenuToMainMenu);

            //Re Init while going back to main menu
            if (FindObjectOfType<CharacterSelectMenuController>())
                FindObjectOfType<CharacterSelectMenuController>().GetCharacter();

            PlayClickSound();
        }

        public void FromMainMenuToOptionsMenu()
        {
            MenuButtonsAnimator.Play(MainMenuToOptionsMenu);
            PlayClickSound();
        }

        public void FromOptionsMenuToMainMenu()
        {
            MenuButtonsAnimator.Play(OptionsMenuToMainMenu);
            PlayClickSound();
        }

        public void FromOptionsMenuToGameplayMenu()
        {
            MenuButtonsAnimator.Play(OptionsMenuToGameplayMenu);
            PlayClickSound();
        }

        public void FromGameplayMenuToOptionsMenu()
        {
            MenuButtonsAnimator.Play(GameplayMenuToOptionsMenu);
            PlayClickSound();
        }

        public void FromOptionsMenuToGraphicsMenu()
        {
            MenuButtonsAnimator.Play(OptionsMenuToGraphicsMenu);
            PlayClickSound();
        }

        public void FromGraphicsMenuToOptionsMenu()
        {
            MenuButtonsAnimator.Play(GraphicsMenuToOptionsMenu);
            PlayClickSound();
        }

        public void ChangeSelectedGameobject(GameObject Obj)
        {
            StartCoroutine(SelectButtonInUI(Obj));
        }

        IEnumerator SelectButtonInUI(GameObject Btn)
        {
            yield return new WaitForSeconds(0.25f);

            eventSystem.SetSelectedGameObject(Btn);

            if (Btn.GetComponent<UnityEngine.UI.Button>())
                Btn.GetComponent<UnityEngine.UI.Button>().Select();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        void PlayClickSound()
        {
            if (EasyAudioUtility.instance)
                EasyAudioUtility.instance.Play(ButtonClickSFX);
        }
    }
}