using UnityEngine;
using UnityEngine.Rendering;

public class QualityController : MonoBehaviour
{
    
    [SerializeField] private RenderPipelineAsset[] qualityLevels;
    
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
    
    public void setQualityLevel(int index)
    {
        QualitySettings.SetQualityLevel(index);
        QualitySettings.renderPipeline = qualityLevels[index];
    }
}