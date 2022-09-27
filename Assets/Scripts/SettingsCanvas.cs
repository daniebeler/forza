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
    private TMP_Text targetFramerateText;


    [SerializeField]
    private Slider targetFramerateSlider;

    [SerializeField] 
    private QualityController qualityController;

    [SerializeField] private TMP_Dropdown qualityLevelDropdown, resolutionsDropdown;

    private Resolution[] resolutions;

    [SerializeField] private General general;
    
    
    void Start()
    {
        qualityLevelDropdown.value = QualitySettings.GetQualityLevel();
        
        if (general.playingOnMobile())
        {
            targetFramerateSlider.GameObject().SetActive(false);
            resolutionsDropdown.GameObject().SetActive(false);
            vsync.GameObject().SetActive(false);
            fullscreen.GameObject().SetActive(false);
        }
        else
        {
            vsync.isOn = PlayerPrefs.GetInt("vsync", 0) == 1;

            vsync.onValueChanged.AddListener(delegate { qualityController.setVsync(vsync.isOn); });

            fullscreen.isOn = Screen.fullScreen;
        
            fullscreen.onValueChanged.AddListener(delegate { Screen.fullScreen = fullscreen.isOn; });

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

            targetFramerateSlider.value = PlayerPrefs.GetInt("framerate", 60);
        }
    }

    public void changeQualityLevel(int index)
    {
        PlayerPrefs.SetInt("qualitylevel", index);
        qualityController.setQualityLevel(index);
    }

    public void changeTargetFramerate(float target) {
        PlayerPrefs.SetInt("framerate", (int)target);

        if (target == targetFramerateSlider.maxValue) {
            targetFramerateText.text = "Target FPS: unlimited";
            qualityController.setTargetFramerate(1000);
        } else {
            targetFramerateText.text = "Target FPS: " + (int)target;
            qualityController.setTargetFramerate((int)target);
        }
    }
}