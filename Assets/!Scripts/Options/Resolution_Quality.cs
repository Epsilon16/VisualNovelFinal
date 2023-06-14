using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Resolution_Quality : MonoBehaviour
{
    private static Resolution_Quality instance;

    public TMPro.TMP_Dropdown ResolutionDropdown;
    [SerializeField] Resolution[] resolutions;

    public float typingspeeding;
    [SerializeField] private Slider SpeedSlider;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static Resolution_Quality GetInstance()
    {
        return instance;
    }

    void Start()
    {
        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int CurrentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
        }
        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        float savespeed = PlayerPrefs.GetFloat("Typing Speed");

        if (savespeed != 0)
        {
            typingspeeding = savespeed;
            SpeedSlider.value = savespeed;
        }
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }

    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }

    public void SetSpeedText()
    {
        typingspeeding = SpeedSlider.value;
    }

    public void QuitOption()
    {
        if (DialogueManager.GetInstance())
        {
            DialogueManager.GetInstance().isOptionOn = false;
        }
        MenuScript.GetInstance().DisableOption();
    }
}
