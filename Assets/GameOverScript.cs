using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverScript : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
