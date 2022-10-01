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
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SpawnRobot()
    {
        GameObject robot = Instantiate(robotToSpawn, spawnLocation.position, Quaternion.identity, GameManager.Instance.World.transform);
        GameManager.Instance.Player.GetComponent<Player>().CurrentActiveRobot = robot.GetComponent<RobotController>();
        GameManager.Instance.Player.GetComponent<Player>().followRobotCamera.Follow = robot.transform;
    }

}
