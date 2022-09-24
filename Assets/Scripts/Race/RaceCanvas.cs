using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text textTime, highscoreTime;

    [SerializeField] private Race race;

    // Update is called once per frame
    void Update()
    {
        textTime.text = race.GetTime().ToString("mm':'ss':'ff");
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscoreTime.text = "Highscore: " + TimeSpan.FromMinutes(PlayerPrefs.GetFloat("highscore")).ToString("mm':'ss':'ff");
        }
    }
}