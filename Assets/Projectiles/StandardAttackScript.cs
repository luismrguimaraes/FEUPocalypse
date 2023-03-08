using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAttackScript : MonoBehaviour
{
    public GameObject hitEffect;
    public Rigidbody2D rb;
    public float duration = 3f;
    public int damage = 10;
    public int upgradeLevelInterval = 3;

    private float timer = 0;
    private LogicScript logicScript;

    private void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player Projectiles and Attacks"));
    }

    private void Update()
    {
        if (timer < duration)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            timer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hit animation
        GameObject hitEffectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitEffectInstance, 1f);

        Collider2D enemyHit = collision.collider;
        if (enemyHit.gameObject.CompareTag("Enemy"))
        {
            // Enemy hit
            if (logicScript.GetLevelNumber() > 2 * upgradeLevelInterval)
                enemyHit.GetComponent<EnemyScript>().Damage(damage *2);
            else
                enemyHit.GetComponent<EnemyScript>().Damage(damage);

            // Piercing Ability check
            if (logicScript.GetLevelNumber() > 3 * upgradeLevelInterval)
            {
                Physics2D.IgnoreCollision(enemyHit, collision.otherCollider);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
