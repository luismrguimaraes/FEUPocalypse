using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionScript : MonoBehaviour
{
    public float damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D enemyHit = collision.collider;
        if (enemyHit.gameObject.CompareTag("Enemy"))
        {
            // Enemy hit
            enemyHit.GetComponent<EnemyScript>().Damage(damage);

            Destroy(gameObject);
        }
    }
}
