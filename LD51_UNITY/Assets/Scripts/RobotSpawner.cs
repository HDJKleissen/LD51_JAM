using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : Interactable
{
    [SerializeField] Animator animator;

    [SerializeField] GameObject robotToSpawn;
    [SerializeField] Transform spawnLocation;
    [field: SerializeField] public bool IsActive { get; private set; }

    public override bool CanInteract()
    {
        return !IsActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        //add itself to the list
        GameManager.Instance.RobotSpawners.Add(this);

        if (IsActive)
        {
            TurnOn();
            SpawnRobot(new List<Collectable>());
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void TurnOff()
    {
        GetComponent<Renderer>().material.color = Color.gray;
        IsActive = false;
    }

    public void TurnOn()
    {
        GetComponent<Renderer>().material.color = Color.white;

        //turn others off
        foreach (RobotSpawner rs in GameManager.Instance.RobotSpawners)
        {
            //skip itself
            if (this == rs)
                continue;

            rs.TurnOff();
        }

        IsActive = true;
    }

    public void SpawnRobot(List<Collectable> collectedItems)
    {
        animator.Play("printer_print", 0, 0);
        // SFX: Oneshot robot creation
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PrintRobot", gameObject);
        StartCoroutine(CoroutineHelper.DelaySeconds(() =>
        {
            GameObject robot = Instantiate(robotToSpawn, spawnLocation.position, Quaternion.identity, GameManager.Instance.World.transform);
            robot.GetComponent<RobotController>().BaseSpeed = robot.GetComponent<RobotController>().MovementSpeed;
            GameManager.Instance.Player.CurrentActiveRobot = robot.GetComponent<RobotController>();
            GameManager.Instance.Player.followRobotCamera.Follow = robot.transform;
            GameManager.Instance.Player.Timer.RestartTimer();
            GameManager.Instance.Player.PlayedNearDeathSound = false;
            //apply item effects
            foreach (Collectable item in collectedItems)
            {
                item.Apply(robot.GetComponent<RobotController>());
            }
        }, 1f));
    }

    public override void Interact()
    {
        if (!IsActive)
        {
            TurnOn();
        }
    }
}
