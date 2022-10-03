using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSegment : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D blockingCollider;
    [SerializeField] AnimationClip OpenClip, CloseClip;
    [SerializeField] Sprite OpenSprite, ClosedSprite;
    [SerializeField] BridgeSegment previousSegment;
    public bool IsClosed => spriteRenderer.sprite == ClosedSprite;
    public bool IsOpen => spriteRenderer.sprite == OpenSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(previousSegment != null)
        {
            spriteRenderer.enabled = previousSegment.IsClosed;
            blockingCollider.enabled = !IsClosed || !previousSegment.IsClosed;
        }
        else
        {
            spriteRenderer.enabled = true;
            blockingCollider.enabled = !IsClosed;
        }
    }

    public void SetBridgeState(bool open)
    {
        if (open)
        {
            animator.Play(OpenClip.GetClipName(),0,0);
        }
        else
        {
            animator.Play(CloseClip.GetClipName(),0,0);
        }
    }
}
