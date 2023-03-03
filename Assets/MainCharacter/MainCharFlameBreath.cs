using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCharFlameBreath : MonoBehaviour
{
    //public GameObject flamePrefab;

    public float attackForce = 10f;
    public float attackCD = 1f;
    private float attackTimer = 0;

    private void Start()
    {
        attackTimer = 0f;
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
            //Shoot();
            attackTimer = 0;
        }
    }
}
