using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemies"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player Projectiles"));
    }
}
