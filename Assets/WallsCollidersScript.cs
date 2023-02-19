using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsCollidersScript : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Main Character"))
        {
            audioSource.Play();
        }
    }
}
