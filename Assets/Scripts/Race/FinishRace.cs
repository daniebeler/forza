using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRace : MonoBehaviour
{
    [SerializeField] private RaceController race;
    private void OnTriggerEnter(Collider other)
    {
        race.FinishRace();
    }
}