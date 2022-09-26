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
    
    private GameObject currentCar;
    void Start()
    {
        UserData userData = SaveData.LoadUserData();
        if (userData == null)
        {
            SaveData.SaveDataVoid(new UserData(0, new List<RaceHighscore>()));
        }
        
        setCurrentCar(userData.CurrentCarId);
    }
    

    public void setCurrentCar(int carIndex)
    {
        Vector3 carPosition = new Vector3(0, 1, 0);
        Quaternion carRotation = new Quaternion();
        if (currentCar != null)
        {
            carPosition = currentCar.transform.position;
            carRotation = currentCar.transform.rotation;
            Destroy(currentCar);
        }
        currentCar = Instantiate(CarPrefabs[carIndex], carPosition, carRotation);
        cameraFollow.setTarget(currentCar.transform);
        UserData userData = SaveData.LoadUserData();
        userData!.CurrentCarId = carIndex;
        SaveData.SaveDataVoid(userData);
    }
}
