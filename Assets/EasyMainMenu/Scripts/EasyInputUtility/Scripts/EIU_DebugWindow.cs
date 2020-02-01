using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EIU {

    public class EIU_DebugWindow : MonoBehaviour {

        public bool debug = true;

        public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

        [Header("UI References")]
        public Transform DebugWindow;
        public GameObject DebugItemPrefab;

        private void Start()
        {
            if (debug && EasyInputUtility.instance)
            {
                populateDebugWindow();
            }

            if (EasyInputUtility.instance == null)
                gameObject.SetActive(false);
        }

        /// <summary>
        /// Populates the Debug Window
        /// if Debug = true
        /// </summary>
        void populateDebugWindow()
        {
            Axes = EasyInputUtility.instance.Axes;
            foreach (EIU_AxisBase a in Axes)
            {
                GameObject positiveAxis = Instantiate(DebugItemPrefab);
                positiveAxis.name = "positiveAxis";

                positiveAxis.transform.SetParent(DebugWindow);
                positiveAxis.transform.localScale = Vector3.one;

                positiveAxis.GetComponent<EIU_DebugItem>().Init(a.pKeyDescription, a.positiveKey.ToString());

                if(a.nKeyDescription != "")
                {
                    GameObject negativeAxis = Instantiate(DebugItemPrefab);
                    negativeAxis.name = "negativeAxis";
                    negativeAxis.transform.SetParent(DebugWindow);
                    negativeAxis.transform.localScale = Vector3.one;

                    negativeAxis.GetComponent<EIU_DebugItem>().Init(a.nKeyDescription, a.negativeKey.ToString());
                }

                
            }

        }


    }
}