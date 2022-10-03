using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipConsole : Interactable
{
    public UnityEvent OnAnimationFinish;
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Reboot");
        StartCoroutine(CoroutineHelper.Chain(
            CoroutineHelper.WaitForSeconds(2f),
            CoroutineHelper.Do(() => animator.Play(ActiveClip.GetClipName())),
            CoroutineHelper.Do(() => Debug.Log("replace this debug log with victory music")),
            CoroutineHelper.DelaySeconds(() => OnAnimationFinish.Invoke(), /*console sound length*/3f)
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
