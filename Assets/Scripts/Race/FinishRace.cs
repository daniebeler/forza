using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FinishRace : MonoBehaviour
{
    [SerializeField]
    private Race race;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        race.FinishRace();
    }
}
