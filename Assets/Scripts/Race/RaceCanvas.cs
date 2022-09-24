using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceCanvas : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textTime;

    [SerializeField] private Race race;


    // Update is called once per frame
    void Update()
    {
        
        textTime.text = race.GetTime().ToString("mm':'ss");
    }
}
