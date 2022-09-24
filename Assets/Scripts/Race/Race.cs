using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Race : MonoBehaviour
{
    [SerializeField]
    private GameObject finish, raceCanvas;

    private DateTime _time;

    private TimeSpan _finishTime;

    private bool _finished;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRace()
    {
        Debug.Log("start the race");
        finish.SetActive(true);
        _time = DateTime.Now;
        raceCanvas.SetActive(true);
    }

    public void FinishRace()
    {
        _finishTime = _time - DateTime.Now;
        _finished = true;
        Debug.Log("finish Race");
        Debug.Log(_finishTime.ToString("mm':'ss"));
    }

    public TimeSpan GetTime()
    {
        if (!_finished)
        {
            return (_time - DateTime.Now);
        }
        else
        {
            return _finishTime;
        }
    }
}
