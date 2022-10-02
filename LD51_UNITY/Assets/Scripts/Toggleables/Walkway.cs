using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkway : Toggleable
{
    public Vector2 WalkwayDirection;
    public float WalkwaySpeed; 

    public override void HandleStateChange()
    {
        GetComponent<SpriteRenderer>().material.color = IsToggledOn ? Color.green : Color.red;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
