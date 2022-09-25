using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class RaceCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text textTime, highscoreTime, CheckpointTime;

    [SerializeField] private RaceController race;

    // Update is called once per frame
    void Update()
    {
        textTime.text = race.GetTime().ToString("mm':'ss':'ff");
        if (race.highscoreExists)
        {
            highscoreTime.text = "Highscore: " + race.highscore.ToString("mm':'ss':'ff");
        }
    }

    public void SetCheckpointTime(TimeSpan timeDiffrence)
    {
        if (timeDiffrence.Milliseconds > 0.0)
        {
            CheckpointTime.color = Color.red;
            CheckpointTime.text = "Checkpoint: +" + timeDiffrence.ToString("mm':'ss':'ff");
        }
        else
        {
            CheckpointTime.color = Color.green;
            CheckpointTime.text = "Checkpoint: -" + timeDiffrence.ToString("mm':'ss':'ff");
        }
        
    }
}