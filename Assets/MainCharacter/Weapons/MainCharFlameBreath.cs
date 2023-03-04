using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCharFlameBreath : MonoBehaviour
{
    public GameObject flameBreathPrefab;

    public float attackCD = 2f;
    private float attackTimer = 0;
    MainCharMovementScript mainCharMovement;
    public int XOffset = 5;

    private void Start()
    {
        attackTimer = 0f;
        mainCharMovement = gameObject.GetComponent<MainCharMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer < attackCD)
        {
            attackTimer += Time.deltaTime;
        }
        else if (Mathf.Abs(mainCharMovement.GetFacingDirection().x) > 0.8)
        {
            Breathe();
            attackTimer = 0;
        }
    }

    private void Breathe()
    {
        Debug.Log("Breathing");

        GameObject flame = Instantiate(flameBreathPrefab, transform.position, transform.rotation);
    }
}