using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;

    private void OnEnable()
    {
        initialValue = new Vector2(0, 0);
    }
}
