using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    private Controls _controls;

    private InputAction _rightMouseInputAction;
    private InputAction _moveMouse;

    private bool isClicked = false;
    CameraFollow cameraFollow;

    // Start is called before the first frame update
    private void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        _rightMouseInputAction = _controls.Player.CameraClick;
        _rightMouseInputAction.Enable();
        _rightMouseInputAction.performed += RotateCamera;
        _moveMouse = _controls.Player.Look;
        _moveMouse.Enable();
    }

    private void OnDisable()
    {
        _rightMouseInputAction.Disable();
        _moveMouse.Disable();
    }

    void Start()
    {
        cameraFollow = gameObject.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rightMouseInputAction.WasReleasedThisFrame())
        {
            isClicked = false;
            cameraFollow.stopRotation();
        }


        if (isClicked)
        {
            cameraFollow.SetRotation(_moveMouse.ReadValue<Vector2>());
        }
    }

    void RotateCamera(InputAction.CallbackContext context)
    {
        isClicked = true;
    }
}