using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EMM
{
    public class GameplayMenuController : MonoBehaviour
    {
        public BrightnessEffect _BrightnessEffect;

        public Slider BrightnessSlider;
        public Slider ContrastSlider;
        public Slider SoundSlider;

        // Use this for initialization
        void Start()
        {
            GetBrightness();
            GetContrast();
            GetSounds();
        }

        void GetBrightness()
        {
            BrightnessSlider.value = PlayerPrefs.GetFloat("_BrightnessValue", 1f);
            SetBrightness();
        }

        void GetContrast()
        {
            ContrastSlider.value = PlayerPrefs.GetFloat("_ContrastValue", 1f);
            SetContrast();
        }

        void GetSounds()
        {
            SoundSlider.value = PlayerPrefs.GetFloat("_SoundsValue", 1f);
            SetSounds();
        }

        public void SetBrightness()
        {
            _BrightnessEffect._Brightness = BrightnessSlider.value;
            PlayerPrefs.SetFloat("_BrightnessValue", BrightnessSlider.value);
        }

        public void SetContrast()
        {
            _BrightnessEffect._Contrast = ContrastSlider.value;
            PlayerPrefs.SetFloat("_ContrastValue", ContrastSlider.value);

        }

        public void SetSounds()
        {
            if (!EasyAudioUtility.instance)
                return;

            foreach (EasyAudioUtility_Helper h in EasyAudioUtility.instance.helper)
            {
                h.source.volume = SoundSlider.value;
                h.volume = SoundSlider.value;
            }

            PlayerPrefs.SetFloat("_SoundsValue", SoundSlider.value);

        }

    }
}