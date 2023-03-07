using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCharStandardShot : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float attackForce = 10f;
    public float attackCD = 1f;
    private float attackTimer = 0;
    private LogicScript logicScript;

    private void Start()
    {
        attackTimer = 0f;
        logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
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

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(gameObject.GetComponent<MainCharMovementScript>().GetFacingDirection() * attackForce, ForceMode2D.Impulse);

        if (logicScript.GetLevelNumber() >= bullet.GetComponent<StandardAttackScript>().upgradeLevelInterval)
        {
            // Fire backwards as well;
            GameObject bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce( - gameObject.GetComponent<MainCharMovementScript>().GetFacingDirection() * attackForce, ForceMode2D.Impulse);

        }
    }
}
