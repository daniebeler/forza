using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageController : MonoBehaviour
{

    [SerializeField] private Camera camera;

    private float lerpSpeed = 10f;
    private Vector3 currentAngle, targetAngle;
 
    public void Start()
    {
        currentAngle = camera.transform.eulerAngles;
        targetAngle = currentAngle;
    }

    public void rotateRight()
    {
        targetAngle[1] += 45f;
    }
    
    public void rotateLeft()
    {
        targetAngle[1] -= 45f;
    }

    private void Update()
    {
        currentAngle = new Vector3(
            currentAngle.x,
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, lerpSpeed * Time.deltaTime),
            currentAngle.z);
 
        camera.transform.eulerAngles = currentAngle;
    }
}
