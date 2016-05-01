using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Singleton object pool for TapGameObjects
/// Holds a list of TapGameObjects that are preinstantiated
/// at startup for our game
/// </summary>
/// <typeparam name="Template"></typeparam>
public class TapGOPoolSingleton<Template> : MonoBehaviour where Template : TapGameObject {

	private static TapGOPool poolInstance;
    private static ActiveTapGOPool activePoolInstance;

    private static object _instlock = new object();
    private static object _createlock = new object();

    public static TapGOPool PoolInstance()
    {
        if (poolInstance != null)
            return poolInstance;
        else
        {
            lock (_instlock)
            {
                if (poolInstance == null)
                {
                        Debug.Log("TapGOPool not found! One must be created through CreatePool()");
                }
                return poolInstance;
            }
        }
    }
    public static ActiveTapGOPool ActivePoolInstance()
    {
        if (activePoolInstance != null)
            return activePoolInstance;
        else
        {
            // TODO make this thread safe!
            Debug.LogError("ActiveTapGOPool not found! One must be created through CreatePool()");
            return null;
        }
    }
    public static TapGOPool CreatePool(GameObject templatePrefab, int amount)
    {
        if (amount <= 0) {
            Debug.LogError("Cannot create an empty pool!");
            return null;
        }
        if (templatePrefab.GetComponent<Template>() == null) {
            Debug.LogError("Object prefab not of template type!");
            return null;
        }
        if (poolInstance != null) {
            Debug.LogError("GameObjectPool already created!");
            return null;
        }
        lock (_createlock)
        {
            if (poolInstance == null && activePoolInstance == null) {
                // create a new instance of TapGOPool for our singleton
                GameObject newinstance = new GameObject("TapGOPool");
                newinstance.AddComponent<TapGOPool>();
                poolInstance = newinstance.GetComponent<TapGOPool>();

                // create a new instance of ActiveTapGOPool
                GameObject newactiveinstance = new GameObject("activePool");
                newactiveinstance.AddComponent<ActiveTapGOPool>();
                activePoolInstance = newactiveinstance.GetComponent<ActiveTapGOPool>();


                if (poolInstance == null || activePoolInstance == null)
                    Debug.LogError("Could not create poolInstance!");
            }
            else
            {
                return poolInstance;
            }
        }

        // add our objects to the pool
        for (int i = 0; i < amount; i++)
        {
            TapGameObject prefab = (Instantiate(templatePrefab) as GameObject).GetComponent<TapGameObject>();
            if (!prefab)
                Debug.LogError("Not a TapGameObject!");
            prefab.gameObject.SetActive(false);                                             // initially set object to be inactive
            prefab.transform.SetParent(poolInstance.transform);                             // set the created prefab to be a child of the pool object for cleanliness
            // initialize the activepool for the object 
            // (we do this after setting it to active to make sure we don't call the OneEnable and OnDisable functions)
            prefab.activeTapGOPool = activePoolInstance;      
            poolInstance.objectList.Add(prefab);
        }
        return poolInstance;
    }
}
