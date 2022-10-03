using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    [field: SerializeField] public float MaxTime { get; private set; }
    [field: SerializeField] public float TimeLeft { get; private set; }

    public event Action OnTimeOver;

    bool running = true;

    bool TimeOverTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        RestartTimer();
    }

    public void RestartTimer()
    {
        TimeLeft = MaxTime;
        TimeOverTriggered = false;
    }

    public void Stop()
    {
        running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.stateMachine != GameManager.StateMachine.InGame || !running)
            return;


        if(TimeLeft > 0f)
        {
            TimeLeft -= Time.deltaTime;
        }
        else if(!TimeOverTriggered)
        {
            TimeOverTriggered = true;
            OnTimeOver?.Invoke();
        }
    }    
}
