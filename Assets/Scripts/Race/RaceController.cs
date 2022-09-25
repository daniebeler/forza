using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RaceController : MonoBehaviour
{
    [SerializeField] private int raceId;
    [SerializeField] private GameObject finish, raceCanvas;

    private DateTime _time;

    private TimeSpan _finishTime;

    private bool _finished;

    public bool highscoreExists;
    public TimeSpan highscore;
    public UserData UserData;

    public void StartRace()
    {
        Debug.Log("start the race");
        finish.SetActive(true);
        _time = DateTime.Now;
        raceCanvas.SetActive(true);
        UserData = SaveData.LoadUserData();
        if (UserData != null && UserData.RaceHighscores != null && UserData.RaceHighscores.Count() != 0 &&
            UserData.RaceHighscores.Any(race => race.Raceid == raceId))
        {
            highscoreExists = true;
            highscore = TimeSpan.Parse(UserData.RaceHighscores.Find(race => race.Raceid == raceId).Highscore);
        }
        else
        {
            highscoreExists = false;
        }
    }

    public void FinishRace()
    {
        _finishTime = DateTime.Now - _time;
        _finished = true;
        Debug.Log("finish Race");
        Debug.Log(_finishTime.ToString("mm':'ss':'ff"));
        SaveTimeIfHighscore();
    }

    private void SaveTimeIfHighscore()
    {
        if (highscoreExists)
        {
            Debug.Log(highscore);
            Debug.Log(_finishTime);
            if (highscore > _finishTime)
            {
                UserData.RaceHighscores.FirstOrDefault(race => race.Raceid == raceId)!.Highscore = _finishTime.ToString();
                UserData.RaceHighscores.FirstOrDefault(race => race.Raceid == raceId)!.CheckpointTimes =
                    new List<string> {""};
                SaveData.SaveDataVoid(UserData);
                highscore = _finishTime;
            }
        }
        else if (UserData.RaceHighscores == null)
        {
            UserData.RaceHighscores = new List<RaceHighscore>
                {new RaceHighscore(raceId, _finishTime.ToString(), new List<string> {""})};
            SaveData.SaveDataVoid(UserData);
            highscore = _finishTime;

        }
        else
        {
            UserData.RaceHighscores.Add(new RaceHighscore(raceId, _finishTime.ToString(), new List<string> {""}));
            SaveData.SaveDataVoid(UserData);
            highscore = _finishTime;
        }
    }

    public TimeSpan GetTime()
    {
        if (!_finished)
        {
            return (DateTime.Now - _time);
        }
        else
        {
            return _finishTime;
        }
    }
}