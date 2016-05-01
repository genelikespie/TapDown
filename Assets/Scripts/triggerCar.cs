using UnityEngine;
using System.Collections;

public class triggerCar : MonoBehaviour {

    Transform carTransform;
    Vector3 moveVectorCar;
    public bool moveZ = true;
    public float moveZAmt = -300f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Killzone")
        {
            //move the car in z direction
            if (moveZ == true) {
                carTransform = other.gameObject.transform;
                moveVectorCar = new Vector3(carTransform.position.x, carTransform.position.y, carTransform.position.z + moveZAmt);
                other.GetComponent<moveCar>().movebackCar(moveVectorCar);
                    }
        }
    }


}
