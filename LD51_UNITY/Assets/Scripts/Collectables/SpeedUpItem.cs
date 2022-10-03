using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : Collectable
{
    [SerializeField] private float speedIncrease;
    

    public override void PickUp()
    {
        Debug.Log("Picking up Item..");
        FMODUnity.RuntimeManager.PlayOneShot("event:/SpeedPickup");
        GameManager.Instance.Player.AddCollectedItem(this);
        Destroy(this.gameObject);
    }

    public override void Apply(RobotController robot)
    {
        robot.MovementSpeed += speedIncrease;
    }
}
