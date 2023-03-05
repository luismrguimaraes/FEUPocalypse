using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsWindow : MonoBehaviour
{
    public Text coinsText;
    // Start is called before the first frame update
    void Start()
    {
        coinsText = transform.Find("CoinText").GetComponent<Text>();

    }

    public void SetCoinsValue(int value)
    {
        coinsText.text = "" + value;
    }
}
