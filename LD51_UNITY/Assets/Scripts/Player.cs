using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    Timer timer;
    public RobotController CurrentActiveRobot;
    public CinemachineVirtualCamera followRobotCamera;

    public List<Collectable> CollectedItems;

    // Start is called before the first frame update
    void Awake()
    {
        timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        timer.OnTimeOver += DeactivateRobot;
        timer.OnTimeOver += SpawnRobot;
    }

    private void OnDisable()
    {
        timer.OnTimeOver -= DeactivateRobot;
        timer.OnTimeOver -= SpawnRobot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCollectedItem(Collectable collectable)
    {
        //animation
        //sfx
        CollectedItems.Add(collectable);
    }

    void SpawnRobot()
    {
        Debug.Log("TRying to spawn robot..");
        foreach(RobotSpawner rs in GameManager.Instance.RobotSpawners)
        {
            if (rs.IsActive)
            {
                rs.SpawnRobot(CollectedItems);
                return;
            }
        }

        Debug.LogError("No Robot Spawners are active..");
    }

    void DeactivateRobot()
    {
        if(CurrentActiveRobot != null)
            CurrentActiveRobot.Deactivate();
    }
}
