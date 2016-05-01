using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllCars : MonoBehaviour {

    public List<GameObject> Cars;

    // Use this for initialization

    void Awake()
    {
        Cars = new List<GameObject>();
        IEnumerable listOfChildren = transform.GetComponentsInChildren<Transform>();
        foreach (Transform i in listOfChildren)
        {
            if (i.tag == "Killzone")
            {
                Cars.Add(i.gameObject);
            }
        }
    }

    void Start () {
   

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void callingAllcars()
    {
        foreach(GameObject Car in Cars)
        {
            Car.GetComponent<moveCar>().playCar();
        }
    }
}
