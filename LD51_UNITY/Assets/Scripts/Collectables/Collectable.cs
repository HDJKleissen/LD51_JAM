using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] public Sprite UIImage;
    public abstract void PickUp();

    public abstract void Apply(RobotController robot);
}
