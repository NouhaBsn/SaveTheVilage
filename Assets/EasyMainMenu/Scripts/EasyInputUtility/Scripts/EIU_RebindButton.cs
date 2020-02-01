using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
    public class EIU_RebindButton : MonoBehaviour
    {
        public Text axis_text;
        // Use this for initialization
        public void init(string n)
        {
            axis_text.text = n;
        }

    }
}