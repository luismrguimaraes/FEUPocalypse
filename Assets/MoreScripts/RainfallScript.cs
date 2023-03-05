using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainfallScript : MonoBehaviour
{
    private GameObject mainChar;

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainChar.transform.position;
    }
}
