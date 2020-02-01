using UnityEngine;
using UnityEngine.UI;

namespace ELU
{
    public enum LoginState { Local, Server }

    public class LoginManager : MonoBehaviour
    {
        public string ObfuscatorString = "asdjhahsdQQKLJALKDnsada12123";

        [Header("Login State")]
        [Tooltip("Simply select from where you want to load the Login details.")]
        public LoginState loginState = LoginState.Local;

        [Header("Login Screen References")]
        public LoginUI loginUI;

        [Header("Signup Screen References")]
        public SignupUI signupUI;

        Animator animator;

        /// <summary>
        /// Initializing UI as soon as Game Starts
        /// </summary>
        void Awake()
        {
            InitializeLoginManager();
        }

        /// <summary>
        /// Iitialize the Login Manager
        /// </summary>
        void InitializeLoginManager() {

            //animator reference
            animator = GetComponent<Animator>();

            //init script based on login state
            switch (loginState) {

                case LoginState.Local:

                    //adding local login script
                    gameObject.AddComponent<LoginManager_Local>();
                    //init script
                    GetComponent<LoginManager_Local>().InitializeLoginManager();

                    //now adding local signup script
                    gameObject.AddComponent<SignupManager_Local>();
                    //init script
                    GetComponent<SignupManager_Local>().InitializeSignupManager(signupUI.incorrectText);

                    break;

                case LoginState.Server:

                    break;
            }

        }

        /// <summary>
        /// Getting the Login texts as soon as user starts typing!
        /// </summary>
        /// <param name="field"></param>
        public void RetrieveLoginText(InputField field)
        {
            if (field == loginUI.usernameInput)
                loginUI.username = field.text;
            else if(field == loginUI.passwordInput)
                loginUI.password = field.text;

        }

        public void RetrieveSignupText(InputField field)
        {

            if (field == signupUI.usernameInput)
                signupUI.username = field.text;
            else if(field == signupUI.passwordInput)
                signupUI.password = field.text;
            else if(field == signupUI.confirmPasswordInput)
                signupUI.confirmPassword = field.text;

        }

        #region OnClick Events

        /// <summary>
        /// Main Login Method which will be called whenever user clicks on Login Button
        /// </summary>
        public void Login() {

            switch (loginState)
            {
                case LoginState.Local:
                    //Calling the local login manager
                    GetComponent<LoginManager_Local>().CheckLoginDetails(loginUI.username, loginUI.password, ObfuscatorString);
                    break;  
                case LoginState.Server:
                    //Calling the server side login manager
                    //WIP
                    break;
            }

        }

        /// <summary>
        /// Main Signup Methid which will be saving the entered details via JSON!
        /// </summary>
        public void Signup() {

            switch (loginState)
            {
                case LoginState.Local:
                    //Calling the local login manager
                    GetComponent<SignupManager_Local>().SetLoginDetails(signupUI.username, signupUI.password, signupUI.confirmPassword, ObfuscatorString);
                    break;
                case LoginState.Server:
                    //Calling the server side login manager
                    //WIP
                    break;
            }

        }

        /// <summary>
        /// OnClick event to open the Signup / Register User process
        /// </summary>
        public void OpenSignup()
        {

            animator.Play("OpenSignup");

        }

        /// <summary>
        /// OnClick event to go back from the Signup / Register User process 
        /// to Login Screen
        /// </summary>
        public void backToLogin()
        {

            animator.Play("CloseSignup");

        }

        /// <summary>
        /// OnClick event to close the Game 
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        #endregion

    }


    #region Serialized Classes

    [System.Serializable]
    public class LoginUI
    {

        public InputField usernameInput;
        public InputField passwordInput;

        public string username;
        public string password;

    }

    [System.Serializable]
    public class SignupUI
    {

        public InputField usernameInput;
        public InputField passwordInput;
        public InputField confirmPasswordInput;

        public Text incorrectText;

        public string username;
        public string password;
        public string confirmPassword;

    }

    #endregion
}