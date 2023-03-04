using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLordSpawnEffectScript : MonoBehaviour
{
    // Self-destruct
    void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
