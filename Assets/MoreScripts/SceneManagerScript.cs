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

    public void SceneTransitionOnStartUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Outside")
        {
            enemySpawnerScript = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawnerScript>();

            if (currentWave >= 0)
                enemySpawnerScript.currentWave = currentWave;
        }

    }

    public void StoreCurrentWave()
    {
        currentWave = enemySpawnerScript.currentWave;
    }
}
