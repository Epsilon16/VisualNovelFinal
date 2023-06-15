using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    private static VolumeSetting instance;

    [SerializeField] private AudioMixer MyMixer;
    [SerializeField] private Slider GeneralSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    public float volume;
    public float volumeMusic;
    public float volumeSFX;


    public float defaultVolume;
    public float defaultVolumeMusic;
    public float defaultVolumeSFX;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        
        instance = this;
    }

    public static VolumeSetting GetInstance()
    {
        return instance;
    }

    //Set les valeurs par rapport aux playerprefs
    private void Start()
    {
        float savegeneral = PlayerPrefs.GetFloat("Volume Général");
        float savemusic = PlayerPrefs.GetFloat("Volume Musique");
        float savesfx = PlayerPrefs.GetFloat("Volume SFX");

        if (savegeneral != 0)
        {
            GeneralSlider.value = savegeneral;
            volume = savegeneral;
        }
        else
        {
            GeneralSlider.value = defaultVolume;
            volume = defaultVolume;
        }

        if (savemusic != 0)
        {
            MusicSlider.value = savemusic;
            volumeMusic = savemusic;
        }
        else
        {
            MusicSlider.value = defaultVolumeMusic;
            volumeMusic = defaultVolumeMusic;
        }

        if (savesfx != 0)
        {
            SFXSlider.value = savesfx;
            volumeSFX = savesfx;
        }
        else
        {
            SFXSlider.value = defaultVolumeSFX;
            volumeSFX = defaultVolumeSFX;
        }

        MyMixer.SetFloat("General", Mathf.Log10(volume) * 20);
        MyMixer.SetFloat("Music", Mathf.Log10(volumeMusic) * 50);
        MyMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 50);
    }

    private void Update()
    {
        volume = GeneralSlider.value;
        volumeMusic = MusicSlider.value;
        volumeSFX = SFXSlider.value;
    }

    public void SetGeneralVolume()
    {
        //float volume = GeneralSlider.value;
        MyMixer.SetFloat("General", Mathf.Log10(volume) * 20);
    }
    

    public void SetMusicVolume()
    {
        //float volumeMusic = MusicSlider.value;
        MyMixer.SetFloat("Music", Mathf.Log10(volumeMusic) * 50);
    }

    public void SetSFXVolume()
    {
        //float volumeSFX = SFXSlider.value;
        MyMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 50);
    }
}

