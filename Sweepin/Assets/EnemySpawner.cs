using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPandaPrefab;
    public GameObject enemyParentGameObject;
    

    public GameObject enemyToSpawn;

    public List<GameObject> spawnAbleEnemies = new List<GameObject>() { };
    private bool isEnemyReadyToSpawn = true;
    private bool isSpawningPaused = true;

    private WavesManager wavesManager;

    public int totalAmountOfEnemiesToSpawn;
    public int currentAmountOfEnemiesSpawned;

    
    void Start()
    {
        enemyParentGameObject = GameObject.Find("Enemies").gameObject;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        wavesManager = GameObject.FindWithTag("WavesManager").GetComponent<WavesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawningPaused && isEnemyReadyToSpawn)
        {
            StartCoroutine(SpawnEnemy(1f));
        }
    }



    private IEnumerator SpawnEnemy(float secondsBetweenSpawns = 1f)
    {
        //print($"Enemies to spawn is: {totalAmountOfEnemiesToSpawn}");
        //print($"Current amount of enemies is: {currentAmountOfEnemiesSpawned}");
        currentAmountOfEnemiesSpawned += 1;
        if(currentAmountOfEnemiesSpawned >= totalAmountOfEnemiesToSpawn)
        {
            print("-----------stopping respawn");
            StopEnemySpawning();
        }

        isEnemyReadyToSpawn = false;
        Vector2 rndPosWithin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
        var spawedEnemy = Instantiate(enemyToSpawn, rndPosWithin, transform.rotation);
        spawedEnemy.name = enemyToSpawn.name;
        spawedEnemy.transform.parent = enemyParentGameObject.transform;


        yield return new WaitForSeconds(secondsBetweenSpawns);
        isEnemyReadyToSpawn = true;

    }

    public void StartEnemySpawning(int amountOfEnemiesToSpawn)
    {
        //print("starting spawn");
        isSpawningPaused = false;
        currentAmountOfEnemiesSpawned = 0;
        totalAmountOfEnemiesToSpawn = amountOfEnemiesToSpawn;
        StartCoroutine(SpawnEnemy(1f));
    }
    public void StopEnemySpawning()
    {
        StopCoroutine(SpawnEnemy(0f));
        isEnemyReadyToSpawn = false;
        isSpawningPaused = true;
    }

}
