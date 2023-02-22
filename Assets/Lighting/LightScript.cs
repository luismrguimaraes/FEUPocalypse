using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Transform mainChar;

    // Update is called once per frame
    void Update()
    {
        transform.position = mainChar.transform.position;

    }
}
