﻿using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    public Wave[] waves;
    public Transform[] spawnPoints;
    private SpawnState state = SpawnState.COUNTING;
    private int nextWave = 0;
    private int waveLabel = 1;
    public float timeBetweenWaves = 5f;
    private float waveCountDown;
    // Search countDown essencial for searchs
    private float searchCountDown = 1f;

    // Getters
    public SpawnState State { get { return this.state; } }
    public float WaveCountDown { get { return this.waveCountDown; } }
    public int WaveLabel { get { return this.waveLabel; } }

    void Start() {
        if (spawnPoints.Length == 0) {
            Debug.Log("No spawn point");
        }
        waveCountDown = timeBetweenWaves;
    }

    void Update() {
        // Verify the round is clear, if not wait
        if (state == SpawnState.WAITING) {
            if (!isEnemyAlive()) {
                waveCompleted();
                return;
            } else {
                return;
            }
        }

        if (waveCountDown <= 0) {
            if (state != SpawnState.SPAWNING) {
                StartCoroutine(spawnWave(waves[0]));
            }
        } else {
            waveCountDown -= Time.deltaTime;
        }
    }

    // Spawn a wave of enemies in a given time
    IEnumerator spawnWave(Wave wave) {
        state = SpawnState.SPAWNING;
        int enemyNumber = nextWave + 2;
        for (int i = 0; i < enemyNumber; i++) {
            //Debug.Log("Spawning Enemy number" + i);
            spawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        state = SpawnState.WAITING;

        // if you have nothing to yield 
        yield break;
    }

    private void spawnEnemy(Transform enemy) {
        //Debug.Log("Spawning Enemy" + enemy.name);
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, sp.position, sp.rotation);
    }

    private void waveCompleted() {
        Debug.Log("Wave completed");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        //int finalWave = waves.Length - 1;
        bool increaseDifficult = (waveLabel + 1) / 5 == 1;
        Debug.Log("waveLabel: " +waveLabel / 5);
        if (increaseDifficult) {
            //nextWave = 0;
            Enemy.difficultModifier = 2;
            // Debug.Log("All Waves completed");
        }
        nextWave++;
        waveLabel++;

    }

    // Verify if there is any enemy alive
    private bool isEnemyAlive() {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0) {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }
        return true;
    }
}
