using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public class CarRespawner : MonoBehaviour
{
    private List<GameObject> _respawnPoints;

    // Start is called before the first frame update
    private Controls _controls;
    private InputAction _respawn;
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
        Debug.Log("hallo");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Respawn(InputAction.CallbackContext context)
    {
        Transform closestRespawnPosition = FindClosestPoint();
        gameObject.transform.position = closestRespawnPosition.position;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    Transform FindClosestPoint()
    {
        Vector3 startPosition = gameObject.transform.position;
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