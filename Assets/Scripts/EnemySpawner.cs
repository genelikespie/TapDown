using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {


    ActiveTapGOPool activeEnemyPool;
    TapGOPool enemyPool;
    public List<Transform> spawnPoints;

    // placeholder for target transform, replace this with dynamic implementation
    public Transform target;
    const float timeBetweenSpawns = .5f;

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

        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.tag == "Spawner")
                spawnPoints.Add(t);
        }
        nexttime = timeBetweenSpawns;
    }
	void Start () {
	
	}
    void FixedUpdate()
    {
        if (currtime > nexttime)
        {
            nexttime = Time.time + timeBetweenSpawns;
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
        enemy.target = target;
        enemy.gameObject.SetActive(true);
    }
}
