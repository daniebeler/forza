using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject f3Menu, settingsMenu;

    private Controls _controls;
    private InputAction f3;
    private InputAction settings;

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
    }

    private void OnDisable()
    {
        f3.Disable();
        settings.Disable();
    }
    
    void Start()
    {
        f3Menu.SetActive(false);
        settingsMenu.SetActive(false);
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
}
