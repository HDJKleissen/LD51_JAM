using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkway : Toggleable
{
    [SerializeField] WalkwayDirectionType WalkwayDir;
    public Vector2 WalkwayDirection { get; private set; }
    public float WalkwaySpeed;
    [SerializeField] Sprite OnSprite, OffSprite;
    List<SpriteRenderer> childRenderers;

    public Dictionary<WalkwayDirectionType, Vector2> directionVectors = new Dictionary<WalkwayDirectionType, Vector2>()
    {
        { WalkwayDirectionType.NorthEast, new Vector2(1, .5f) },
        { WalkwayDirectionType.NorthWest, new Vector2(-1,.5f) },
        { WalkwayDirectionType.SouthEast, new Vector2(1,-.5f) },
        { WalkwayDirectionType.SouthWest, new Vector2(-1,-.5f) },
    };

    public override void HandleStateChange()
    {
        foreach(SpriteRenderer renderer in childRenderers)
        {
            renderer.sprite = IsToggledOn ? OnSprite : OffSprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        childRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
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
