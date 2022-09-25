using UnityEngine;
using UnityEngine.Rendering;

public class QualityController : MonoBehaviour
{
    
    [SerializeField] private RenderPipelineAsset[] qualityLevels;
    
    private Resolution[] resolutions;
    
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
        
        setQualityLevel(PlayerPrefs.GetInt("qualitylevel", 5));

        setTargetFramerate(PlayerPrefs.GetInt("framerate", 60));
        
        resolutions = Screen.resolutions;
    }

    public void setVsync(bool state)
    {
        if (state)
        {
            PlayerPrefs.SetInt("vsync", 1);
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            PlayerPrefs.SetInt("vsync", 0);
            QualitySettings.vSyncCount = 0;
        }
    }

    public void setTargetFramerate(int framerate) {
        Application.targetFrameRate = framerate;
    }
    
    public void setQualityLevel(int index)
    {
        QualitySettings.SetQualityLevel(index);
        QualitySettings.renderPipeline = qualityLevels[index];
    }

    public void setResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }
}