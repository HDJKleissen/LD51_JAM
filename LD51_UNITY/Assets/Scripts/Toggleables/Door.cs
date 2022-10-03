using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Door : Toggleable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator DoorAnimator;
    [SerializeField] Collider2D DoorCollider;
    [SerializeField] AnimationClip DoorOpenClip, DoorCloseClip;
    [SerializeField] Sprite OpenDoorSprite, ClosedDoorSprite;
    [SerializeField] ShadowCaster2D shadowCaster;

    public void Start()
    {
        DoorAnimator.speed = 0;
        DoorAnimator.enabled = false;
    }

    private void Update()
    {
        spriteRenderer.sortingOrder = spriteRenderer.sprite == OpenDoorSprite ? 0 : 1;
        shadowCaster.castsShadows = spriteRenderer.sprite == OpenDoorSprite ? false : true;
    }

    public override void HandleStateChange()
    {
        DoorAnimator.enabled = true;
        DoorAnimator.speed = 1;
        // Needs to be on ground "layer" when open
        DoorCollider.enabled = IsToggledOn;
        DoorAnimator.Play(IsToggledOn ? DoorCloseClip.GetClipName() : DoorOpenClip.GetClipName());
    }

    private void OnValidate()
    {
        spriteRenderer.sprite = IsToggledOn ? ClosedDoorSprite : OpenDoorSprite;
        DoorCollider.enabled = IsToggledOn;
    }
}
