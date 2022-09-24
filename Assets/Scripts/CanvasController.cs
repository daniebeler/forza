using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject f3Menu;

    private Controls _controls;
    private InputAction f3;

    public void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        f3 = _controls.UI.F3;
        f3.Enable();
        f3.performed += toggleFpsCanvas;
    }

    private void OnDisable()
    {
        f3.Disable();
    }
    
    void Start()
    {
        f3Menu.SetActive(false);
    }

    void toggleFpsCanvas(InputAction.CallbackContext context) {
        if(f3Menu.activeSelf) {
            f3Menu.SetActive(false);
        } else {
            f3Menu.SetActive(true);
        }
    }
}
