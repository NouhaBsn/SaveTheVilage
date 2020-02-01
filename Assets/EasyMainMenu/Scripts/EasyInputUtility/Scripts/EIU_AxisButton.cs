using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
    public class EIU_AxisButton : MonoBehaviour
    {
        #region Variables
        [Header("Child Text Objects")]
        public Text axisName_text;
        public Text keyName_text;

        [Space(10)]

        [Header("Axis Button Info")]
        public string axisName;
        public bool negativeKey;

        #endregion

        /// <summary>
        /// Initialize current UI axis prefab
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="buttonDescription"></param>
        /// <param name="key"></param>
        /// <param name="nKey"></param>
        public void init(string axisName, string buttonDescription, string key, bool nKey = false)
        {
            this.axisName = axisName;
            axisName_text.text = buttonDescription;
            keyName_text.text = key;
            negativeKey = nKey;

        }

        /// <summary>
        /// Change the key text of the current UI prefab
        /// </summary>
        /// <param name="key"></param>
        public void ChangeKeyText(string key) {
            keyName_text.text = key;
        }

        /// <summary>
        /// Opens the Rebind Dialog
        /// </summary>
        public void RebindAxis() {
            EIU_ControlsMenu.Instance().OpenRebindButtonDialog(axisName,negativeKey);
        }

        public void HoverClip()
        {
            EasyAudioUtility.instance.Play("Hover");
        }
    }
}