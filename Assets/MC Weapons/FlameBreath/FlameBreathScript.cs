using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBreathScript : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject mainChar;
    public float dmgPerSecond = 10;
    private float damageTimer = 0;
    public float damageInterval = 0.2f;

    private bool isFlameActive = false;
    private bool isFiringRight;

    private void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(mainChar.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player Projectiles/Attacks"));

        // Flame has build-up time
        SetFlameInactive();
    }


    void OnAnimatorEnd()
    {
        Destroy(gameObject);
    }

    void SetFlameActive()
    {
        isFlameActive = true;
    }

    void SetFlameInactive()
    {
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
            transform.position = mainChar.transform.position;
            transform.rotation = Quaternion.identity;
        }
        else if (!isFiringRight)
        {
            //transform.SetPositionAndRotation(mainChar.transform.position + new Vector3(-1.8f, 0.5f, 0), Quaternion.Euler(0, 180, 0));
            transform.position = mainChar.transform.position;
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
                // GameObject hitEffectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
                // Destroy(hitEffectInstance, 5f);

                // Damage enemy
                enemyHit.GetComponent<EnemyScript>().Damage(dmgPerSecond * Time.deltaTime);
            }
        }
    }
}
