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
    void Awake()
    {
        objectList = new List<TapGameObject>();
    }

    /// <summary>
    /// Returns an inactive object from the pool.
    /// The caller is responsible for reactivating the game object.
    /// Returns null if all objects are currently active
    /// </summary>
    /// <returns></returns>
    public TapGameObject GetObject()
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
