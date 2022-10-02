using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Toggleable
{
    [SerializeField] SpriteRenderer DoorSprite;
    [SerializeField] Collider2D DoorCollider;

    public override void HandleStateChange()
    {
        DoorCollider.enabled = !IsToggledOn;
        DoorSprite.enabled = !IsToggledOn;
    }
}
