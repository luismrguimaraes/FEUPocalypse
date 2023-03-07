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
    public GameObject coinDrop;
    public GameObject moveSpeedBoostDrop;

    // Audio Sources
    public AudioSource nightLordSpawnSfx;

    // Other
    GameObject mainChar;
    GameObject logicManager;

    public float moveSpeed;
    public float maxHp = 100;
    public float dmgPerSecond = 5;
    public int experienceDrop = 25;
    public int coinAmountDrop = 1;
    public float powerUpDropChance;

    Vector2 movement;
    float currentHp;
    bool isMoving = false;
    int isRecovering = 0; // recovering frames after being hit

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
    public void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        logicManager = GameObject.FindGameObjectWithTag("LogicManager");

        // Set current hp to full
        currentHp = maxHp;
        healthBar.SetHealth(currentHp, maxHp);

        // Set Sprite Renderer color to white (default), just in case
        sr.color = Color.white;

        DontDestroyOnLoad(gameObject);
    }

    public void InitZombie()
    {
        // Change animator
        animator.runtimeAnimatorController = NameToAnimController("Zombie");

        // Set attributes
        moveSpeed = 2;
        maxHp = 12;
        dmgPerSecond = 50;
        rb.mass = 1;
        coinAmountDrop = 1;

        // set drop chances
        powerUpDropChance = 0.1f;
    }

    public void InitNightLord()
    {
        // Change animator
        animator.runtimeAnimatorController = NameToAnimController("NightLord");

        // Instantiate spawn effect and play sfx
        Instantiate(nightLordSpawnEffect, transform.position, Quaternion.identity);
        nightLordSpawnSfx.Play();

        // Set attributes
        moveSpeed = 2.5f;
        maxHp = 200;
        dmgPerSecond = 150;
        rb.mass = 1;
        coinAmountDrop = 20;

        // set drop chances
        powerUpDropChance = 0.15f;

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

    public void Damage(float dmg)
    {
        currentHp -= dmg;
        healthBar.SetHealth(currentHp, maxHp);

        // Play hurt animation
        animator.SetTrigger("Hurt");

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

        // Invokes the gain experience function in game logic manager
        logicManager.GetComponent<LogicScript>().GainXP(experienceDrop);

        // Drop? Collectibles 
        RollDropDice(powerUpDropChance);

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

    private void RollDropDice(float dropChance)
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue < dropChance * 100)
        {
            // Dropping a power-up Item 
            if (randomValue % 2 == 0)
            {
                // Dropping fullVisionDrop
                Instantiate(fullVisionDrop, centerPoint.transform.position, Quaternion.identity);

            }
            else
            {
                // Dropping moveSpeedBoostDropChance
                Instantiate(moveSpeedBoostDrop, centerPoint.transform.position, Quaternion.identity);
            }
        }
        else
        {
            GameObject droppedCoin = Instantiate(coinDrop, centerPoint.transform.position, Quaternion.identity);
            droppedCoin.GetComponent<CoinScript>().Init(coinAmountDrop);
        }
    }

    private void RollDropDice(GameObject dropPrefab, float dropChance)
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue < dropChance * 100)
        {
           Instantiate(dropPrefab, centerPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(coinDrop, centerPoint.transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            logicManager.GetComponent<LogicScript>().Damage(dmgPerSecond * Time.deltaTime);
        }
    }
}
