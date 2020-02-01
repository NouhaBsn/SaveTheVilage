using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EMM
{
    public class CharacterSelectMenuController : MonoBehaviour
    {
        public Text CharacterNameText;
        public Text SelectButtonText;

        [HideInInspector]
        public List<AllCharactersData> AllCharacters = new List<AllCharactersData>();

        int _totalCharacters;
        int _currentSelectedCharCount;
        string _currentSelectedCharName;

        // Use this for initialization
        void Start()
        {
            //get total characters available
            _totalCharacters = AllCharacters.Count;

            //init
            GetCharacter();
        }

        public void GetCharacter()
        {
            //we will try getting if any char have been selected
            //if not we will use the first one by default
            _currentSelectedCharName = PlayerPrefs.GetString("CurrentSelectedCharacter", AllCharacters[0].CharacterName);

            //get the count of the previous selected character
            for(int i  = 0; i< AllCharacters.Count; i++)
            {
                if(AllCharacters[i].CharacterName == _currentSelectedCharName)
                {
                    _currentSelectedCharCount = i;
                    break; //exit
                }
            }

            //now enable that
            SelectNextCharacter();
        }

        public void SelectNextCharacter()
        {
            //disable all
            foreach (AllCharactersData _d in AllCharacters)
                _d.CharacterMesh.SetActive(false);

            //always select the char first
            AllCharacters[_currentSelectedCharCount].CharacterMesh.SetActive(true);

            //get it's name
            _currentSelectedCharName = AllCharacters[_currentSelectedCharCount].CharacterName;

            //update text
            CharacterNameText.text = _currentSelectedCharName;

            //change Select button text based on the current selected character
            SelectButtonText.text = _currentSelectedCharName == PlayerPrefs.GetString("CurrentSelectedCharacter", AllCharacters[0].CharacterName) ? "Selected" : "Select";

            //now set _currentSelectedCharCount
            if (_currentSelectedCharCount < _totalCharacters-1)
                _currentSelectedCharCount++;
            else
                _currentSelectedCharCount = 0;

            PlayClickSound();
        }

        public void SelectCharacter()
        {
            //we will be accessing the character by name and not by index is coz, index can be  changed, but hopefully,
            //the name will remain same.
            //save character by name
            PlayerPrefs.SetString("CurrentSelectedCharacter", _currentSelectedCharName);

            SelectButtonText.text = "Selected";

            PlayClickSound();
        }

        void PlayClickSound()
        {
            if (EasyAudioUtility.instance)
                EasyAudioUtility.instance.Play(FindObjectOfType<MainMenuController>().ButtonClickSFX);
        }
    }

    [System.Serializable]
    public class AllCharactersData
    {
        public string CharacterName;
        public GameObject CharacterMesh;
    }
}