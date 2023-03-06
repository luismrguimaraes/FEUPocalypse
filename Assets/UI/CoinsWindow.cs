using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsWindow : MonoBehaviour
{
    public Text coinsText;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        coinsText = transform.Find("CoinText").GetComponent<Text>();
        SetCoinsValue(0);

    }

    public void SetCoinsValue(int value)
    {
        coinsText.text = "" + value;
    }

    public void SceneTransitionOnStartUpdate()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
