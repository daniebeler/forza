using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject f3Menu, settingsMenu, mapMenu, pauseMenu, garageMenu, touchElements;

    private Controls _controls;
    private InputAction f3;
    private InputAction settings;
    private InputAction map;
    private InputAction pause;

    [SerializeField] private General general;

    public void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        f3 = _controls.UI.F3;
        f3.Enable();
        f3.performed += toggleFpsCanvas;

        settings = _controls.UI.Settings;
        settings.Enable();
        settings.performed += toggleSettingsMenu;
        
        map = _controls.UI.Map;
        map.Enable();
        map.performed += toggleMapMenu;
        
        pause = _controls.UI.Pause;
        pause.Enable();
        pause.performed += pressedEsc;
    }

    private void OnDisable()
    {
        f3.Disable();
        settings.Disable();
        map.Disable();
        pause.Disable();
    }
    
    void Start()
    {
        f3Menu.SetActive(false);
        settingsMenu.SetActive(false);
        mapMenu.SetActive(false);
        pauseMenu.SetActive(false);
        garageMenu.SetActive(false);
        touchElements.SetActive(true);
    }

    private void pressedEsc(InputAction.CallbackContext context) {
        if (mapMenu.activeSelf) {
            closeMapMenu();
        } else if (settingsMenu.activeSelf) {
            closeSettingsMenu();
        } else if (garageMenu.activeSelf) {
            general.switchFromGarageToGame();
        } else if (pauseMenu.activeSelf) {
            closePauseMenu();
            openTouchElemens();
        } else {
            openPauseMenu();
            closeTouchElements();
        }
    }

    void toggleFpsCanvas(InputAction.CallbackContext context) {
        if(f3Menu.activeSelf) {
            f3Menu.SetActive(false);
        } else {
            f3Menu.SetActive(true);
        }
    }

    void toggleSettingsMenu(InputAction.CallbackContext context) {
        if(settingsMenu.activeSelf) {
            settingsMenu.SetActive(false);
        } else {
            settingsMenu.SetActive(true);
        }
    }

    public void openSettingsMenu() {
        settingsMenu.SetActive(true);
    }

    public void closeSettingsMenu() {
        settingsMenu.SetActive(false);
    }
    
    void toggleMapMenu(InputAction.CallbackContext context) {
        if(mapMenu.activeSelf) {
            mapMenu.SetActive(false);
        } else { 
            mapMenu.SetActive(true);
        }
    }

    public void openMapMenu() {
        mapMenu.SetActive(true);
    }

    public void closeMapMenu() {
        mapMenu.SetActive(false);
    }

    public void openPauseMenu() {
        pauseMenu.SetActive(true);
    }

    public void closePauseMenu() {
        pauseMenu.SetActive(false);
    }

    public void openGarageMenu()
    {
        garageMenu.SetActive(true);
    }

    public void closeGarageMenu()
    {
        garageMenu.SetActive(false);
    }

    public void openTouchElemens()
    {
        touchElements.SetActive(true);
    }

    public void closeTouchElements()
    {
        touchElements.SetActive(false);
    }
}
