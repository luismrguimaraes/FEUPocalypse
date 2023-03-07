using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public VectorValue startingPosition;
    private bool stopMoving = false;

    Vector2 movement;
    Vector3 facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        facingDirection = new Vector3(0, -1, 0);
        transform.position = startingPosition.initialValue;
    }


    // Update is called once per frame
    private void Update()
    {
        if (stopMoving)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        if (!(movement.x == 0 && movement.y == 0))
        {
            facingDirection = new Vector3(movement.x, movement.y);
        }

        animator.SetFloat("Horizontal", facingDirection.x);
        animator.SetFloat("Vertical", facingDirection.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (stopMoving)
        {
            animator.SetFloat("Speed", 0);
            return;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public Vector3 GetFacingDirection()
    {
        return facingDirection;
    }

    public void SetStopMoving(bool flag)
    {
        stopMoving = flag;
    }

    public bool IsMCFacingUp()
    {
        return facingDirection == new Vector3(0, 1, 0);
    }
}
