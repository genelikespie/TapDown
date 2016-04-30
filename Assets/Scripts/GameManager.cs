using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    TapGOPool enemyPool;
    TapGOPool tapAreaPool;

    TapPoint tapPoint;
    EnemySpawner enemySpawner;

    GameObject enemyPrefab;
    GameObject tapAreaPrefab;
    GameObject tapPointPrefab;
    GameObject enemySpawnerPrefab;

    const int enemyPoolAmount = 100;
    const int tapAreaPoolAmount = 100;

    public Base playerBase;

    private static GameManager instance;
    private static Object instance_lock = new Object();
    public static GameManager Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (GameManager)FindObjectOfType(typeof(GameManager));
            if (FindObjectsOfType(typeof(GameManager)).Length > 1)
            {
                Debug.LogError("There can only be one instance!");
                return instance;
            }
            if (instance != null)
                return instance;
            Debug.LogError("Could not find a instance!");
            return null;
        }
    }

	// Use this for initialization
	void Awake () {
        enemyPrefab = Resources.Load("enemyPrefab") as GameObject;
        tapAreaPrefab = Resources.Load("tapAreaPrefab") as GameObject;
        tapPointPrefab = Resources.Load("tapPointPrefab") as GameObject;
        enemySpawnerPrefab = Resources.Load("enemySpawnerPrefab") as GameObject;

        if (enemyPrefab == null || tapAreaPrefab == null || tapPointPrefab == null || enemySpawnerPrefab == null)
            Debug.LogError("prefab loading failed!");

        enemyPool = TapGOPoolSingleton<Enemy>.CreatePool(enemyPrefab, enemyPoolAmount);
        tapAreaPool = TapGOPoolSingleton<TapArea>.CreatePool(tapAreaPrefab, tapAreaPoolAmount);

        if (enemyPool == null || tapAreaPool == null || !playerBase)
            Debug.LogError("GameObjectPool creation failed!");
	}

    /// <summary>
    /// Starts the game, selected from the Start button from mainmenu
    /// </summary>
    public void BeginGame()
    {
        tapPoint = (Instantiate(tapPointPrefab) as GameObject).GetComponent<TapPoint>();
        if (!tapPoint)
            Debug.LogError("creation of tap point object failed!");

        enemySpawner = (Instantiate(enemySpawnerPrefab) as GameObject).GetComponent<EnemySpawner>();
        if (!enemySpawner) Debug.LogError("creation of enemy spawner failed!");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
