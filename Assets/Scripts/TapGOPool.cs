using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Object pool for TapGameObjects
/// 
/// Holds a list of TapGameObjects that are preinstantiated
/// at startup for our game.
/// 
/// This includes inactive Objects
/// </summary>
/// <typeparam name="Template"></typeparam>
public class TapGOPool : MonoBehaviour
{
    public List<GameObject> objectList;
    void Awake()
    {
        objectList = new List<GameObject>();
    }

    /// <summary>
    /// Returns an inactive object from the pool
    /// Returns null if all objects are currently active
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        foreach (GameObject obj in objectList)
        {
            if (obj.activeSelf == false)
                return obj;
        }
        Debug.LogError("All objects in pool are active!");
        return null;
    }
}
