using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOption : MonoBehaviour
{
    private static SaveOption instance;

    [SerializeField] private float GlobalVolumeSave;
    [SerializeField] private float MusicVolumeSave;
    [SerializeField] private float SFXVolumeSave;
    [SerializeField] private float typingSpeedSave;

    //[SerializeField] private Resolution[] resolutionSave;
    //[SerializeField] private QualitySettings qualitySave;
    //[SerializeField] private Screen fullScreenSave;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }

        instance = this;
    }

    public static SaveOption GetInstance()
    {
        return instance;
    }

    public void Savedata()
    {
        GlobalVolumeSave = VolumeSetting.GetInstance().volume;
        PlayerPrefs.SetFloat("Volume Général", GlobalVolumeSave);
        Debug.Log(GlobalVolumeSave);
        MusicVolumeSave = VolumeSetting.GetInstance().volumeMusic;
        PlayerPrefs.SetFloat("Volume Musique", MusicVolumeSave);
        Debug.Log(MusicVolumeSave);
        SFXVolumeSave = VolumeSetting.GetInstance().volumeSFX;
        PlayerPrefs.SetFloat("Volume SFX", SFXVolumeSave);
        Debug.Log(SFXVolumeSave);
        typingSpeedSave = Resolution_Quality.GetInstance().typingspeeding;
        PlayerPrefs.SetFloat("Typing Speed", typingSpeedSave);
        Debug.Log(typingSpeedSave);
    }
}
