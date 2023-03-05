using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LogicScript : MonoBehaviour
{
    public float experience = 0f;
    public GameObject mainChar;

    public enum Weapons { STANDARD_ATTACK, FLAME_BREATH }
    public bool[] mcAcquiredWeapons;

    private SceneManagerScript sceneManagerScript;

    //private int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        mcAcquiredWeapons = new bool[] { true, false };

        sceneManagerScript = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainXP (float xp)
    {
        experience += xp;
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
