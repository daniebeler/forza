using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageController : MonoBehaviour
{

    [SerializeField] private Camera camera;

    public void rotateRight()
    {
        camera.transform.Rotate(0.0f, 45f, 0.0f, Space.World);
    }
    
    public void rotateLeft()
    {
        camera.transform.Rotate(0.0f, -45f, 0.0f, Space.World);
    }
}
