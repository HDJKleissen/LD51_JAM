using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] RobotAnimator robotAnimator;
    private Rigidbody2D body;

    [field: SerializeField] public float MovementSpeed { get; set; }
    [SerializeField] float isometricYMoveModifier = .6f;

    public bool Interacting = false;
    Interactable closestInteractable;

    
    Vector2 input;
    Vector2 moveSpeedModifier;

    private FMOD.Studio.EventInstance movementSound;
    internal float BaseSpeed;

    internal void InitEndGame()
    {
        movementSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameManager.Instance.Player.StopBeeping();
        GameManager.Instance.Player.Timer.Stop();
        body.velocity = Vector2.zero;
        enabled = false;
        robotAnimator.InitEndGame();
    }

    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
        if(robotAnimator == null)
        {
            robotAnimator = GetComponent<RobotAnimator>();
        }
        movementSound = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerMovement");
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (GameManager.Instance.Player.infoBox.activeInHierarchy)
            {
                StartCoroutine(CoroutineHelper.DelaySeconds(() => GameManager.Instance.Player.infoBox.SetActive(false), 1));
            }
        }

        if(BaseSpeed != MovementSpeed)
        {
            BaseSpeed = MovementSpeed;
            SpeedUpSound(1);
        }
        // -1 x is left, -1 y is down
        Vector2 prevInput = input;

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (prevInput == Vector2.zero && input != Vector2.zero)
        {
            // SFX: Start playing movement sound
            movementSound.start();
        }
        else if (prevInput != Vector2.zero && input == Vector2.zero)
        {
            // SFX: Stop playing movement sound
            movementSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }


        if (Input.GetButtonDown("Interact") && closestInteractable != null)
        {
            Interacting = true;
            closestInteractable.Interact();
            if (!closestInteractable.CanInteract())
            {
                SetInteractable(null);
            }
            StartCoroutine(robotAnimator.PlayInteract());
        }

        robotAnimator.AnimateMovement(input);
    }

    public void Deactivate()
    {
        // SFX: Oneshot Death sound
        movementSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        robotAnimator.PlayBreak();
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        if (GetComponent<DisplayTimer>() != null)
        {
            Destroy(GetComponent<DisplayTimer>().overheadText);
            Destroy(GetComponent<DisplayTimer>());
        }
        Destroy(this);
    }

    private void OnDestroy()
    {
        SpeedUpSound(0);
        movementSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        movementSound.release();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collectable")
        {
            collision.GetComponent<Collectable>().PickUp();
        }

        Walkway walkway = collision.GetComponent<Walkway>();
        if (walkway != null && walkway.IsToggledOn)
        {
            moveSpeedModifier = walkway.WalkwayDirection * walkway.WalkwaySpeed;
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Conveyor", gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if (interactable != null)
        {
            if(closestInteractable == null || Vector2.Distance(transform.position, interactable.transform.position) < Vector2.Distance(transform.position, closestInteractable.transform.position))
            {
                SetInteractable(interactable);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if(interactable != null && interactable == closestInteractable)
        {
            SetInteractable(null);
        }

        Walkway walkway = collision.GetComponent<Walkway>();
        if (walkway != null && walkway.IsToggledOn)
        {
            moveSpeedModifier = Vector2.zero;
        }
    }

    void SetInteractable(Interactable interactable)
    {
        closestInteractable?.ShowUsable(false);
        closestInteractable = interactable;
        closestInteractable?.ShowUsable(true);
    }

    private void Move()
    {
        // isometric babyyy
        input.y *= isometricYMoveModifier;

        body.velocity = new Vector2(input.x, input.y) * MovementSpeed + moveSpeedModifier;
    }
    

    void FixedUpdate()
    {
        if (GameManager.Instance.stateMachine == GameManager.StateMachine.InGame)
        {
            Move();
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    public void SpeedUpSound(int value)
    {
        Debug.Log("setting speed");
        movementSound.setParameterByName("SpeedUp", value, false);
    }
}
