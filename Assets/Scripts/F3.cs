using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class F3 : MonoBehaviour
{
    [SerializeField] private TMP_Text textFPS, textQualityLevel, textVsync, textResolution;


    private void Start()
    {
        //InvokeRepeating("updateData", 0f, 1f);
        
    }

    private void Update()
    {
        textFPS.text = ((int) (1f / Time.unscaledDeltaTime)).ToString() + " FPS";
        updateData();
    }

    private void updateData()
    {
        string qualityLevel = QualitySettings.GetQualityLevel() switch
        {
            0 => "Very Low",
            1 => "Low",
            2 => "Medium",
            3 => "High",
            4 => "Very High",
            5 => "Ultra",
            _ => "Null"
        };

        textQualityLevel.text = "Quality Level: " + qualityLevel;

        textVsync.text = QualitySettings.vSyncCount == 0 ? "V-Sync: OFF" : "V-Sync: ON";

        textResolution.text = "Resolution: " + Screen.height + "x" + Screen.width;
    }
}