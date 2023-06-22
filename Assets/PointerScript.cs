using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    public Vector3 initialPosition;

    public void Next()
    {
        transform.Find("Pointer").Translate(new Vector3(-1.6f, 0, 0));
    }
    public void Previous()
    {
        transform.Find("Pointer").Translate(new Vector3(1.6f, 0, 0));
    }
    public void ResetPosition()
    {
        transform.transform.Find("Pointer").transform.position = initialPosition;
    }
}
