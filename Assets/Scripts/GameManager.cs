using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Create the game object pools
    TapGOPool enemyPool;
    TapGOPool tapAreaPool;
    TapGOPool catParticlePool;

    TapPoint tapPoint;
    public EnemySpawner enemySpawner;

    public GameObject enemyPrefab;
    public GameObject tapAreaPrefab;
    public GameObject tapPointPrefab;
    public GameObject enemySpawnerPrefab;
    public GameObject catParticlePrefab;

    public int score {get; private set;}
    MainMenu mainMenu;

    const int enemyPoolAmount = 160;
    const int tapAreaPoolAmount = 160;
    const int catParticlePoolAmount = 80;

    private int maxHealth = 3;
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
    void Awake()
    {
        carActivate = GameObject.Find("carholder");
        if (carActivate == null)
            Debug.LogError("Add carholder");
        if (enemyPrefab == null || tapAreaPrefab == null || tapPointPrefab == null || enemySpawnerPrefab == null || !catParticlePrefab)
            Debug.LogError("prefab loading failed!");
    }

    void Start()
    {
        enemyPool = TapGOPoolSingleton<Enemy>.CreatePool(enemyPrefab, enemyPoolAmount);
        tapAreaPool = TapGOPoolSingleton<TapArea>.CreatePool(tapAreaPrefab, tapAreaPoolAmount);
        catParticlePool = TapGOPoolSingleton<CatParticle>.CreatePool(catParticlePrefab, catParticlePoolAmount);
        if (enemyPool == null || tapAreaPool == null || !playerBase || !catParticlePool)
            Debug.LogError("GameObjectPool creation failed!");
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
        if (tapPoint == null)
        {
            Debug.LogError("creation of tap point object failed!");
        }
        if (enemySpawner == null)
        {
            enemySpawner = (Instantiate(enemySpawnerPrefab) as GameObject).GetComponent<EnemySpawner>();
            Debug.LogWarning("No enemy spawner found, creating one by default");
        }
        enemySpawner.gameObject.SetActive(true);

        carActivate.GetComponent<AllCars>().callingAllcars();
    }
    public void SetDifficulty(int diff)
    {
        maxHealth = diff;
    }

    public void StopGame()
    {
        foreach (TapGameObject o in enemyPool.objectList)
        {
            if (o.gameObject.activeSelf)
                o.gameObject.SetActive(false);
        }
        foreach (TapGameObject o in tapAreaPool.objectList)
        {
            if (o.gameObject.activeSelf)
                o.gameObject.SetActive(false);
        }
        if (tapPoint)
            GameObject.Destroy(tapPoint.gameObject);
        if (enemySpawner != null)
        {
            enemySpawner.gameObject.SetActive(false);
        }
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
        mainMenu.GameOver(score);
        StopGame();
    }
}
