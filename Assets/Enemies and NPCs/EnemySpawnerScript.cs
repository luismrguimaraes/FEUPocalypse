using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public Transform enemy;
    public int count;
    public float rate;
}

public class EnemySpawnerScript : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    public Wave[] waves;
    private int currentWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    public float spawnCooldown = 1f;
    public float searchCountdown = 1f;

    public SpawnState spawnState = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnState == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if( !EnemyIsAlive())
            {
                Debug.Log(" Wave " + waves[currentWave] + "Completed");

                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0) //spawning 
        {
            if (spawnState != SpawnState.SPAWNING)
            {
                // Start Spawning
                StartCoroutine(SpawnWave(waves[currentWave]));
            }
            
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted() {

        waveCountdown = timeBetweenWaves;

        if (waves.Length -1 > currentWave) // if not last wave
        {
            spawnState = SpawnState.COUNTING;
            currentWave++;
        }
        else
        {
            Debug.Log("Game Completed!");
        }
    }


    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }


        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        spawnState = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);

            // Waits for a given time before spawning the next enemy
            yield return new WaitForSeconds(spawnCooldown); 
        }


        spawnState = SpawnState.WAITING;

        yield break;

    }

    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log(" Spawning Enemy: ");

        Instantiate(_enemy, transform.position, transform.rotation);
    }
}
