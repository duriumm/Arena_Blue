using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public TextMeshProUGUI enemyWavesText;
    public TextMeshProUGUI enemiesLeftText;
    public TextMeshProUGUI nextWaveCounterText;
    public int nextWaveCounter;
    private bool isNextWaveCounterActive;
    private bool canWeEnterCounterCoroutine;

    public EnemySpawner enemySpawner;


    public int currentWave;


    public GameObject enemiesContainer;
    public AnimationCurve enemiesPerWaveCurve;

    public int enemiesOnScreen;
    public int currentWavesTotalAmountOfEnemies;
    public int totalAmountOfWaves = 20;
    public int totalAmountOfEnemies = 100;

    public AudioClip newWaveSound;
    private AudioManager audioManager;


    void Start()
    {
        isNextWaveCounterActive = false;
        nextWaveCounterText.enabled = false;
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        Keyframe[] keyframes = enemiesPerWaveCurve.keys;
        keyframes[1].time = totalAmountOfWaves;
        keyframes[1].value = totalAmountOfEnemies;
        enemiesPerWaveCurve.keys = keyframes;

        currentWave = 0; // Start is wave 0, the rest is calculated
        currentWavesTotalAmountOfEnemies = (int)enemiesPerWaveCurve.Evaluate(currentWave);
        StartNewWave();

        enemiesOnScreen = CheckCurrentAmountOfEnemies();
        enemyWavesText.text = $"Wave {currentWave} / {totalAmountOfWaves}";
        enemiesLeftText.text = $"Enemies\n{enemiesOnScreen}/{currentWavesTotalAmountOfEnemies}";
    }

    private void Update()
    {
        if(isNextWaveCounterActive == true && canWeEnterCounterCoroutine == true)
        {
            print("entering coroutine start");
            StartCoroutine(CountDownToNextWave());
        }
    }
    public int CalculateNextWavesAmountOfEnemies()
    {
        int amountOfEnemiesForNextWave = (int)enemiesPerWaveCurve.Evaluate(currentWave);
        return amountOfEnemiesForNextWave;
    }

    public void StartNewWave()
    {
        enemyWavesText.enabled = true;
        nextWaveCounterText.enabled = false;
        audioManager.PlaySoundEffect(newWaveSound, 0.2f, audioManager.soundEffectsSource);
        currentWave += 1; // We add 1 here so no need to in function below
        int enemiesToSpawn = CalculateNextWavesAmountOfEnemies();
       // print($"Enemies for new wave is: {enemiesToSpawn}");
        enemiesOnScreen = enemiesToSpawn;
        currentWavesTotalAmountOfEnemies = enemiesToSpawn;
        // Wave text
        enemyWavesText.text = $"Wave {currentWave} / {totalAmountOfWaves}";
        // Enemies text
        enemiesLeftText.text = $"Enemies\n{enemiesOnScreen}/{currentWavesTotalAmountOfEnemies}";
        enemySpawner.StartEnemySpawning(enemiesToSpawn);
    }

    public void RemoveEnemyFromCounter()
    {
        enemiesOnScreen -= 1;
        enemiesLeftText.text = $"Enemies\n{enemiesOnScreen}/{currentWavesTotalAmountOfEnemies}";
        if(enemiesOnScreen <= 0)
        {
            print("You passed now we wait 30 sec before next wave");
            //enemySpawner.StopEnemySpawning();
            StartNextWaveCounter();
            
        }
    }
    public void AddEnemyToCounter()
    {
        
        enemiesLeftText.text = $"Enemies\n{enemiesOnScreen}/{currentWavesTotalAmountOfEnemies}";
    }

    public void SetAmountOfEnemiesOnNewWave()
    {
        enemiesLeftText.text = $"Enemies\n{enemiesOnScreen}/{currentWavesTotalAmountOfEnemies}";
    }

    public int CheckCurrentAmountOfEnemies()
    {
        print("We have: " + enemiesContainer.transform.childCount + " enemies in scene!");
        return enemiesContainer.transform.childCount;
    }

    public void StartNextWaveCounter()
    {
        print("in startnextwavecounter");

        nextWaveCounter = 30;
        enemyWavesText.enabled = false;
        nextWaveCounterText.enabled = true;
        nextWaveCounterText.text = $"Next wave in\n{nextWaveCounter}";
        isNextWaveCounterActive = true;
        canWeEnterCounterCoroutine = true;

    }
    private IEnumerator CountDownToNextWave()
    {
        canWeEnterCounterCoroutine = false;
        print("Starting next wave counter");
        nextWaveCounter -= 1;
        yield return new WaitForSeconds(1f);
        if(nextWaveCounter <= 0)
        {
            print("ending coroutine counter");
            StartNewWave();
            yield break;
        }
        nextWaveCounterText.text = $"Next wave in\n{nextWaveCounter}";
        canWeEnterCounterCoroutine = true;

    }
}
