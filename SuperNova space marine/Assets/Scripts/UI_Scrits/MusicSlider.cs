using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public AudioMixer mixer;
    private void Start()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        mixer.GetFloat("volumeMusic", out float volume);
        volume = Mathf.Pow(10, volume / 20);
        slider.value = volume;
    }

    
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("volumeMusic", Mathf.Log10(sliderValue) * 20);
       
    }
}
