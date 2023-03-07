using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public Vector2 playerPositionAfterExiting;

    public string initialScene = "Outside";
    private LogicScript logicScript;
    private EnemySpawnerScript enemySpawnerScript;
    private int currentWave;
    private static GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();

        currentWave = -1;

        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(initialScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerPositionAfterExiting(Vector3 newPosition)
    {
        playerPositionAfterExiting = newPosition;
    }

    public Vector3 GetPlayerPositionAfterExiting()
    {
        return playerPositionAfterExiting;
    }

    public void OnSceneTransitionStart()
    {
        if (SceneManager.GetActiveScene().name == "Outside")
        {
            enemySpawnerScript = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawnerScript>();

            if (currentWave >= 0)
                enemySpawnerScript.currentWave = currentWave;

            ReenableEnemies();

            enemySpawnerScript.spawnState = EnemySpawnerScript.SpawnState.WAITING;
        }
    }

    public void StoreCurrentWave()
    {
        currentWave = enemySpawnerScript.currentWave;
    }

    public void DisableEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies != null)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyScript>().enabled = false;
                enemies[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
    }

    public void ReenableEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies != null)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyScript>().enabled = true;
                enemies[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
