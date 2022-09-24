using UnityEngine;

public class QualityController : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("vsync", 0) == 1)
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 300;
        }
    }

    public void setVsync(bool state)
    {
        if (state)
        {
            PlayerPrefs.SetInt("vsync", 1);
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }
        else
        {
            PlayerPrefs.SetInt("vsync", 0);
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 300;
        }
    }
}