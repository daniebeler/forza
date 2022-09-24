using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSmoothness;
    public float rotSmoothness;

    public Vector3 moveOffset;
    public Vector3 rotOffset;
    public Vector3 rotMovingCamera;

    public Transform carTarget;
    private bool lookAround = false;
    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }

    void HandleRotation()
    {
        var direction = carTarget.position - transform.position;
        var rotation = new Quaternion();

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);
        if (lookAround)
        {
            rotation = Quaternion.LookRotation(direction + rotMovingCamera, Vector3.up);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }

    public void stopRotation()
    {
        lookAround = false;
    }

    public void SetRotation(Vector2 y)
    {
        if (!lookAround)
        {
            rotMovingCamera = new Vector3(rotOffset.x + y.x * 0.1f  * Time.deltaTime, rotOffset.y + y.y * 0.1f , rotOffset.z);
            lookAround = true;
        }
        else
        {
            rotMovingCamera = new Vector3(rotMovingCamera.x + y.x * 0.1f, rotMovingCamera.y + y.y * 0.1f, rotMovingCamera.z);
        }
    }

    public void setTarget(Transform target)
    {
        carTarget = target;
    }

}