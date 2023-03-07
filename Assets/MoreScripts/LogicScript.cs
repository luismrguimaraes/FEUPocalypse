using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class LogicScript : MonoBehaviour
{
    public enum Weapons { STANDARD_ATTACK, FLAME_BREATH }
    public bool[] mcAcquiredWeapons;

    private GameObject mainChar;
    private SceneManagerScript sceneManagerScript;

    public int coins = 0;
    public float myMaxHealth = 500.0f;
    public float myCurrHealth;
    private LevelSystem levelSystem;
    public GameObject coinsWindow;
    public GameObject myStatusBar;

    [SerializeField] private GameObject levelWindowCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        mcAcquiredWeapons = new bool[] { true, false };

        sceneManagerScript = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();

        RestoreToMaxHealth();

        levelSystem = gameObject.AddComponent<LevelSystem>();
        levelSystem.Init();

        LevelWindow levelWindow = levelWindowCanvas.GetComponentInChildren<LevelWindow>();
        levelWindow.Init();
        levelWindow.SetLevelSystem(levelSystem);

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

    public void Damage(float damage)
    {
        myCurrHealth -= damage;
        SetMyCurrHealth(myCurrHealth);
    }

    public void SetMyCurrHealth(float healthVal)
    {
        myCurrHealth = healthVal;
        MyHealthBar myHealthBar = myStatusBar.GetComponent<MyHealthBar>();
        myHealthBar.SetHealth(myCurrHealth / myMaxHealth, 1.0f);
    }

    public void RestoreToMaxHealth()
    {
        myCurrHealth = myMaxHealth;
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

    public void OnSceneTransitionStart()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");

        levelWindowCanvas.SetActive(false);

        DisableAllWeapons();
        if (SceneManager.GetActiveScene().name == "Outside")
        {
            levelWindowCanvas.SetActive(true);
            EnableAcquiredWeapons();
        }
    }

}
