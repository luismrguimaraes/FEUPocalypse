using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharHealthScript : MonoBehaviour
{
    public float maxHp = 500;
    float currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    public void Damage(float damage)
    {
        currentHp -= damage;
        //Debug.Log("MC HP: " + currentHp);
    }
}
