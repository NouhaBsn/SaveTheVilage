using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ELU
{
    public class SignupManager_Local : MonoBehaviour
    {
        public string username;
        public string password;
        public string confirmPassword;

        Text incorrectText;

        /// <summary>
        /// Init the local Login Manager via
        /// Login Manager Script
        /// </summary>
        public void InitializeSignupManager(Text incText)
        {
            incorrectText = incText;

            if (incorrectText)
                incorrectText.enabled = false;

        }

        /// <summary>
        /// Handy method to display the incorrect text 
        /// whenever wrong password / confirm password inserted
        /// </summary>
        void ShakeIncorrectText(Color c, string text = "Incorrect Details!")
        {
            incorrectText = GameObject.Find("IncorrectText_Signup").GetComponent<Text>();

            if (incorrectText)
            {
                incorrectText.enabled = true;
                incorrectText.text = text;
                incorrectText.color = c;
                incorrectText.GetComponent<SimpleShake>().StartShaking();
            }
        }

        /// <summary>
        /// Saving Login details locally in the drive using JSON
        /// </summary>
        /// <param name="u"></param>
        /// <param name="p"></param>
        /// <param name="cp"></param>
        public void SetLoginDetails(string u, string p, string cp, string obfuscatorString) {

            //setting data
            username = obfuscatorString + u + obfuscatorString;
            password = obfuscatorString + p + obfuscatorString;
            confirmPassword = obfuscatorString + cp + obfuscatorString;

            //if 1 or more fields are empty 
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                ShakeIncorrectText(Color.red);
                return;
            }

            //if password and confirm password don't match
            if(password != confirmPassword)
            {
                ShakeIncorrectText(Color.red);
                return;
            }

            //if everything is fine, generate JSON File
            string path = Application.persistentDataPath + @"\"+ obfuscatorString + u + p + obfuscatorString + ".json";

            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(JsonUtility.ToJson(this));
            writer.Close();

            Debug.Log("Saved user details at : " + path);

            //now see if file exists
            if (File.Exists(path)) {
                ShakeIncorrectText(Color.green, "Success!");
                return;
            }

        }


    }
}