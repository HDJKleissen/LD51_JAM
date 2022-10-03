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
    public bool PlayedNearDeathSound = false;

    // Start is called before the first frame update
    void Awake()
    {
        Timer = GetComponent<Timer>();
    }

    private void Start()
    {
        MusicManager.instance.SetMusicSection(1);
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
        if (Timer.TimeLeft < 2 && !PlayedNearDeathSound)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/NearDeath");
            PlayedNearDeathSound = true;
        }
    }

    public void AddCollectedItem(Collectable collectable)
    {
        //animation
        //sfx
        CollectedItems.Add(collectable);
    }

    void SpawnRobot()
    {
        //playedNearDeathSound = false;
        foreach (RobotSpawner rs in GameManager.Instance.RobotSpawners)
        {
            if (rs.IsActive)
            {
                rs.SpawnRobot(CollectedItems);
                return;
            }
        }

        Debug.LogError("No Robot Spawners are active..");
    }

    public void StopBeeping()
    {
        // Stop playing beeping if it is playing
    }

    void DeactivateRobot()
    {
        if (CurrentActiveRobot != null)
            CurrentActiveRobot.Deactivate();
    }
}