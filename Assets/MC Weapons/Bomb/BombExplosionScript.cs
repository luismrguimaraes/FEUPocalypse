using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionScript : MonoBehaviour
{
    public float damage = 50;
    public Rigidbody2D rb;

    private bool isFlameActive;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player Projectiles and Attacks"));

        SetFlameInactive();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D enemyHit = collision.collider;
        if (enemyHit.gameObject.CompareTag("Enemy") && isFlameActive)
        {
            // Enemy hit
            enemyHit.GetComponent<EnemyScript>().Damage(damage);
        }
        Destroy(gameObject, 2);
    }
}
