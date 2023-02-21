using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCharShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public bool shooting;

    public float standardAttackForce = 10f;
    public float standardAttackCD = 1f;
    private float standardAttackTimer = 0;

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
        standardAttackTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
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
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(gameObject.GetComponent<MainCharacterMovementScript>().GetFacingDirection() * standardAttackForce, ForceMode2D.Impulse);
    }
}
