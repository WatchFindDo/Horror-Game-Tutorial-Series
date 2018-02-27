using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.PostProcessing;

public class SettingsConfig : MonoBehaviour {

    [Header("Content")]
    [SerializeField] AudioMixer             audioMixer  = null;
    [SerializeField] PostProcessingProfile  ppProfiler  = null;

    [Header("Settings Parameters")]
    [SerializeField] Slider     volumeSlider            = null;
    [SerializeField] Dropdown   qualityDropdown         = null;
    [SerializeField] Dropdown   effectsDropdown         = null;
    [SerializeField] Dropdown   winModeDropdown         = null;
    [SerializeField] Dropdown   resolutionDropdown      = null;
    [SerializeField] Dropdown   shadowsDropdown         = null;
    [SerializeField] Slider     brightnessSlider        = null;

    [Header("Current Settings")]
    [HideInInspector]
    public float                Volume                  = 1;
    [HideInInspector]
    public int                  Quality                 = 0;
    [HideInInspector]
    public bool                 Effects                 = true;
    [HideInInspector]
    public bool                 Fullscreen              = true;
    [HideInInspector]
    public string               Resolution              = null;
    [HideInInspector]
    public bool                 Shadows                 = true;
    [HideInInspector]
    public int                  ShadowsQuality          = 0;
    [HideInInspector]
    public float                Brightness              = 1;

    [Header("Private Variables")]
    Resolution[]                resolutions;
    float                       defaultShadowDistance;

    void Start ()
    {
        //Set the default shadows distance value.
        defaultShadowDistance = QualitySettings.shadowDistance;

        //Gather and store all available screen resolutions.
        resolutions = Screen.resolutions;

        //Remove all options.
        resolutionDropdown.ClearOptions();

        //List of all resolutions.
        List<string> res = new List<string>();

        int resIndex = 0;

        //Loop through all available resolutions and add them to the options list. 
        for (int i = 0; i < resolutions.Length; i++)
        {
            //Create new resolution option name.
            string newRes = resolutions[i].width + " x " + resolutions[i].height;

            //Add new resolution to the list.
            res.Add(newRes);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resIndex = i;
            }
        }

        //Add new available list of resolutions to the dropdown list.
        resolutionDropdown.AddOptions(res);

        //Set the default parameters on startup.
        SetDefaultValues(resIndex);
    }

	public void ChangeVolume (float _vol)
    {
        //Set new volume value.
        Volume = _vol;

        //Change volume.
        if (audioMixer)
        {
            audioMixer.SetFloat("volume_mixer_value", Volume);
        }
        else
        {
            Debug.LogWarning("Warning! 'AudioMixer' is not assigned!");
        }

        //Set the volume value.
        if (volumeSlider)
        {
            volumeSlider.value = Volume;
        }
        else
        {
            Debug.LogWarning("'Volume' Slider is not assigned!");
        }
    }

    public void ChangeQuality (int _quality)
    {
        //Set new Quality Level.
        QualitySettings.SetQualityLevel(_quality);

        //Change quality value.
        Quality = _quality;

        //Set the current value and refresh the dropdown.
        if (qualityDropdown)
        {
            qualityDropdown.value = Quality;
            qualityDropdown.RefreshShownValue();
        }
        else
        {
            Debug.LogWarning("'Quality' Dropdown is not assigned!");
        }
    }

    public void ChangeEffects (int _effects)
    {
        //Check and Change the current effect state.
        Effects = (_effects == 0) ? true : false;

        //Set the current value and refresh the dropdown.
        if (effectsDropdown)
        {
            effectsDropdown.value = _effects;
            effectsDropdown.RefreshShownValue();
        }
        else
        {
            Debug.LogWarning("'Effects' Dropdown is not assigned!");
        }

        //Set the current effect state.
        SetEffects(Effects);
    }
    private void SetEffects (bool _state)
    {
        if (ppProfiler == null)
        {
            Debug.LogWarning("Warning! 'PostProcessingProfile' is not assigned!");
            return;
        }
            

        ppProfiler.bloom.enabled = _state;
        ppProfiler.motionBlur.enabled = _state;
        ppProfiler.chromaticAberration.enabled = _state;
        ppProfiler.ambientOcclusion.enabled = _state;
        ppProfiler.vignette.enabled = _state;
    }

    public void ChangeWinMode (int _mode)
    {
        Fullscreen = (_mode == 0) ? true : false;

        //Set the current value and refresh the dropdown.
        if (winModeDropdown)
        {
            winModeDropdown.value = _mode;
            winModeDropdown.RefreshShownValue();
        }
        else
        {
            Debug.LogWarning("'WinMode' Dropdown is not assigned!");
        }

        Screen.fullScreen = Fullscreen;
    }

    public void ChangeResolution (int _index)
    {
        //Get new resolution.
        Resolution newResolution = resolutions[_index];

        //Set the current value and refresh the dropdown.
        if (resolutionDropdown)
        {
            resolutionDropdown.value = _index;
            resolutionDropdown.RefreshShownValue();
        }
        else
        {
            Debug.LogWarning("'Resolution' Dropdown is not assigned!");
        }

        //Set new resolution.
        Screen.SetResolution(newResolution.width, newResolution.height, Fullscreen);
        Resolution = newResolution.width + " x " + newResolution.height;
    }

    public void ChangeShadows (int _state)
    {
        //Check and set the shadows state.
        Shadows = (_state == 0) ? false : true;

        //Set the current value and refresh the dropdown.
        if (shadowsDropdown)
        {
            shadowsDropdown.value = _state;
            shadowsDropdown.RefreshShownValue();
        }
        else
        {
            Debug.LogWarning("'Shadows' Dropdown is not assigned!");
        }

        #region Change Shadow State

        //Check if shadows are disabled.
        if (!Shadows) //If so..
        {
            //Disable all shadows.
            QualitySettings.shadowDistance = 0;

            //Return from this function.
            return;
        }
        else //If not..
        {
            //Set the shadows distance value, back to default.
            QualitySettings.shadowDistance = defaultShadowDistance;
        }
            
        //Switch to the correct shadows quality level.
        switch (_state)
        {
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 3:
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            case 4:
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                break;
            default:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
        }

        #endregion

        //Set the current shadows quality level value.
        ShadowsQuality = _state;
    }

    public void ChangeBrightness (float _value)
    {
        if (ppProfiler == null)
        {
            Debug.LogWarning("Warning! 'PostProcessingProfile' is not assigned!");
            return;
        }

        Brightness = _value;

        //Set the current value.
        if (brightnessSlider)
        {
            brightnessSlider.value = _value;
        }
        else
        {
            Debug.LogWarning("'Brightness' Slider is not assigned!");
        }

        ColorGradingModel.Settings g = ppProfiler.colorGrading.settings;
        g.basic.postExposure = Brightness;
        ppProfiler.colorGrading.settings = g;
    }

    private void SetDefaultValues (int currentResolution)
    {
        ChangeVolume(0);
        ChangeQuality(2);
        ChangeEffects(0);
        ChangeWinMode(0);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
        ChangeShadows(4);
        ChangeBrightness(1.65f);
    }
}