using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public Vector3 centerOfMass;

    public void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
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
            if (axleInfo.steering)
            {
                axleInfo.leftWheelCollider.steerAngle = steering;
                axleInfo.rightWheelCollider.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheelCollider.motorTorque = motor;
                axleInfo.rightWheelCollider.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo);

            // WheelHit hit = new WheelHit();
            // WheelCollider wheel = axleInfo.leftWheelCollider;
            // if (wheel.GetGroundHit(out hit))
            // {
            //     if (hit.collider.tag == "ice")
            //     {
            //         wheel.sidewaysFriction = 0;
            //     }
            //     else
            //     {

            //     }
            // }

            WheelHit hit;
            WheelCollider wheel = axleInfo.leftWheelCollider;
            if (wheel.GetGroundHit(out hit))
            {
                WheelFrictionCurve fFriction = wheel.forwardFriction;
                fFriction.stiffness = hit.collider.material.staticFriction;
                wheel.forwardFriction = fFriction;
                WheelFrictionCurve sFriction = wheel.sidewaysFriction;
                sFriction.stiffness = hit.collider.material.staticFriction;
                wheel.sidewaysFriction = sFriction;
            }

            wheel = axleInfo.rightWheelCollider;
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
}

[System.Serializable]
public class AxleInfo
{
    public GameObject leftWheelMesh;
    public GameObject rightWheelMesh;
    public WheelCollider leftWheelCollider;
    public WheelCollider rightWheelCollider;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}