using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] private int spawnScene;
    
    [SerializeField]
    private Color desertFog, mountainFog;

    [SerializeField] private Material desertSky, mountainSky;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnScene == 0)
            SetDesertLighting();
        else
            SetMountainLighting();
    }

    private void SetDesertLighting()
    {
        RenderSettings.fogColor = desertFog;
        RenderSettings.fogDensity = 0.003f;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fog = true;

        RenderSettings.skybox = desertSky;
    }
    
    private void SetMountainLighting()
    {
        RenderSettings.fogColor = mountainFog;
        RenderSettings.fogDensity = 0.0012f;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fog = true;
        
        RenderSettings.skybox = mountainSky;
    }

    public void SwitchFog()
    {
        if (RenderSettings.fogColor.Equals(desertFog))
        {
            SetMountainLighting();
        }
        else
        {
           SetDesertLighting();
        }
    }
}
