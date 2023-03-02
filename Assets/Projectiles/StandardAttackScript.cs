using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAttackScript : MonoBehaviour
{
    public GameObject hitEffect;
    public Rigidbody2D rb;
    public float duration = 1;
    public int damage = 10;

    private float timer = 0;

    private void Start()
    {
        GameObject mainChar = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(mainChar.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player Projectiles"))
        {
            return;
        }

        // Hit animation
        GameObject hitEffectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitEffectInstance, 5f);

        Collider2D enemyHit = collision.collider;
        if (enemyHit.gameObject.CompareTag("Enemy"))
        {
            // Enemy hit
            enemyHit.GetComponent<EnemyScript>().Damage(damage);
        }

        Destroy(gameObject);
    }
}
