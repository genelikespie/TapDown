using UnityEngine;
using System.Collections;

public class TapGameObject : MonoBehaviour {

    // this object's active pool 
    // so we can remove from it when destroyed
    // and add to it when enabled
    private ActiveTapGOPool activePool;
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

    // MUST BE SET after setting activeTapOPool
    private bool initialized = false;
	// Use this for initialization

    protected void OnEnable ()
    {
        if (initialized)
        {
            activePool.activeObjectList.Add(this.gameObject);
        }
        // register this object with the activetapobjectpool
    }

    protected void OnDisable()
    {
        if (initialized)
        {
            if (!activePool.activeObjectList.Remove(this.gameObject))
                Debug.LogError("Failed to remove object from active object list");
            // if active, 
            //   remove this object from activetapobjectpool
            // else
            //   throw error/exception
            // sets this object to be inactive
        }
    }
}
