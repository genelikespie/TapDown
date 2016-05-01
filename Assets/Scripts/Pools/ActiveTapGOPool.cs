using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This object stores a list of all active TapGameObjects
/// When a TapGM gets destroyed/inactive it removes itself from this list
/// 
/// </summary>
public class ActiveTapGOPool : MonoBehaviour {

    public List<TapGameObject> activeObjectList;

    void Awake()
    {
        activeObjectList = new List<TapGameObject>();
    }
}
