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
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        
        instance = this;

    }

    private void Start()
    {
        float savegeneral = PlayerPrefs.GetFloat("Volume Général");
        float savemusic = PlayerPrefs.GetFloat("Volume Musique");
        float savesfx = PlayerPrefs.GetFloat("Volume SFX");
        if ( savegeneral != 0)
        {
            GeneralSlider.value = savegeneral;
            volume = savegeneral;
            MyMixer.SetFloat("General", Mathf.Log10(volume) * 20);
        }
        if (savemusic != 0)
        {
            MusicSlider.value = savemusic;
            volumeMusic = savemusic;
            MyMixer.SetFloat("Music", Mathf.Log10(volumeMusic) * 50);
        }
        if (savesfx != 0)
        {
            SFXSlider.value = savesfx;
            volumeSFX = savesfx;
            MyMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 50);
        }
        
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

    public static VolumeSetting GetInstance()
    {
        return instance;
    }
}

