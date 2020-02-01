using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EIU
{
    public class EIU_ControlsMenu : MonoBehaviour
    {
        [HideInInspector]
        public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();
        Dictionary<string, int> defaultAxes = new Dictionary<string, int>();

        [Header("UI References")]
        [Space(5)]
        public GameObject axisPrefab;
        public Transform controlsList;
        public EIU_RebindButton rebBtn;

        [HideInInspector]
        public bool rebinding;

        bool negativeKey;
        EIU_AxisBase targetAxis;
        bool initOnce;


        public void Init() {

            if (!initOnce)
            {
                SaveAllAxes();
                initOnce = true;

            }

            //disabling rebind btn if it's enable
            rebBtn.gameObject.SetActive(false);
            LoadAllAxes();
            CreateAxisButtons();

            ResetAxes();

            //Overriding the inputs in EasyInputUtility Class
            EasyInputUtility.instance.Axes = Axes;
        }

        /// <summary>
        /// Checks if it is left to be set as default
        /// </summary>
        void ResetAxes()
        {
            foreach (EIU_AxisBase a in Axes)
            {
                if (a.positiveKey == KeyCode.None)
                {
                    int pKeyVal = 0;

                    defaultAxes.TryGetValue(a.axisName + "pKey", out pKeyVal);
                    a.positiveKey = (KeyCode)pKeyVal;
                    a.pUIButton.ChangeKeyText(a.positiveKey.ToString());

                    SaveAxis(a);

                }
                if (a.negativeKey == KeyCode.None)
                {
                    int nKeyVal = 0;

                    defaultAxes.TryGetValue(a.axisName + "nKey", out nKeyVal);
                    a.negativeKey = (KeyCode)nKeyVal;

                    if (a.nUIButton)
                        a.nUIButton.ChangeKeyText(a.negativeKey.ToString());

                    SaveAxis(a);

                }

            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < Axes.Count; i++) {

                EIU_AxisBase a = Axes[i];
                a.negative = Input.GetKey(a.negativeKey);
                a.positive = Input.GetKey(a.positiveKey);

                a.targetAxis = (a.negative) ? -1 : (a.positive) ? 1 : 0;

                a.axis = Mathf.MoveTowards(a.axis, a.targetAxis, Time.deltaTime * a.sensitivity);
            }
        }

        /// <summary>
        /// Main Input Mapping Method
        /// </summary>
        private void OnGUI()
        {
            if (rebinding)
            {

                controlsList.gameObject.SetActive(false);

                Event e = Event.current;

                if (e != null)
                {
                    if (e.isKey && !e.isMouse)
                    {

                        if (e.keyCode != KeyCode.None)
                        {
                            ChangeInputKey(targetAxis.axisName, e.keyCode, negativeKey);
                            CloseRebindingDialog();

                        }
                    }

                    //making sure if player presses cancel btn, it won't take input then also
                    if (e.isMouse && !EventSystem.current.IsPointerOverGameObject())
                    {

                        KeyCode targetKey = KeyCode.None;

                        switch (e.button)
                        {
                            case 0:
                                targetKey = KeyCode.Mouse0;
                                break;
                            case 1:
                                targetKey = KeyCode.Mouse1;
                                break;
                        }

                        if (targetKey != KeyCode.None)
                        {
                            ChangeInputKey(targetAxis.axisName, targetKey, negativeKey);
                            CloseRebindingDialog();
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Resets All axes and saves them
        /// </summary>
        public void ResetAllAxes() {
            foreach(EIU_AxisBase a in Axes)
            {
                int pKeyVal = 0;
                defaultAxes.TryGetValue(a.axisName + "pKey", out pKeyVal);
                a.positiveKey = (KeyCode)pKeyVal;

                int nKeyVal = 0;
                defaultAxes.TryGetValue(a.axisName + "nKey", out nKeyVal);
                a.negativeKey = (KeyCode)nKeyVal;

                a.pUIButton.ChangeKeyText(a.positiveKey.ToString());

                if (a.nUIButton)
                a.nUIButton.ChangeKeyText(a.negativeKey.ToString());

                SaveAxis(a);
            }
        }

        #region UI Management

        public void ChangeInputKey(string name, KeyCode newKey, bool negative = false)
        {
            EIU_AxisBase a = ReturnAxis(name);

            if (a == null)
            {
                Debug.Log("Doesn't exist!");
                return;
            }

            if (negative)
            {
                a.negativeKey = newKey;
                a.nUIButton.ChangeKeyText(a.negativeKey.ToString());
            }
            else
            {
                a.positiveKey = newKey;
                a.pUIButton.ChangeKeyText(a.positiveKey.ToString());
            }
            SaveAxis(a);

        }

        void CreateAxisButtons()
        {
            foreach (EIU_AxisBase a in Axes)
            {
                //instantiate UI prefab
                GameObject positive = Instantiate(axisPrefab);

                //set it's parent
                positive.transform.SetParent(controlsList);

                //forcing it's scale to 1 
                positive.transform.localScale = Vector3.one;
                //forcing it's position to 0
                positive.transform.localPosition = Vector3.zero;

                //get it's axis button
                EIU_AxisButton p_ab = positive.GetComponent<EIU_AxisButton>();
                //set init values
                p_ab.init(a.axisName, a.pKeyDescription, a.positiveKey.ToString(), false);
                //set axes positive btn to this
                a.pUIButton = p_ab;

                //Debug.Log("+ve created");

                //same for negative btn IF it exist
                if (a.nKeyDescription != "")
                {
                    //instantiate UI prefab
                    GameObject negative = Instantiate(axisPrefab);

                    //set it's parent
                    negative.transform.SetParent(controlsList);

                    //forcing it's scale to 1
                    negative.transform.localScale = Vector3.one;
                    //forcing it's position to 0
                    negative.transform.localPosition = Vector3.zero;

                    //get it's axis button
                    EIU_AxisButton n_ab = negative.GetComponent<EIU_AxisButton>();
                    //set init values
                    n_ab.init(a.axisName, a.nKeyDescription, a.negativeKey.ToString(), true);
                    //set axes positive btn to this
                    a.nUIButton = n_ab;

                    //Debug.Log("-ve created");

                }
            }

        }

        EIU_AxisBase ReturnAxis(string name)
        {

            EIU_AxisBase val = null;

            for (int i = 0; i < Axes.Count; i++)
            {

                if (string.Equals(name, Axes[i].axisName))
                {
                    val = Axes[i];
                }
            }
            return val;
        }

        public void OpenRebindButtonDialog(string axisName, bool negative)
        {
            targetAxis = ReturnAxis(axisName);
            rebinding = true;

            if (!negative)
                rebBtn.init(targetAxis.pKeyDescription);
            else
                rebBtn.init(targetAxis.nKeyDescription);

            rebBtn.gameObject.SetActive(true);
            negativeKey = negative;

        }

        void CloseRebindingDialog()
        {
            rebinding = false;
            rebBtn.gameObject.SetActive(false);
            controlsList.gameObject.SetActive(true);

        }

        public void CancelRebinding() {

            CloseRebindingDialog();
        }
        #endregion

        #region Save/Load
        void SaveAllAxes() {
            for (int i = 0; i < Axes.Count; i++)
            {

#if UNITY_EDITOR
                SaveAxis(Axes[i]);
#endif

                SaveAxesDefault(Axes[i]);

            }
        }

        void SaveAxis(EIU_AxisBase axis)
        {
            PlayerPrefs.SetInt(axis.axisName + "pKey", (int)axis.positiveKey);
            PlayerPrefs.SetInt(axis.axisName + "nKey", (int)axis.negativeKey);

        }

        void SaveAxesDefault(EIU_AxisBase axis)
        {
            //adding in dictionary the +ve key
            defaultAxes.Add(axis.axisName + "pKey", (int)axis.positiveKey);

            //adding in dictionary the -ve key
            defaultAxes.Add(axis.axisName + "nKey", (int)axis.negativeKey);


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

        #endregion

        #region Create Instance

        public static EIU_ControlsMenu instance;

        public static EIU_ControlsMenu Instance() {
            return instance;
        }

        private void Awake()
        {
            instance = this;

        }


        #endregion

        public float GetAxis(string name)
        {
            float v = 0;

            for(int i = 0; i < Axes.Count; i++)
            {
                if(string.Equals(Axes[i].axisName, name))
                {
                    v = Axes[i].axis;
                }
            }
            return v;
        }

        public bool GetButton(string name)
        {
            bool retVal = false;

            for (int i = 0; i< Axes.Count; i++)
            {
                if(string.Equals(Axes[i].axisName, name))
                {
                    retVal = Axes[i].positive;
                }
            }

            return retVal;
        }
        
    }

 }