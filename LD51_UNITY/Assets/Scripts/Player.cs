using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public Timer Timer;
    public RobotController CurrentActiveRobot;
    public CinemachineVirtualCamera followRobotCamera;

    public List<Collectable> CollectedItems;

    // Start is called before the first frame update
    void Awake()
    {
        Timer = GetComponent<Timer>();
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        Timer.OnTimeOver += DeactivateRobot;
        Timer.OnTimeOver += SpawnRobot;
    }

    private void OnDisable()
    {
        Timer.OnTimeOver -= DeactivateRobot;
        Timer.OnTimeOver -= SpawnRobot;
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
        if (CurrentActiveRobot != null)
            CurrentActiveRobot.Deactivate();
    }
}
