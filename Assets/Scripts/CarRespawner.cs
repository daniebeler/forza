using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CarRespawner : MonoBehaviour
{
    private List<GameObject> _respawnPoints;

    private Controls _controls;
    private InputAction _respawn;

    private CarSpawner _carSpawner;

    [SerializeField] private CanvasController canvasController;
    private void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        _respawn = _controls.Player.Respawn;
        _respawn.Enable();
        _respawn.performed += Respawn;
    }

    private void OnDisable()
    {
        _respawn.Disable();
    }

    void Start()
    {
        _respawnPoints = GameObject.FindGameObjectsWithTag("respawnpoints").ToList();
        _carSpawner = GetComponent<CarSpawner>();
    }

    void Respawn(InputAction.CallbackContext context) 
    {
        respawnCar();
    }

    public void respawnCarAndClosePauseMenu()
    {
        respawnCar();
        canvasController.closePauseMenu();
    }

    private void respawnCar()
    {
        Transform closestRespawnPosition = FindClosestPoint();
        _carSpawner.getCurrentCar().transform.position = closestRespawnPosition.position;
        _carSpawner.getCurrentCar().GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    
    Transform FindClosestPoint()
    {
        Vector3 startPosition = _carSpawner.getCurrentCar().transform.position;
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (var respawnPoint in _respawnPoints)
        {
            Vector3 directionToTarget = respawnPoint.transform.position - startPosition;
 
            float dSqrToTarget = directionToTarget.sqrMagnitude;
 
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = respawnPoint;
            }
        }
        return bestTarget.transform;
    }
}
