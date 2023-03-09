using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorsCameraScript : MonoBehaviour
{
    private Transform mainChar;

    private void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = mainChar.transform.position + new Vector3(0, 0, -15);
    }
}
