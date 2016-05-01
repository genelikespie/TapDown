using UnityEngine;
using System.Collections;

public class TapGameObject : MonoBehaviour {

    // this object's active pool 
    // so we can remove from it when destroyed
    // and add to it when enabled
    private ActiveTapGOPool activePool;

    // Set if the object was handed out by the pool
    public bool taken = false;

    // MUST BE SET after setting activeTapOPool
    private bool initialized = false;

    public ActiveTapGOPool activeTapGOPool
    {
        get
        {
            return activePool;
        }
        set
        {
            activePool = value;
            // after setting the value, set the object as initialized
            initialized = true;
        }
    }

	// Use this for initialization

    protected void OnEnable ()
    {
        if (initialized)
        {
            taken = true;
            activePool.activeObjectList.Add(this);
        }
        // register this object with the activetapobjectpool
    }

    protected void OnDisable()
    {
        if (initialized)
        {
            if (!activePool.activeObjectList.Remove(this))
                Debug.LogError("Failed to remove object from active object list");
            // if active, 
            //   remove this object from activetapobjectpool
            // else
            //   throw error/exception
            // sets this object to be inactive
        }
        taken = false;
    }
}
