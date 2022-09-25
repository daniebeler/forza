using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    private float rotationSpeed = 20f;
    void Update()
    {
        transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f, Space.Self);
    }
}
