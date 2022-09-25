using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class General : MonoBehaviour
{

    [SerializeField] private Camera carCamera, garageCamera;

    [SerializeField] private CanvasController _canvasController;

    private void Start()
    {
        carCamera.enabled = true;
        garageCamera.enabled = false;
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
    public void switchFromGameToGarage()
    {
        carCamera.enabled = false;
        garageCamera.enabled = true;
        _canvasController.closePauseMenu();
        _canvasController.openGarageMenu();
    }
    
    public void switchFromGarageToGame()
    {
        carCamera.enabled = true;
        garageCamera.enabled = false;
        _canvasController.closeGarageMenu();
    }
}
