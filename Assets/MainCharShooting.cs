using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharShooting : MonoBehaviour
{
    public GameObject bulletPrefab;


    public float standardAttackForce = 20f;
    public float standardAttackCD = 2f;
    private float standardAttackTimer = 0;

    private void Start()
    {
        standardAttackTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (standardAttackTimer < standardAttackCD)
        {
            standardAttackTimer = standardAttackTimer + Time.deltaTime;
        }
        else
        {
            Shoot();
            standardAttackTimer = 0;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(gameObject.GetComponent<MainCharacterScript>().GetFacingDirection() * standardAttackForce, ForceMode2D.Impulse);
    }
}
