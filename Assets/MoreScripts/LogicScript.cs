using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public float experience = 0f;
    public int coins = 0;
    private LevelSystem levelSystem;
    public GameObject coinsWindow;
    [SerializeField] private GameObject canvas;

    void Awake()
    {
        levelSystem = gameObject.AddComponent<LevelSystem>();
        levelSystem.Init();

        LevelWindow levelWindow = Instantiate(canvas, transform.position, transform.rotation).GetComponentInChildren<LevelWindow>();
        

        levelWindow.Init();
        levelWindow.SetLevelSystem(levelSystem);

    }

    private void Start()
    {
        coinsWindow.GetComponent<CoinsWindow>().Init();
    }

    public void GainXP(int xp)
    {
        levelSystem.AddExperience(xp);
    }

    public void GainCoins(int value)
    {
        coins += value;
        coinsWindow.GetComponent<CoinsWindow>().SetCoinsValue(coins);
    }
}
