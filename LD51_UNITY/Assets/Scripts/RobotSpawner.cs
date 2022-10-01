using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [SerializeField] GameObject robotToSpawn;
    [SerializeField] Transform spawnLocation;
    [field: SerializeField] public bool IsActive { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        //add itself to the list
        GameManager.Instance.RobotSpawners.Add(this);

        if (IsActive)
        {
            TurnOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void TurnOff()
    {
        //animate
        GetComponent<Renderer>().material.color = Color.white;
        IsActive = false;
        //sfx
    }

    public void TurnOn()
    {
        //animate
        GetComponent<Renderer>().material.color = Color.red;
        //sfx

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

    public void SpawnRobot()
    {
        GameObject robot = Instantiate(robotToSpawn, spawnLocation.position, Quaternion.identity, GameManager.Instance.World.transform);
        GameManager.Instance.Player.GetComponent<Player>().CurrentActiveRobot = robot.GetComponent<RobotController>();
        GameManager.Instance.Player.GetComponent<Player>().followRobotCamera.Follow = robot.transform;
    }

}
