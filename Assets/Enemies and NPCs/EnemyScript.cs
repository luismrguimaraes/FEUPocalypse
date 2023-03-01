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
    public HealthBar healthBar;

    public FlashEffectScript flashEffect;
    public GameObject nightLordSpawnEffect;
    public AudioSource hurtSfx;
    public AudioSource nightLordSpawnSfx;


    Vector2 movement;

    GameObject mainChar;

    public int maxHp = 100;
    int currentHp;
    public bool isMoving;


    [SerializeField] RuntimeAnimatorController [] animatorControllers;

    public RuntimeAnimatorController NameToAnimController(string name)
    {
        for (int i = 0; i < animatorControllers.Length; i++)
        {
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
        healthBar.SetHealth(currentHp, maxHp);

        // Set Sprite Renderer color to white (default), just in case
        sr.color = Color.white;



        // Depends on enemy:

        // Change animator
        animator.runtimeAnimatorController = NameToAnimController("NightLord");

        // Instantiate spawn effect and play sfx
        Instantiate(nightLordSpawnEffect, transform.position, Quaternion.identity);
        nightLordSpawnSfx.Play();
    }

    // Update is called once per frame
    void Update()
    {
        movement = mainChar.transform.position - centerPoint.position;
        movement.Normalize();

        animator.SetFloat("Horizontal", movement.x);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = (moveSpeed * movement);
        }
    }

    public void Damage(int dmg)
    {
        currentHp -= dmg;
        healthBar.SetHealth(currentHp, maxHp);

        // Play hurt animation
        animator.SetTrigger("Hurt");

        // Play hurt sfx
        hurtSfx.Play();

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
        isMoving = false;
        Destroy(gameObject, 0.5f);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }

    private void OnRiseEnd()
    {
        isMoving = true;
    }
}
