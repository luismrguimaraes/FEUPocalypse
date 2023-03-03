using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCharStandardShot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public bool shooting;

    public float attackForce = 10f;
    public float attackCD = 1f;
    private float attackTimer = 0;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Outside")
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }
        attackTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {
            if (attackTimer < attackCD)
            {
                attackTimer = attackTimer + Time.deltaTime;
            }
            else
            {
                Shoot();
                attackTimer = 0;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-gameObject.GetComponent<MainCharMovementScript>().GetFacingDirection() * attackForce, ForceMode2D.Impulse);
    }
}
