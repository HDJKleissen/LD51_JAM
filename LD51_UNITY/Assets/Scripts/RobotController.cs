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


    private float moveLimiter = 0.7f;

    Vector2 input;
    Vector2 moveSpeedModifier;

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

    }

    void Update()
    {
        // -1 x is left, -1 y is down
        if (!Interacting)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


            if (Input.GetButtonDown("Interact") && closestInteractable != null)
            {
                Interacting = true;
                closestInteractable.Interact();
                StartCoroutine(robotAnimator.PlayInteract());
            }
            robotAnimator.AnimateMovement(input);
        }
    }

    public void Deactivate()
    {
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
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if (interactable != null)
        {
            closestInteractable?.ShowUsable(false);
            closestInteractable = collision.GetComponent<Interactable>();
            closestInteractable.ShowUsable(true);
        }

        if (collision.tag == "collectable")
        {
            collision.GetComponent<Collectable>().PickUp();
        }

        Walkway walkway = collision.GetComponent<Walkway>();
        if (walkway != null && walkway.IsToggledOn)
        {
            moveSpeedModifier = walkway.WalkwayDirection * walkway.WalkwaySpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if(interactable != null && interactable == closestInteractable)
        {
            closestInteractable.ShowUsable(false);
            closestInteractable = null;
        }

        Walkway walkway = collision.GetComponent<Walkway>();
        if (walkway != null && walkway.IsToggledOn)
        {
            moveSpeedModifier = Vector2.zero;
        }
    }


    private void Move()
    {
        // isometric babyyy
        input.y *= isometricYMoveModifier;

        body.velocity = new Vector2(input.x, input.y) * MovementSpeed + moveSpeedModifier;
    }
    

    void FixedUpdate()
    {
        if (GameManager.Instance.stateMachine == GameManager.StateMachine.InGame && !Interacting)
        {
            Move();
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }
}
