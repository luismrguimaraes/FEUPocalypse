using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LogicScript : MonoBehaviour
{
    public enum Weapons { STANDARD_ATTACK, FLAME_BREATH }
    public bool[] mcAcquiredWeapons;

    private GameObject mainChar;
    private SceneManagerScript sceneManagerScript;

    public int coins = 0;
    private LevelSystem levelSystem;
    public GameObject coinsWindow;
    [SerializeField] private GameObject levelWindowCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        levelSystem = gameObject.AddComponent<LevelSystem>();
        levelSystem.Init();

        LevelWindow levelWindow = levelWindowCanvas.GetComponentInChildren<LevelWindow>();

        levelWindow.Init();
        levelWindow.SetLevelSystem(levelSystem);

        mainChar = GameObject.FindGameObjectWithTag("Player");
        mcAcquiredWeapons = new bool[] { true, false };

        sceneManagerScript = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();

        coinsWindow = GameObject.FindGameObjectWithTag("CoinsWindow");
        coinsWindow.GetComponent<CoinsWindow>().Init();

        DontDestroyOnLoad(gameObject);
    }

    void DisableAllWeapons()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(Weapons)).Length; i++)
        {
            switch (i)
            {
                case (int)Weapons.STANDARD_ATTACK:
                    mainChar.GetComponent<MainCharStandardShot>().enabled = false;
                    break;
                case (int)Weapons.FLAME_BREATH:
                    mainChar.GetComponent<MainCharFlameBreath>().enabled = false;
                    break;
                default:
                    break;
            }
        }
    }

    void EnableAcquiredWeapons()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(Weapons)).Length; i++)
        {
            switch (i)
            {
                case (int)Weapons.STANDARD_ATTACK:
                    if (mcAcquiredWeapons[i])
                    {
                        mainChar.GetComponent<MainCharStandardShot>().enabled = true;
                    }
                    break;
                case (int)Weapons.FLAME_BREATH:
                    if (mcAcquiredWeapons[i])
                    {
                        mainChar.GetComponent<MainCharFlameBreath>().enabled = true;
                    }
                    break;
                default:
                    break;
            }
        }
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

    public void SceneTransitionOnStartUpdate()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");

        DisableAllWeapons();
        if (SceneManager.GetActiveScene().name == "Outside")
        {
            EnableAcquiredWeapons();
        }
    }

}
