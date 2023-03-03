using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBreathScript : MonoBehaviour
{
    public GameObject hitEffect;
    public Rigidbody2D rb;
    public float dmgPerSecond = 10;
    private bool isFlameActive = false;

    private void Start()
    {
        GameObject mainChar = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(mainChar.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player Projectiles"));
    }


    void OnAnimatorEnd()
    {
        Destroy(gameObject);
    }

    void SetFlameActive()
    {
        isFlameActive = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Hit animation
        GameObject hitEffectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitEffectInstance, 5f);

        Collider2D enemyHit = collision.collider;
        if (isFlameActive && enemyHit.gameObject.CompareTag("Enemy"))
        {
            // Enemy hit
            enemyHit.GetComponent<EnemyScript>().Damage(dmgPerSecond * Time.deltaTime);
        }
    }
}
