using UnityEngine;
using System.Collections;

public class moveCar : MonoBehaviour {


    public const float initialSpeed = 10f;
    Vector3 currdir;
    public float speed;
    bool start = false;
    Rigidbody rigidbody;


    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        speed = initialSpeed;
        start = true;
    }
	
	// Update is called once per frame
	void Update () {
        playGame();

	}

    public void playGame()
    {
        if (start == true)
        {
            start = false;
            print("driving");
            currdir = transform.forward;
            rigidbody.velocity = currdir * initialSpeed;
            print(rigidbody.velocity); 
        }
    }
}
