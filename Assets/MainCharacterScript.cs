using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
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

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
