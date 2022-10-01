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
    // Start is called before the first frame update
    void Start()
    {
        RestartTimer();
    }

    void RestartTimer() => TimeLeft = MaxTime;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.stateMachine != GameManager.StateMachine.InGame)
            return;


        if(TimeLeft > 0f)
        {
            TimeLeft -= Time.deltaTime;
        }
        else
        {
            OnTimeOver?.Invoke();
            RestartTimer();
        }
    }

    
}
