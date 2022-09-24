using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour
{
    [SerializeField] 
    private Toggle vsync, fullscreen;

    [SerializeField] 
    private QualityController qualityController;

    [SerializeField] private TMP_Dropdown qualityLevelDropdown;
    
    
    void Start()
    {
        vsync.isOn = PlayerPrefs.GetInt("vsync", 0) == 1;

        vsync.onValueChanged.AddListener(delegate { qualityController.setVsync(vsync.isOn); });

        fullscreen.isOn = Screen.fullScreen;
        
        fullscreen.onValueChanged.AddListener(delegate { Screen.fullScreen = fullscreen.isOn; });

        qualityLevelDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void changeQualityLevel(int index)
    {
        PlayerPrefs.SetInt("qualitylevel", index);
        qualityController.setQualityLevel(index);
    }
}