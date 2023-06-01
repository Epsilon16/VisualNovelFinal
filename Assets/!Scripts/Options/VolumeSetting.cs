using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer MyMixer;
    [SerializeField] private Slider GeneralSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    // Start is called before the first frame update

    public void SetGeneralVolume()
    {
        float volume = GeneralSlider.value;
        MyMixer.SetFloat("General", Mathf.Log10(volume) * 20);
    }
    

    public void SetMusicVolume()
    {
        float volumeMusic = MusicSlider.value;
        MyMixer.SetFloat("Music", Mathf.Log10(volumeMusic) * 50);
    }

    public void SetSFXVolume()
    {
        float volumeSFX = SFXSlider.value;
        MyMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 20);
    }
}

