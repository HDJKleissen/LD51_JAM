using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimator : MonoBehaviour
{
    [SerializeField] RobotController robot;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] AnimationClip WalkDown, WalkUp, Interact, Break, Spawn;
    AnimationClip currentClip;

    bool playingInteractingClip;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (robot == null)
        {
            robot = GetComponent<RobotController>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        currentClip = Spawn;
    }

    internal void InitEndGame()
    {
        StartCoroutine(
            CoroutineHelper.Chain(
            CoroutineHelper.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f),
            CoroutineHelper.Do(() => animator.speed = 0)
        ));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentClip != null && !playingInteractingClip)
        {
            animator.Play(currentClip.GetClipName());
        }
    }

    public void AnimateMovement(Vector2 movement)
    {
        if (!robot.Interacting && !isDead)// && currentClip != Spawn)
        {
            if (movement != Vector2.zero)
            {
                animator.speed = 1;

                if (movement.x < 0)
                {
                    spriteRenderer.flipX = true;
                    if (currentClip == Spawn && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f || currentClip != Spawn)
                    {
                        currentClip = WalkDown;
                    }
                }
                else if (movement.x > 0)
                {
                    spriteRenderer.flipX = false;
                    if (currentClip == Spawn && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f || currentClip != Spawn)
                    {
                        currentClip = WalkDown;
                    }
                }

                if (movement.y > 0)
                {
                    if (currentClip == Spawn && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f || currentClip != Spawn)
                    {
                        currentClip = WalkUp;
                    }
                }
                else if (movement.y < 0)
                {
                    if (currentClip == Spawn && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f || currentClip != Spawn)
                    {
                        currentClip = WalkDown;
                    }
                }

            }
            else if (currentClip != Spawn)
            {
                animator.speed = 0;
            }
        }
    }

    internal void PlayBreak()
    {
        robot.Interacting = false;
        playingInteractingClip = false;
        StopAllCoroutines();
        animator.speed = 1;
        animator.ForceStateNormalizedTime(0); // wacky hacky stuff
        currentClip = Break;
        isDead = true;
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PlayerDeath", gameObject);
    }

    public IEnumerator PlayInteract()
    {
        if (!isDead)
        {
            playingInteractingClip = true;
            currentClip = Interact;
            animator.speed = 1;
            animator.ForceStateNormalizedTime(0); // wacky hacky stuff
            animator.Play(Interact.GetClipName());
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            robot.Interacting = false;
            playingInteractingClip = false;
            animator.ForceStateNormalizedTime(0); // wacky hacky stuff
        }
    }
}


public static class ClipUtil
{
    public static string GetClipName(this AnimationClip clip)
    {
        return clip.ToString().Split(' ')[0];
    }
}