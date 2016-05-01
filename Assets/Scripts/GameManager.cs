using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    TapGOPool enemyPool;
    TapGOPool tapAreaPool;
    public TapGOPool catParticlePool;

    TapPoint tapPoint;
    EnemySpawner enemySpawner;
    GameObject enemyPrefab;
    GameObject tapAreaPrefab;
    GameObject tapPointPrefab;
    GameObject enemySpawnerPrefab;
    GameObject catParticlePrefab;

    public int score {get; private set;}
    MainMenu mainMenu;

    const int enemyPoolAmount = 160;
    const int tapAreaPoolAmount = 160;
    const int catParticlePoolAmount = 80;

    const int maxHealth = 3;
    int health;
    public static bool godMode = false;

    public Base playerBase;
    public GameObject carActivate;

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
        catParticlePrefab = Resources.Load("catParticlePrefab") as GameObject;

        if (enemyPrefab == null || tapAreaPrefab == null || tapPointPrefab == null || enemySpawnerPrefab == null || !catParticlePrefab)
            Debug.LogError("prefab loading failed!");

        enemyPool = TapGOPoolSingleton<Enemy>.CreatePool(enemyPrefab, enemyPoolAmount);
        tapAreaPool = TapGOPoolSingleton<TapArea>.CreatePool(tapAreaPrefab, tapAreaPoolAmount);
        catParticlePool = TapGOPoolSingleton<CatParticle>.CreatePool(catParticlePrefab, catParticlePoolAmount);
        if (enemyPool == null || tapAreaPool == null || !playerBase || !catParticlePool)
            Debug.LogError("GameObjectPool creation failed!");

        if (GameObject.Find("carholder") == null)
            Debug.LogError("Add carholder");

        carActivate = GameObject.Find("carholder");
    }

    /// <summary>
    /// Starts the game, selected from the Start button from mainmenu
    /// </summary>
    public void BeginGame(MainMenu m)
    {
        mainMenu = m;
        score = 0;
        health = maxHealth;
        mainMenu.SetHealthMeter(health);
        tapPoint = (Instantiate(tapPointPrefab) as GameObject).GetComponent<TapPoint>();
        if (!tapPoint)
            Debug.LogError("creation of tap point object failed!");

        enemySpawner = (Instantiate(enemySpawnerPrefab) as GameObject).GetComponent<EnemySpawner>();
        if (!enemySpawner) Debug.LogError("creation of enemy spawner failed!");

        carActivate.GetComponent<AllCars>().callingAllcars();
    }

    public void StopGame()
    {
        foreach (GameObject o in enemyPool.objectList) {
            if (o.activeSelf)
                o.SetActive(false);
        }
        foreach (GameObject o in tapAreaPool.objectList)
        {
            if(o.activeSelf)
                o.SetActive(false);
        }
        if (tapPoint)
            GameObject.Destroy(tapPoint.gameObject);
        if(enemySpawner)
            GameObject.Destroy(enemySpawner.gameObject);

    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (enemySpawner)
                enemySpawner.SpawnEnemy();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = !godMode;
        }
	}

    public void DecHealth(int amount)
    {
        if (godMode)
            return;
        health -= amount;
        if (health <= 0)
        {
            GameOver();
        }
        mainMenu.DecHealthMeter(health,true);
    }

    public void IncScore(int s)
    {
        score += s;
        mainMenu.UpdateScore(score);
    }
    void GameOver()
    {
        StopGame();
        mainMenu.GameOver();
    }
}
