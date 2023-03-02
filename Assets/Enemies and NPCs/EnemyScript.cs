using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Transform centerPoint;
    public SpriteRenderer sr;

    public HealthBar healthBar;
    public FlashEffectScript flashEffect;

    // Prefabs
    public GameObject nightLordSpawnEffect;
    public GameObject fullVisionDrop;

    // Audio Sources
    public AudioSource hurtSfx;
    public AudioSource nightLordSpawnSfx;

    GameObject mainChar;

    public float moveSpeed;
    public float fullVisionDropChance;
    public int maxHp = 100;

    Vector2 movement;
    int currentHp;
    bool isMoving = false;
    int isRecovering = 0;


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
        mainChar = GameObject.FindGameObjectWithTag("Player");

        // Set current hp to full
        currentHp = maxHp;
        healthBar.SetHealth(currentHp, maxHp);

        // Set Sprite Renderer color to white (default), just in case
        sr.color = Color.white;
    }

    public void initZombie()
    {
        // Change animator
        animator.runtimeAnimatorController = NameToAnimController("Zombie");

        moveSpeed = 2;
        maxHp = 60;

        // set drops chances
        fullVisionDropChance = 0.2f;
    }

    public void initNightLord()
    {
        // Change animator
        animator.runtimeAnimatorController = NameToAnimController("NightLord");

        // Instantiate spawn effect and play sfx
        Instantiate(nightLordSpawnEffect, transform.position, Quaternion.identity);
        nightLordSpawnSfx.Play();

        maxHp = 200;
        moveSpeed = 1;

        // set drops chances
        fullVisionDropChance = 0.4f;
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
            rb.velocity = moveSpeed * movement;
        }
        else
        {
            // If recovering from hit
            if (isRecovering > 0)
            {
                rb.velocity = new Vector2(0, 0);
                isRecovering--;
                if (isRecovering == 0)
                {
                    isMoving = true;
                }
            }
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

        // Stop for 12 frames
        isRecovering = 12;
        isMoving = false;

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

        // Drop? Collectibles 
        int randomValue = Random.Range(0, 100);
        if (randomValue < fullVisionDropChance * 100)
        {
            Instantiate(fullVisionDrop, centerPoint.transform.position, Quaternion.identity);
        };

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
