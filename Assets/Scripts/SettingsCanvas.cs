using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour
{
    [SerializeField]
    private Toggle vsync;

    [SerializeField]
    private QualityController qualityController;

    void Start()
    {
        if (PlayerPrefs.GetInt("vsync", 0) == 1)
        {
            vsync.isOn = true;
        }
        else
        {
            vsync.isOn = false;
        }

        vsync.onValueChanged.AddListener(delegate
        {
            qualityController.setVsync(vsync.isOn);
        });
    }
}
