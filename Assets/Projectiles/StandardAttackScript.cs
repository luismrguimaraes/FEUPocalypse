using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAttackScript : MonoBehaviour
{
    public GameObject hitEffect;
    public float duration = 10;
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
        GameObject hitEffectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitEffectInstance, 5f);
        Destroy(gameObject);
    }
}
