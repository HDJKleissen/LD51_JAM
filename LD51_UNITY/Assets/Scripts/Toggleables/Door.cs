using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Toggleable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator DoorAnimator;
    [SerializeField] Collider2D DoorCollider;
    [SerializeField] AnimationClip DoorOpenClip, DoorCloseClip;
    [SerializeField] Sprite OpenDoorSprite;

    private void Update()
    {
        spriteRenderer.sortingOrder = spriteRenderer.sprite == OpenDoorSprite ? 0 : 1;
    }

    public override void HandleStateChange()
    {
        // Needs to be on ground "layer" when open
        DoorCollider.enabled = IsToggledOn;
        DoorAnimator.Play(IsToggledOn ? DoorCloseClip.GetClipName() : DoorOpenClip.GetClipName());
    }
}
