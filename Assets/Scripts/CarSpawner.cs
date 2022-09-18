using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> CarPrefabs;
    [SerializeField]
    private Vector3 position;
    public CameraFollow cameraFollow;
    void Start()
    {
        GameObject car = Instantiate(CarPrefabs[0], position, Quaternion.identity);
        cameraFollow.setTarget(car.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
