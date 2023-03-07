using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBreathScript : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject mainChar;
    public float dmgPerSecond = 150;
    private float damageTimer;
    public float damageInterval = 0.5f;
    public AudioSource flameBreathSFX;
    public Rigidbody2D rb;

    private bool isFlameActive = false;
    private bool isFiringRight;
    private Vector3 initialPosition;

    private void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player Projectiles and Attacks"));

        // Flame has build-up time
        SetFlameInactive();

        // Store initial position
        initialPosition = transform.position;

        damageTimer = damageInterval; // Damage right away
        flameBreathSFX.Play();
    }


    void OnAnimationEnd()
    {
        Destroy(gameObject, 3f);
    }

    void SetFlameActive()
    {
        rb.simulated = true;
        isFlameActive = true;
    }

    void SetFlameInactive()
    {
        rb.simulated = false;
        isFlameActive = true;
    }

    private void Update()
    {
        if (mainChar.GetComponent<MainCharMovementScript>().GetFacingDirection().x > 0)
        {
            isFiringRight = true;
        }
        else if (mainChar.GetComponent<MainCharMovementScript>().GetFacingDirection().x < 0)
        {
            isFiringRight = false;
        }

        if (isFiringRight)
        {
            //transform.SetPositionAndRotation(mainChar.transform.position + new Vector3( 1.8f, 0.5f, 0), Quaternion.identity);
            transform.position = mainChar.transform.position + initialPosition;
            transform.rotation = Quaternion.identity;
        }
        else if (!isFiringRight)
        {
            //transform.SetPositionAndRotation(mainChar.transform.position + new Vector3(-1.8f, 0.5f, 0), Quaternion.Euler(0, 180, 0));
            transform.position = mainChar.transform.position + initialPosition;
            transform.rotation = Quaternion.identity;
            transform.RotateAround( mainChar.transform.position, mainChar.transform.up, 180);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D enemyHit = collision.collider;
        if (isFlameActive && enemyHit.gameObject.CompareTag("Enemy"))
        {
            // Enemy hit
            if (damageTimer < damageInterval)
            {
                damageTimer += Time.deltaTime;
            }
            else
            {
                // Reset Timer
                damageTimer = 0;

                // Hit animation
                GameObject hitEffectInstance = Instantiate(hitEffect, collision.gameObject.GetComponent<EnemyScript>().centerPoint.position, Quaternion.identity);
                Destroy(hitEffectInstance, 1f);

                // Damage enemy
                enemyHit.GetComponent<EnemyScript>().Damage(dmgPerSecond * Time.deltaTime);
            }
        }
    }
}
