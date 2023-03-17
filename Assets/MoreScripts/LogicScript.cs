using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static ShopItem;

public class LogicScript : MonoBehaviour
{
    enum ItemType { FlameBreath, CocktailMolotov };
    public enum Weapons { STANDARD_ATTACK, FLAME_BREATH, COCKTAIL_MOLOTOV };
    public bool[] mcAcquiredWeapons;

    private GameObject mainChar;
    private SceneManagerScript sceneManagerScript;

    public int coins = 0;
    private int totalCollectedCoins = 0;
    private int zombiesKills = 0;
    private int nightLordKills = 0;
    public float myMaxHealth = 500.0f;
    public float myCurrHealth;
    private LevelSystem levelSystem;
    public GameObject coinsWindow;
    public GameObject myStatusBar;
    [SerializeField] private GameObject levelWindowCanvas;
    private List<int> itemsBought = new List<int>();
    public AudioSource invalidPickSfx;
    public AudioSource purchaseSfx;
    public AudioSource fullHpRecoverSfx;
    public VectorValue playerPosition;
    public bool isGameCompleted = false;

    //private UnityEngine.UI.Button restartButton;
    //private UnityEngine.UI.Button quitButton;


    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        mcAcquiredWeapons = new bool[] { true, false , false};

        sceneManagerScript = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();

        SetMyCurrHealth(myMaxHealth);

        levelSystem = gameObject.AddComponent<LevelSystem>();
        levelSystem.Init();

        LevelWindow levelWindow = levelWindowCanvas.GetComponentInChildren<LevelWindow>();
        levelWindow.Init();
        levelWindow.SetLevelSystem(levelSystem);

        coinsWindow = GameObject.FindGameObjectWithTag("CoinsWindow");
        coinsWindow.GetComponent<CoinsWindow>().Init();

        DontDestroyOnLoad(gameObject);
    }

    public void incrementNightLordKill()
    {
        nightLordKills += 1;
    }
    public void incrementZombieKill()
    {
        zombiesKills += 1;
    }

    public bool CheckIfMCDead()
    {
        return myCurrHealth <= 0;
    }

    public void AddItemBought(int boughtItemType)
    {
        
        itemsBought.Add(boughtItemType);
    }

    public List<int> GetItemsBought()
    {
        return itemsBought;
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
                case (int)Weapons.COCKTAIL_MOLOTOV:
                    mainChar.GetComponent<MainCharBomb>().enabled = false;
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
                case (int)Weapons.COCKTAIL_MOLOTOV:
                    if (mcAcquiredWeapons[i])
                    {
                        mainChar.GetComponent<MainCharBomb>().enabled = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public bool BuyWeapon(int cost, int weaponBought)
    {
        if (coins < cost)
        {
            invalidPickSfx.Play();
            return false;
        }

        purchaseSfx.Play();
        SpendCoins(cost);

        for (int i = 0; i < System.Enum.GetNames(typeof(Weapons)).Length; i++)
        {
            switch (weaponBought + 1)
            {
                case (int)Weapons.FLAME_BREATH:
                    mcAcquiredWeapons[(int)Weapons.FLAME_BREATH] = true;
                    break;
                case (int)Weapons.COCKTAIL_MOLOTOV:
                    mcAcquiredWeapons[(int)Weapons.COCKTAIL_MOLOTOV] = true;
                    break;
                default:
                    break;
            }
        }

        return true;
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


        if (CheckIfMCDead())
        {
            GameOver();
        }

    }

    public void RestoreToMaxHealth()
    {
        if (myCurrHealth != myMaxHealth)
        {
            fullHpRecoverSfx.Play();
            SetMyCurrHealth(myMaxHealth);
        }
    }

    public void GainXP(int xp)
    {
        levelSystem.AddExperience(xp);
    }

    public void GainCoins(int value)
    {
        coins += value;
        totalCollectedCoins += value;
        coinsWindow.GetComponent<CoinsWindow>().SetCoinsValue(coins);
    }

    public void SpendCoins(int value)
    {
        coins -= value;
        coinsWindow.GetComponent<CoinsWindow>().SetCoinsValue(coins);
    }
    public int GetLevelNumber()
    {
        return levelSystem.GetLevelNumber();
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

    private void GameOver()
    {
        //restartButton = GameObject.FindGameObjectWithTag("GameOver").GetComponentsInChildren<UnityEngine.UI.Button>()[0];
        //restartButton.onClick.AddListener(RestartGame);

        //quitButton = GameObject.FindGameObjectWithTag("GameOver").GetComponentsInChildren<UnityEngine.UI.Button>()[1];
        //quitButton.onClick.AddListener(ReturnToMenu);

        GameObject gameOver = GameObject.FindGameObjectWithTag("GameOver");
        gameOver.GetComponent<Canvas>().enabled = true;

        gameOver.GetComponent<GameOverScript>().SetActive(true);

        gameOver.GetComponent<AudioSource>().Play();

        mainChar.GetComponent<MainCharMovementScript>().SetStopMoving(true);
        mainChar.GetComponent<Rigidbody2D>().simulated = false;

        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = 0;

        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreScript>().Show(zombiesKills, nightLordKills, totalCollectedCoins, levelSystem.GetComponent<LevelSystem>().GetTotalExperience());

        DisableAllWeapons();
    }

    public void GameCompleted()
    {
        if (!isGameCompleted)
        {
            isGameCompleted = true;
            //restartButton = GameObject.FindGameObjectWithTag("GameCompleted").GetComponentsInChildren<UnityEngine.UI.Button>()[0];
            //restartButton.onClick.AddListener(RestartGame);

            //quitButton = GameObject.FindGameObjectWithTag("GameCompleted").GetComponentsInChildren<UnityEngine.UI.Button>()[1];
            //quitButton.onClick.AddListener(ReturnToMenu);

            GameObject gameCompleted = GameObject.FindGameObjectWithTag("GameCompleted");
            gameCompleted.GetComponent<Canvas>().enabled = true;

            gameCompleted.GetComponent<GameCompleted>().SetActive(true);

            gameCompleted.GetComponent<AudioSource>().Play();

            mainChar.GetComponent<MainCharMovementScript>().SetStopMoving(true);
            mainChar.GetComponent<Rigidbody2D>().simulated = false;

            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = 0;

            GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreScript>().Show(zombiesKills, nightLordKills, totalCollectedCoins, levelSystem.GetComponent<LevelSystem>().GetTotalExperience());

            DisableAllWeapons();
        }
    }


    public void RestartGame()
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < gameObjects.Length; i++) {
           Destroy(gameObjects[i]);
        }
        SceneManager.LoadScene("DontDestroyOnLoad");

        playerPosition.initialValue = new Vector3(0, 0, 0);
    }

    public void ReturnToMenu()
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        SceneManager.LoadScene("MainMenu");
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
    }

}
