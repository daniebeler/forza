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
    [SerializeField] private GameObject finishObject, raceCanvas, raceFinishedCanvas;
    [SerializeField] private int bronzeSeconds, silverSeconds, goldSeconds;

    private List<SingleCheckpoint> _singleCheckpointsList;
    private int _nextCheckpointIndex;
    private DateTime _time;
    private TimeSpan _finishTime;
    private List<TimeSpan> _checkpointTimes;
    private List<TimeSpan> _checkpointTimesHighscore;
    private bool _finished;

    public bool highscoreExists;
    public bool checkpointTimesExists;
    public TimeSpan highscore;
    public UserData UserData;

    public void StartRace()
    {
        _time = DateTime.Now;
        Debug.Log("start the race");
        finishObject.SetActive(true);
        Debug.Log(finishObject.activeSelf);
        _time = DateTime.Now;
        raceCanvas.SetActive(true);
        UserData = SaveData.LoadUserData();
        _checkpointTimes = new List<TimeSpan>();
        ActivateCheckpoints();
        if (UserData != null && UserData.RaceHighscores != null && UserData.RaceHighscores.Count() != 0 &&
            UserData.RaceHighscores.Any(race => race.Raceid == raceId))
        {
            highscoreExists = true;
            highscore = TimeSpan.Parse(UserData.RaceHighscores.Find(race => race.Raceid == raceId).Highscore);
            if (UserData.RaceHighscores.Find(race => race.Raceid == raceId).CheckpointTimes.Count() != 0)
            {
                checkpointTimesExists = true;
                _checkpointTimesHighscore =
                    GetCheckpointTimesListToTimeSpan(UserData.RaceHighscores.Find(race => race.Raceid == raceId)
                        .CheckpointTimes);
            }
            else
            {
                checkpointTimesExists = false;
            }
        }
        else
        {
            checkpointTimesExists = false;
            highscoreExists = false;
        }
    }

    private void ActivateCheckpoints()
    {
        _singleCheckpointsList = new List<SingleCheckpoint>();
        Transform checkpoints = transform.Find("Checkpoints");
        checkpoints.gameObject.SetActive(true);
        foreach (Transform singleCheckpointTransform in checkpoints)
        {
            SingleCheckpoint singleCheckpoint = singleCheckpointTransform.GetComponent<SingleCheckpoint>();
            singleCheckpoint.SetRaceController(this);
            _singleCheckpointsList.Add(singleCheckpoint);
        }

        _nextCheckpointIndex = 0;
    }

    public void PlayerThroughCheckpoint(SingleCheckpoint singleCheckpoint)
    {
        if (_singleCheckpointsList.IndexOf(singleCheckpoint) == _nextCheckpointIndex)
        {
            _nextCheckpointIndex++;
            Debug.Log(singleCheckpoint);
            _checkpointTimes.Add(DateTime.Now - _time);
            if (checkpointTimesExists)
            {
                RaceCanvas raceCanvasScript = raceCanvas.GetComponent<RaceCanvas>();
                raceCanvasScript.SetCheckpointTime(_checkpointTimes[_nextCheckpointIndex - 1] - _checkpointTimesHighscore[_nextCheckpointIndex - 1]);
            }
        }
        else
        {
            Debug.Log("wrong CheckPoint");
        }
    }

    public void FinishRace()
    {
        if (_nextCheckpointIndex == _singleCheckpointsList.Count())
        {
            _finishTime = DateTime.Now - _time;
            _finished = true;
            raceCanvas.SetActive(false);
            raceFinishedCanvas.SetActive(true);
            raceFinishedCanvas.GetComponent<RaceFinishedCanvas>().setMedal(whichMedal());
            Debug.Log("finish Race");
            Debug.Log(_finishTime.ToString("mm':'ss':'ff"));
            SaveTimeIfHighscore();
        }
        else
        {
            Debug.Log("missing Checkpoints");
        }
    }

    private String whichMedal()
    {
        if (_finishTime.Seconds < goldSeconds)
        {
            return "Gold";
        } else if (_finishTime.Seconds < silverSeconds)
        {
            return "Silver";
        } else if (_finishTime.Seconds < bronzeSeconds)
        {
            return "Bronze";
        }
        else return "";
    }

    private void SaveTimeIfHighscore()
    {
        if (highscoreExists)
        {
            Debug.Log(highscore);
            Debug.Log(_finishTime);
            if (highscore > _finishTime)
            {
                UserData.RaceHighscores.FirstOrDefault(race => race.Raceid == raceId)!.Highscore =
                    _finishTime.ToString();
                UserData.RaceHighscores.FirstOrDefault(race => race.Raceid == raceId)!.CheckpointTimes =
                    GetCheckpointTimesListToString();
                SaveData.SaveDataVoid(UserData);
                highscore = _finishTime;
            }
        }
        else if (UserData.RaceHighscores == null)
        {
            UserData.RaceHighscores = new List<RaceHighscore>
                {new RaceHighscore(raceId, _finishTime.ToString(), GetCheckpointTimesListToString())};
            SaveData.SaveDataVoid(UserData);
            highscore = _finishTime;
        }
        else
        {
            UserData.RaceHighscores.Add(new RaceHighscore(raceId, _finishTime.ToString(),
                GetCheckpointTimesListToString()));
            SaveData.SaveDataVoid(UserData);
            highscore = _finishTime;
        }
    }

    private List<String> GetCheckpointTimesListToString()
    {
        List<String> checkpointTimesString = new List<string>();
        foreach (TimeSpan checkpointTime in _checkpointTimes)
        {
            checkpointTimesString.Add(checkpointTime.ToString());
        }

        return checkpointTimesString;
    }

    private List<TimeSpan> GetCheckpointTimesListToTimeSpan(List<String> stringList)
    {
        List<TimeSpan> checkpointTimesString = new List<TimeSpan>();
        foreach (String checkpointTimeString in stringList)
        {
            checkpointTimesString.Add(TimeSpan.Parse(checkpointTimeString));
        }

        return checkpointTimesString;
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