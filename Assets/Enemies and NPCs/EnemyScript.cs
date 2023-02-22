using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform centerPoint;
    public SpriteRenderer sr;

    public FlashEffectScript flashEffect;

    Vector2 movement;

    GameObject mainChar;

    public int maxHp = 100;
    int currentHp;
    public bool isAlive;


    [SerializeField] RuntimeAnimatorController [] animatorControllers;

    public RuntimeAnimatorController NameToAnimController(string name)
    {
        for (int i = 0; i < animatorControllers.Length; i++)
        {
            Debug.Log(i);
            Debug.Log(animatorControllers[i].name);
            if (animatorControllers[i].name == name)
            {
                return animatorControllers[i];
            }
        }

        Debug.LogWarning("Invalid animator controller name " + name);
        return null;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("Start");
        mainChar = GameObject.FindGameObjectWithTag("Player");

        // Set current hp to full
        currentHp = maxHp;
        isAlive = true;

        // Set Sprite Renderer color to white (default), just in case
        sr.color = Color.white;

        // Change animator
        animator.runtimeAnimatorController = NameToAnimController("Zombie");
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

    public void Damage(int dmg)
    {
        currentHp -= dmg;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        // If dead, die
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            // Else, flash white
            flashEffect.Flash();
        }
    }

    private void Die()
    {
        // Change Sprite Renderer color to grey
        sr.color = Color.grey;

        // Play dying animation
        animator.SetTrigger("Death");

        // Stop movement
        rb.velocity = new Vector2(0, 0);

        // Disable script
        isAlive = false;
        Destroy(gameObject, 0.5f);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
