using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    public Vector3 initialPosition;

    public void Next()
    {
        transform.Translate(new Vector3(0, -1.75f, 0));
    }
    public void Previous()
    {
        transform.Translate(new Vector3(0, 1.75f, 0));
    }
    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
