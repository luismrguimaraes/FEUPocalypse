using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public Transform enemy;
    public int count;
    public int numOfBosses;
    public float rate; // how many enemies spawning per second, example: rate = 10, every 0.1 a enemy is spawned
}

public class EnemySpawnerScript : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    public Wave[] waves;
    public int currentWave = 0;

    public float timeBetweenWaves = 2.5f;
    public float waveCountdown;
    public float searchCountdown = 3f;

    public SpawnState spawnState = SpawnState.COUNTING;

    public bool defaultSpawningPoint = false;
    public Transform[] spawningPoints;

    public AudioSource zombieSfx;

    // Start is called before the first frame update
    void Start()
    {
        if (spawningPoints.Length == 0)
        {
            Debug.Log("No Spawning Points were introduced, origin will be used!");
            defaultSpawningPoint = true;
        }

        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnState == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if( !EnemyIsAlive() )
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

        if (currentWave < waves.Length -1) // if not last wave
        {
            spawnState = SpawnState.COUNTING;
            currentWave++;
        }
        else
        {
            GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>().GameCompleted();

            Debug.Log("Game Completed!" + currentWave);
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
        zombieSfx.Play();

        for (int i = 0; i < _wave.count; i++)
        {
            if (i + _wave.numOfBosses >= _wave.count)
            {
                SpawnEnemy(_wave.enemy, true);
            }
            else
            {
                SpawnEnemy(_wave.enemy, false);
            }

            // Waits for a given time before spawning the next enemy
            yield return new WaitForSeconds(1 / waves[currentWave].rate); 
        }


        spawnState = SpawnState.WAITING;

        yield break;

    }

    void SpawnEnemy (Transform _enemy, bool isBoss)
    {

        Transform _sp;
        if (!defaultSpawningPoint)
        {
            _sp = spawningPoints[Random.Range(0, spawningPoints.Length)];
        }
        else
        {
            _sp = transform;
        }

        Transform enemySpawned = Instantiate(_enemy, _sp.position, _sp.rotation);

        if (isBoss)
        {
            enemySpawned.GetComponent<EnemyScript>().InitNightLord();
        }
        else
        {
            enemySpawned.GetComponent<EnemyScript>().InitZombie();
        }

    }
}
