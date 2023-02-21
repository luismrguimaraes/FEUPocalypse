using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform centerPoint;
    public SpriteRenderer sr;

    Vector2 movement;

    GameObject mainChar;

    public int maxHp = 100;
    int currentHp;
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");

        currentHp = maxHp;
        isAlive = true;

        sr.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            movement = mainChar.transform.position - centerPoint.position;
            movement.Normalize();

            animator.SetFloat("Horizontal", movement.x);
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            rb.velocity = (moveSpeed * movement);
        }
    }

    [ContextMenu("Damage 50")]
    public void Damage50()
    {
        Damage(50);
    }

    public void Damage(int dmg)
    {
        currentHp -= dmg;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        sr.color = Color.grey;
        // Play dying animation
        animator.SetTrigger("Death");

        // Stop movement
        rb.velocity = new Vector2(0, 0);

        isAlive = false;
        Destroy(gameObject, 0.3f);
    }

}
