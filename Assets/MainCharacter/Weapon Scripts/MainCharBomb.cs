using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCharBomb : MonoBehaviour
{
    public GameObject bombPrefab;

    public float attackCD = 4f;
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
            Plant();
            attackTimer = 0;
        }
    }

    void Plant()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
    }
}
