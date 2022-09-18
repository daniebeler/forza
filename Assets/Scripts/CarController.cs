using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Vector3 centerOfMass;

    public void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        setSkidMarks();
    }

    public void setSkidMarks()
    {
        Skidmarks skidmarks = GameObject.FindGameObjectWithTag("scriptholder").GetComponent<Skidmarks>();
        foreach (var axle in axleInfos)
        {
            axle.leftWheel.GetComponent<WheelSkid>().setSkidMarkController(skidmarks);
            axle.rightWheel.GetComponent<WheelSkid>().setSkidMarkController(skidmarks);
        }
    }

    public void ApplyLocalPositionToVisuals(AxleInfo axleInfo)
    {
        Vector3 position;
        Quaternion rotation;
        axleInfo.leftWheelCollider.GetWorldPose(out position, out rotation);
        axleInfo.leftWheelMesh.transform.position = position;
        axleInfo.leftWheelMesh.transform.rotation = rotation *= Quaternion.Euler(0, 0, 90);
        axleInfo.rightWheelCollider.GetWorldPose(out position, out rotation);
        axleInfo.rightWheelMesh.transform.position = position;
        axleInfo.rightWheelMesh.transform.rotation = rotation *= Quaternion.Euler(0, 0, 90);
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.isSteering)
            {
                axleInfo.leftWheelCollider.steerAngle = steering;
                axleInfo.rightWheelCollider.steerAngle = steering;
            }
            if (axleInfo.isAttachedToMotor)
            {
                axleInfo.leftWheelCollider.motorTorque = motor;
                axleInfo.rightWheelCollider.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo);

            setWheelFrictionCurve(axleInfo.leftWheelCollider);
            setWheelFrictionCurve(axleInfo.rightWheelCollider);
        }
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