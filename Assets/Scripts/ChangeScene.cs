using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Lighting lighting;
    public void OnTriggerEnter(Collider other)
    {
        lighting.SwitchFog();
    }
}
