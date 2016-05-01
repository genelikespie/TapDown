using UnityEngine;
using System.Collections;

public class AllCars : MonoBehaviour {

    public GameObject[] Cars;

    // Use this for initialization

    void Awake()
    {
        Cars = GameObject.FindGameObjectsWithTag("Killzone");
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
