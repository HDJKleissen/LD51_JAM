using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Toggleable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator DoorAnimator;
    [SerializeField] Collider2D DoorCollider;
    [SerializeField] AnimationClip DoorOpenClip, DoorCloseClip;

    public override void HandleStateChange()
    {
        // Needs to be on ground "layer" when open
        spriteRenderer.sortingOrder = IsToggledOn ? 1 : 0;
        DoorCollider.enabled = IsToggledOn;
        DoorAnimator.Play(IsToggledOn ? DoorCloseClip.GetClipName() : DoorOpenClip.GetClipName());
    }
}
