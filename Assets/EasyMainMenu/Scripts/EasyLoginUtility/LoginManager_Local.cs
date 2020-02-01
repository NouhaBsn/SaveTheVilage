using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ELU
{
    /// <summary>
    /// Local Login Manager
    /// - Here we will save the data locally in the system
    ///   using the Persisten Data method / JSON
    /// - JSON makes it hard for the hackers to hack the data and 
    ///   manipulate user settings!
    /// </summary>
    public class LoginManager_Local : MonoBehaviour
    {
        [HideInInspector]
        public Text incorrectText;

        /// <summary>
        /// Init the local Login Manager via
        /// Login Manager Script
        /// </summary>
        public void InitializeLoginManager() {
            //find text reference
            incorrectText = GameObject.Find("IncorrectText").GetComponent<Text>();
            incorrectText.enabled = false;

        }

        /// <summary>
        /// Handy method to display the incorrect text 
        /// whenever wrong username / password inserted
        /// </summary>
        void ShakeIncorrectText(string text = "Incorrect Details!") {
            incorrectText.enabled = true;
            incorrectText.text = text;
            incorrectText.GetComponent<SimpleShake>().StartShaking();
        }

        #region Login Check Method

        /// <summary>
        /// We just check whether these 2 fields exists together or not!
        /// </summary>
        /// <param name="u"></param>
        /// <param name="p"></param>
        public void CheckLoginDetails(string u, string p, string obfuscatorString) {

            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p)) {

                ShakeIncorrectText();
                Debug.Log("Incorrect Details");
                    
                return;
            }

            string path = Application.persistentDataPath + @"\" + obfuscatorString + u + p + obfuscatorString + ".json";
            Debug.Log(path);

            if (File.Exists(path))
            {
                incorrectText.color = Color.green;
                ShakeIncorrectText("Correct Details");
                PlayerPrefs.SetString("sceneToLoad", "MainMenu");
                FindObjectOfType<Fader>().FadeIntoLevel("LoadingScreen");
                Debug.Log("Correct Details");

            }
            else
            {
                ShakeIncorrectText();
                Debug.Log("Incorrect Details");

            }
           

        }

        #endregion

    }

}