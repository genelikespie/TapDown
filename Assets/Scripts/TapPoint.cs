﻿using UnityEngine;
using System.Collections;

/*
 * This is the object that keeps track of where the player taps 
 */
public class TapPoint : MonoBehaviour {

    // The height that tap areas are set to
    const float tapAreaHeight = 1.1f;

    // the pool that holds info on all active TapAreas
    ActiveTapGOPool activeTapAreaPool;
    TapGOPool tapAreaPool;
	// Use this for initialization
	void Awake () {
        activeTapAreaPool = TapGOPoolSingleton<TapArea>.ActivePoolInstance();
        tapAreaPool = TapGOPoolSingleton<TapArea>.PoolInstance();

        if (!activeTapAreaPool || !tapAreaPool)
            Debug.LogError("Cannot find activeTapAreaPool!");
	}
	
	// Update is called once per frame
	void Update () {
	    // TODO
        // Get user input (tap on screen)
        //   Check if we there is already a TapArea where the player tapped
        //     if yes, ignore
        //     else
        //       spawn a new taparea (from the object pool)
        //       register the taparea with activetapgameobjectpool
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray tapRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 tapPos = Vector3.zero;
            if (Physics.Raycast(tapRay, out hit))
            {
                Debug.Log(hit.transform.tag);
                // make sure the the ray hit the ground, if not, then the tap doesn't count
                if (hit.transform.tag != "Ground")
                    return;
                tapPos = hit.point;
                tapPos = new Vector3(tapPos.x, tapAreaHeight, tapPos.z);
                // check if our tap position is within the range of any currently active tap areas
                foreach (GameObject ta in activeTapAreaPool.activeObjectList)
                {
                    if ((ta.transform.position - tapPos).magnitude < TapArea.Radius)
                    {
                        return;
                    }
                }
            }
            else
                return;
            Debug.Log("fire");
            // we are not in range of any tap areas
            // set a fresh tap area and make it appear on the place
            TapArea freshTapArea = tapAreaPool.GetObject().GetComponent<TapArea>();
            // set our tap area as active
            freshTapArea.enabled = true;
            if (!freshTapArea)
            {
                Debug.LogError("freshTapArea was null!");
                return;
            }
            freshTapArea.transform.position = tapPos;
            freshTapArea.gameObject.SetActive(true);

        }
	}
}