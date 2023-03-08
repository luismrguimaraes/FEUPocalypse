using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float duration = 1;
    public GameObject explosionPrefab;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (timer < duration)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            timer = 0;
        }
    }
}
