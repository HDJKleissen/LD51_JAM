using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkway : Toggleable
{
    [SerializeField] WalkwayDirectionType WalkwayDir;
    public Vector2 WalkwayDirection { get; private set; }
    public float WalkwaySpeed;

    public Dictionary<WalkwayDirectionType, Vector2> directionVectors = new Dictionary<WalkwayDirectionType, Vector2>()
    {
        { WalkwayDirectionType.NorthEast, new Vector2(1, .5f) },
        { WalkwayDirectionType.NorthWest, new Vector2(-1,.5f) },
        { WalkwayDirectionType.SouthEast, new Vector2(1,-.5f) },
        { WalkwayDirectionType.SouthWest, new Vector2(-1,-.5f) },
    };

    public override void HandleStateChange()
    {
        GetComponent<SpriteRenderer>().material.color = IsToggledOn ? Color.green : Color.red; // TODO; remove this when walkway has a proper sprite
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(WalkwayDirection != directionVectors[WalkwayDir])
        {
            WalkwayDirection = directionVectors[WalkwayDir];
        }
    }

    private void OnValidate()
    {
        if (WalkwayDirection != directionVectors[WalkwayDir])
        {
            WalkwayDirection = directionVectors[WalkwayDir];
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(WalkwayDirection.x, WalkwayDirection.y, 0));
    }
}

public enum WalkwayDirectionType
{
    NorthEast,
    NorthWest,
    SouthEast,
    SouthWest
}
