using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EIU
{
    [System.Serializable]
    public class EIU_AxisBase 
    {
        public string axisName;

        public KeyCode positiveKey;
        public KeyCode negativeKey;

        [HideInInspector]
        public bool positive;
        [HideInInspector] 
        public bool negative;

        [HideInInspector]
        public float axis;
        [HideInInspector]
        public float targetAxis;
        public float sensitivity = 3;

        public string pKeyDescription;
        public string nKeyDescription;

        [HideInInspector]
        public EIU_AxisButton pUIButton;
        [HideInInspector]
        public EIU_AxisButton nUIButton;

    }
}