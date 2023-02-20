using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    GameObject mainChar;

    float hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        movement = mainChar.transform.position - transform.position;
        movement.Normalize();

        animator.SetFloat("Horizontal", movement.x);

    }

    private void FixedUpdate()
    {
        rb.velocity = (moveSpeed * movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player Projectiles"))
        {
            // Enemy hit
            Damage(100);
        }
    }

    private void Damage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
