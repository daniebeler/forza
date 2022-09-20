using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Vector3 centerOfMass;

    private Controls _controls;
    private InputAction _move;

    public void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        _move = _controls.Player.Move;
        _move.Enable();
        _controls.Player.Handbreak.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
        _controls.Player.Handbreak.Disable();

    }
    public void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        SetSkidMarks();
    }

    private void SetSkidMarks()
    {
        Skidmarks skidmarks = GameObject.FindGameObjectWithTag("scriptholder").GetComponent<Skidmarks>();
        foreach (var axle in axleInfos)
        {
            axle.leftWheel.GetComponent<WheelSkid>().setSkidMarkController(skidmarks);
            axle.rightWheel.GetComponent<WheelSkid>().setSkidMarkController(skidmarks);
        }
    }

    private void ApplyLocalPositionToVisuals(AxleInfo axleInfo)
    {
        Vector3 position;
        Quaternion rotation;
        axleInfo.leftWheelCollider.GetWorldPose(out position, out rotation);
        axleInfo.leftWheelMesh.transform.position = position;
        axleInfo.leftWheelMesh.transform.rotation = rotation *= Quaternion.Euler(0, 0, 0);
        axleInfo.rightWheelCollider.GetWorldPose(out position, out rotation);
        axleInfo.rightWheelMesh.transform.position = position;
        axleInfo.rightWheelMesh.transform.rotation = rotation *= Quaternion.Euler(0, 0, 0);
    }

    public void Move()
    {
        Vector2 movement = _move.ReadValue<Vector2>();
        float motor = maxMotorTorque * movement.y;
        float steering = maxSteeringAngle * movement.x;
        bool handBrake = _controls.Player.Handbreak.IsPressed();

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (handBrake)
            {
                axleInfo.leftWheelCollider.brakeTorque = 6000f;
                axleInfo.rightWheelCollider.brakeTorque = 6000f;
                axleInfo.leftWheelCollider.motorTorque = 0;
                axleInfo.rightWheelCollider.motorTorque = 0;
            }
            else
            {
                axleInfo.leftWheelCollider.brakeTorque = 0;
                axleInfo.rightWheelCollider.brakeTorque = 0;
                if (axleInfo.isAttachedToMotor)
                {
                    axleInfo.leftWheelCollider.motorTorque = motor;
                    axleInfo.rightWheelCollider.motorTorque = motor;
                }
            }
            if (axleInfo.isSteering)
            {
                axleInfo.leftWheelCollider.steerAngle = steering;
                axleInfo.rightWheelCollider.steerAngle = steering;
            }

            ApplyLocalPositionToVisuals(axleInfo);

            setWheelFrictionCurve(axleInfo.leftWheelCollider);
            setWheelFrictionCurve(axleInfo.rightWheelCollider);
        }
    }

    public void FixedUpdate()
    {
        Move();
    }

    private void setWheelFrictionCurve(WheelCollider wheel)
    {
        WheelHit hit;
        if (wheel.GetGroundHit(out hit))
        {
            WheelFrictionCurve fFriction = wheel.forwardFriction;
            fFriction.stiffness = hit.collider.material.staticFriction;
           wheel.forwardFriction = fFriction;
            WheelFrictionCurve sFriction = wheel.sidewaysFriction;
            sFriction.stiffness = hit.collider.material.staticFriction;
           wheel.sidewaysFriction = sFriction;
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public GameObject leftWheelMesh;
    public GameObject rightWheelMesh;
    public WheelCollider leftWheelCollider;
    public WheelCollider rightWheelCollider;
    public GameObject leftWheel;
    public GameObject rightWheel;
    public bool isAttachedToMotor;
    public bool isSteering;
}