using System;
using System.Collections.Generic;
[Serializable]
public class UserData
{
    public int CurrentCarId;
    public List<RaceHighscore> RaceHighscores;

    public UserData(int currentCarId, List<RaceHighscore> raceHighscores)
    {
        CurrentCarId = currentCarId;
        RaceHighscores = raceHighscores;
    }
}