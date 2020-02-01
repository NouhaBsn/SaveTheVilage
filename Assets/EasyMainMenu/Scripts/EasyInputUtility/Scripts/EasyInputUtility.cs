using System.Collections.Generic;
using UnityEngine;


namespace EIU
{
    public class EasyInputUtility : MonoBehaviour
    {
        public static EasyInputUtility instance;

        EIU_ControlsMenu menu;

        [Header("Define All Axes and Buttons Here")]
        [Space(5)]
        public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

        private void Awake()
        {
            menu = FindObjectOfType<EIU_ControlsMenu>();

            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            //send data to Controls Menu if we are at main menu
            if (menu)
            {
                //send all axes
                menu.Axes = Axes;
                //init
                menu.Init();

            }
            LoadAllAxes();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < Axes.Count; i++)
            {

                EIU_AxisBase a = Axes[i];
                a.negative = Input.GetKey(a.negativeKey);
                a.positive = Input.GetKey(a.positiveKey);

                a.targetAxis = (a.negative) ? -1 : (a.positive) ? 1 : 0;

                a.axis = Mathf.MoveTowards(a.axis, a.targetAxis, Time.deltaTime * a.sensitivity);
            }
        }

        /// <summary>
        /// Your Alternative for Input.GetAxis
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GetAxis(string name)
        {
            float val = 0;

            for (int i = 0; i < Axes.Count; i++)
            {
                if (string.Equals(Axes[i].axisName, name))
                {
                    val = Axes[i].axis;
                }
            }
            return val;
        }

        /// <summary>
        /// Your Alternative for Input.GetButton
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetButton(string name)
        {
            bool retVal = false;

            for (int i = 0; i < Axes.Count; i++)
            {
                if (string.Equals(Axes[i].axisName, name))
                {
                    retVal = Axes[i].positive;
                    retVal = Input.GetKey(Axes[i].positiveKey);

                }
            }

            return retVal;
        }

        /// <summary>
        /// Your Alternative for Input.GetButtonDown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetButtonDown(string name)
        {
            bool retVal = false;

            for (int i = 0; i < Axes.Count; i++)
            {
                if (string.Equals(Axes[i].axisName, name))
                {
                    retVal = Input.GetKeyDown(Axes[i].positiveKey);
                }
            }

            return retVal;
        }

        void LoadAllAxes()
        {

            for (int i = 0; i < Axes.Count; i++)
            {
                EIU_AxisBase a = Axes[i];

                //retrieving in the form of integer
                int p = PlayerPrefs.GetInt(a.axisName + "pKey");
                int n = PlayerPrefs.GetInt(a.axisName + "nKey");

                //BUT loading in the form of KeyCode
                a.positiveKey = (KeyCode)p;
                a.negativeKey = (KeyCode)n;

            }
        }
    }
}