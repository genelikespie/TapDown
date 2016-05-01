using UnityEngine;
using System.Collections;

public class moveCar : MonoBehaviour {


    public const float initialSpeed = 15f;
    Vector3 currdir;
    public float speed;
    bool start = false;
    Rigidbody rigidbody;


    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        speed = initialSpeed;
    }
	
	// Update is called once per frame
	void Update () {


	}

    public void playCar()
    {
            this.gameObject.SetActive(true);
            currdir = transform.forward;
            rigidbody.velocity = currdir * initialSpeed;
    }

    public void stopCar()
    {
        this.gameObject.SetActive(false);
        rigidbody.velocity = Vector3.zero;

    }

    public void movebackCar(Vector3 MoveHere)
    {
        this.transform.position = MoveHere;
    }
}
