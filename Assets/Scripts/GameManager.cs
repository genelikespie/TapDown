using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    TapGOPool enemyPool;
    TapGOPool tapAreaPool;

    TapPoint tapPoint;

    GameObject enemyPrefab;
    GameObject tapAreaPrefab;
    GameObject tapPointPrefab;

    const int enemyPoolAmount = 100;
    const int tapAreaPoolAmount = 100;

	// Use this for initialization
	void Awake () {
        enemyPrefab = Resources.Load("enemyPrefab") as GameObject;
        tapAreaPrefab = Resources.Load("tapAreaPrefab") as GameObject;
        tapPointPrefab = Resources.Load("tapPointPrefab") as GameObject;

        if (enemyPrefab == null || tapAreaPrefab == null || tapPointPrefab == null)
            Debug.LogError("prefab loading failed!");

        enemyPool = TapGOPoolSingleton<Enemy>.CreatePool(enemyPrefab, enemyPoolAmount);
        tapAreaPool = TapGOPoolSingleton<TapArea>.CreatePool(tapAreaPrefab, tapAreaPoolAmount);

        if (enemyPool == null || tapAreaPool == null)
            Debug.LogError("GameObjectPool creation failed!");
	}

    void Start()
    {
        tapPoint = (Instantiate(tapPointPrefab) as GameObject).GetComponent<TapPoint>();
        if (!tapPoint)
            Debug.LogError("creation of tap point object failed!");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
