﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles the automated spawning of enemies.
/// </summary>
public class EnemySpawner : MonoBehaviour {
    ActiveTapGOPool activeEnemyPool;
    TapGOPool enemyPool;

    [SerializeField]
    private List<Transform> spawnPoints;

    // placeholder for target transform, replace this with dynamic implementation
    public Transform target;
    public float timeBetweenSpawns = 2.5f;

    float nexttime = 0;
    float currtime = 0;
	// Use this for initialization
    void Awake()
    {
        target = GameManager.Instance().playerBase.transform;
        spawnPoints = new List<Transform>();
        activeEnemyPool = TapGOPoolSingleton<Enemy>.ActivePoolInstance();
        enemyPool = TapGOPoolSingleton<Enemy>.PoolInstance();
        if (!activeEnemyPool || !enemyPool || !target)
            Debug.LogError("Cannot find activeEnemyPool!");

        // Build the spawner list from child objects with the "spawner" tag
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.tag == "Spawner")
                spawnPoints.Add(t);
        }
        currtime = 0f;
        nexttime = timeBetweenSpawns;
    }
	void Start () {
	
	}
    void FixedUpdate()
    {
        if (currtime >= nexttime)
        {
            nexttime = currtime + timeBetweenSpawns;
            SpawnEnemy();
        }
        currtime += Time.deltaTime;
    }
    public void SpawnEnemy()
    {
        int i = (int)(Random.value * (spawnPoints.Count - 1));
        Enemy enemy = enemyPool.GetObject().GetComponent<Enemy>();
        if (!enemy) Debug.LogError("enemy not found");
        enemy.transform.position = spawnPoints[i].position;
        enemy.transform.rotation = spawnPoints[i].rotation;
        enemy.SetTarget(target);
        enemy.gameObject.SetActive(true);
    }
}
