using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsWindow : MonoBehaviour
{
    public Text coinsText;
    // Start is called before the first frame update

    public void Init()
    {
        coinsText = transform.Find("CoinText").GetComponent<Text>();
        SetCoinsValue(0);
    }

    public void SetCoinsValue(int value)
    {
        coinsText.text = "" + value;
    }
}
