using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject Lint, Screeb, BossLint, BossScreeb;
    public Transform playerPos;
    public Transform[] Spawners = new Transform[4];


    public int totalSpawn = 0;      //total number of enemies spawned
    public int totalWaves = 0;
    public float waveRate;      //how often is new wave
    public float spawnRate;     //how often is new enemy spawned
    public int spawnFreq;       //how many spawn per wave
    public float spawnDistanceX = 5f, spawnDistanceY = 5f;

    public bool canSpawn;

    Vector3 min = new Vector3(-100f, -100f, 0f);
    Vector3 max = new Vector3(100f, 100f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            Spawn();
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnRate);
        canSpawn = true;
    }

    IEnumerator WaveDelay()
    {
        yield return new WaitForSeconds(waveRate);
        canSpawn = true;
        totalSpawn = 0;
        totalWaves++;
    }

    public void Spawn()
    {
        totalSpawn++;
        int num = (int)Random.Range(0f, 4f);

        //pick spawn location
        if (num == 0 || num == 1)
        {
            spawnDistanceX = 25;
            spawnDistanceY = 5;
        }
        else if (num == 2 || num == 3)
        {
            spawnDistanceX = 5f;
            spawnDistanceY = 15f;
        }

        //Create Spawn Position using Random Numbers polish this for later
        Vector3 spawnPos = new Vector3(Spawners[num].position.x + Random.Range(-spawnDistanceX, spawnDistanceX), Spawners[num].position.y + Random.Range(-spawnDistanceY, spawnDistanceY), 0f);

        GameObject enemy = ChooseEnemy();

        //create enemy and reset timer
        Instantiate(enemy, spawnPos, Quaternion.identity);      //use an overload with a Vec3 because transform creates a parent object and messes with future spawns and movements
        canSpawn = false;

        //if enemies spawned in wave < wave total keep spawning enemies
        if (totalSpawn >= spawnFreq)
        {
            StartCoroutine(WaveDelay());
        }
        else
        {
            StartCoroutine(SpawnDelay());
        }


    }

    public GameObject ChooseEnemy()
    {

        if (totalSpawn % 9 == 0)
            return Lint;
        else if (totalSpawn == 5 && totalWaves % 5 == 3)
            return BossScreeb;
        else if (totalSpawn == 5 && totalWaves % 5 == 0)
            return BossLint;
        else
            return Screeb;

    }

}
