using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipConsole : Interactable
{
    [SerializeField] Animator animator;

    [SerializeField] AnimationClip ErrorClip, RestartClip, ActiveClip;

    bool interactStarted = false;

    public override bool CanInteract() => !interactStarted;

    public override void Interact()
    {
        interactStarted = true;
        spriteRenderer.material.color = Color.white;
        animator.Play(RestartClip.GetClipName());
        // SFX: Oneshot restart
        StartCoroutine(CoroutineHelper.Chain(
            CoroutineHelper.WaitForSeconds(2f),
            CoroutineHelper.Do(() => animator.Play(ActiveClip.GetClipName())),
            CoroutineHelper.Do(() => Debug.Log("replace this debug log with victory music")),
            CoroutineHelper.DelaySeconds(() => /*end game*/Debug.Log("GAME OVER"), /*console sound length*/3f)
        ));

    }

    // Start is called before the first frame update
    void Start()
    {
        animator.Play(ErrorClip.GetClipName());
    }

    // Update is called once per frame
    void Update()
    {
    }
}