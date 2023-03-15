using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //gameObject.SetActive(false);
    }

    public void Show(int zombieKills, int nightLordKills, int coinsCollected, int XpGained)
    {
        Debug.Log("entered");
        gameObject.GetComponent<Canvas>().enabled = true;

        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].SetText("Zombie Kills: {0}\nNight Lord Kills: {1}\n\nCoins Collected: {2}\nXP Gained: {3}", zombieKills, nightLordKills, coinsCollected, XpGained);
    }


}
