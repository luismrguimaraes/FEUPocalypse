using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorsCameraScript : MonoBehaviour
{
    public Transform mainChar;

    // Update is called once per frame
    void Update()
    {
        transform.position = mainChar.transform.position + new Vector3(0, 0, -15);
    }
}
