using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> CarPrefabs;
    [SerializeField] 
    private int carNumber;
    [SerializeField]
    private Vector3 position;
    public CameraFollow cameraFollow;
    void Start()
    {
        GameObject car = Instantiate(CarPrefabs[carNumber], position, Quaternion.identity);
        cameraFollow.setTarget(car.transform);
    }
    void SpawnCar(Vector3 spawnPosition)
    {
        GameObject car = Instantiate(CarPrefabs[carNumber], spawnPosition, Quaternion.identity);
        cameraFollow.setTarget(car.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
