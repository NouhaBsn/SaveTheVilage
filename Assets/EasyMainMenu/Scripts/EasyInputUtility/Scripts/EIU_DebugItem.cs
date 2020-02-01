using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
    public class EIU_DebugItem : MonoBehaviour
    {

        public Text axisName;
        public Text keyName;
        // Use this for initialization
        public void Init(string axisName, string keyName)
        {
            this.axisName.text = axisName;
            this.keyName.text = keyName;
        }

    }
}