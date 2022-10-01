using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Timer timer;
    public RobotController CurrentActiveRobot;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        timer.OnTimeOver += DeactivateRobot;
        timer.OnTimeOver += SpawnRobot;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRobot()
    {
        Debug.Log("TRying to spawn robot..");
        foreach(RobotSpawner rs in GameManager.Instance.RobotSpawners)
        {
            if (rs.IsActive)
            {
                rs.SpawnRobot();
            }
        }
    }

    void DeactivateRobot()
    {
        CurrentActiveRobot.Deactivate();
    }
}
