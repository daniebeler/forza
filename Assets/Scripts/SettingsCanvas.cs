using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour
{
    [SerializeField] 
    private Toggle vsync, fullscreen;

    [SerializeField] 
    private QualityController qualityController;

    [SerializeField] private TMP_Dropdown qualityLevelDropdown, resolutionsDropdown;

    private Resolution[] resolutions;
    
    
    void Start()
    {
        vsync.isOn = PlayerPrefs.GetInt("vsync", 0) == 1;

        vsync.onValueChanged.AddListener(delegate { qualityController.setVsync(vsync.isOn); });

        fullscreen.isOn = Screen.fullScreen;
        
        fullscreen.onValueChanged.AddListener(delegate { Screen.fullScreen = fullscreen.isOn; });

        qualityLevelDropdown.value = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions;
        
        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    public void changeQualityLevel(int index)
    {
        PlayerPrefs.SetInt("qualitylevel", index);
        qualityController.setQualityLevel(index);
    }
}