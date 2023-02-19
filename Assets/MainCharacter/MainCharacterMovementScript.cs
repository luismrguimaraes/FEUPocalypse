using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    Vector3 facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        facingDirection = new Vector3(0, -1, 0);
    }


    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(movement.x) + Mathf.Abs(movement.y) > 1)
        {
            movement.x *= Mathf.Cos(Mathf.PI / 4);
            movement.y *= Mathf.Sin(Mathf.PI / 4);
        }

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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public Vector3 GetFacingDirection()
    {
        return facingDirection;
    }
}
