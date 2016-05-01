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
    public List<TapGameObject> objectList;
    static private Object _lock;
    void Awake()
    {
        _lock = new Object();
        objectList = new List<TapGameObject>();
    }

    /// <summary>
    /// Returns an inactive object from the pool
    /// Returns null if all objects are currently active
    /// </summary>
    /// <returns></returns>
    public TapGameObject GetObject()
    {
        lock (_lock)
        {
            foreach (TapGameObject obj in objectList)
            {
                if (obj.gameObject.activeSelf == false && !obj.taken)
                {
                    obj.taken = true;
                    return obj;
                }
            }
            Debug.LogError("All objects in pool are active!");
            return null;
        }
    }
}
