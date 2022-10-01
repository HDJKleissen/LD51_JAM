using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [field: SerializeField] public float MovementSpeed { get; set; }
    
    private Rigidbody2D body;

    private float horizontalInput;
    private float verticalInput;
    private float moveLimiter = 0.7f;

    Vector2 moveSpeedModifier;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 is left
        verticalInput = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    public void Deactivate() {
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<DisplayTimer>().overheadText);
        Destroy(GetComponent<DisplayTimer>());
        Destroy(this);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "interactable")
        {
            collision.GetComponent<Interactable>().Interact();
        }

        if(collision.tag == "robotSpawner")
        {
            collision.GetComponent<RobotSpawner>().TurnOn();
        }

        if (collision.tag == "collectable")
        {
            collision.GetComponent<Collectable>().PickUp();
        }
        Walkway walkway = collision.GetComponent<Walkway>();
        if (walkway != null && walkway.IsToggledOn)
        {
            //float modifier = Vector2.Dot(walkway.WalkwayDirection.normalized, new Vector2(horizontalInput, verticalInput).normalized);
            moveSpeedModifier = walkway.WalkwayDirection * walkway.WalkwaySpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Walkway walkway = collision.GetComponent<Walkway>();
        if (walkway != null && walkway.IsToggledOn)
        {
            moveSpeedModifier = Vector2.zero;
        }
    }

    //spritedefault rotation should be looking right
    private void Rotate()
    {
        Vector2 moveDirection = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Move()
    {
        if (horizontalInput != 0 && verticalInput != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontalInput *= moveLimiter;
            verticalInput *= moveLimiter;
        }

        body.velocity = new Vector2(horizontalInput, verticalInput) * MovementSpeed + moveSpeedModifier;
    }
    

    void FixedUpdate()
    {
        if (GameManager.Instance.stateMachine == GameManager.StateMachine.InGame)
        {
            Move();
        }
    }
}
