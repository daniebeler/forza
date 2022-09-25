using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class RaceHighscore
{
    public int Raceid;
    public String Highscore;
    public List<String> CheckpointTimes;

    public RaceHighscore(int raceId,String highscore, List<String> checkpointTimes)
    {
        Raceid = raceId;
        this.Highscore = highscore;
        CheckpointTimes = checkpointTimes;
    }
}