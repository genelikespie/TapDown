using UnityEngine;
using System.Collections;

public class RedColor : MonoBehaviour {
    Color tappedColor;
    Color changeThisColor;
    float startTime;
    bool changeTime = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        startTime += Time.deltaTime;

        if (startTime >= 1f && changeTime == true)
        {
            changeTime = false;
            transform.GetComponent<Renderer>().material.color = tappedColor;
        }

    }

    public void SwitchColor() {
        startTime = 0;
        changeTime = true;
        tappedColor = changeThisColor;
        transform.GetComponent<Renderer>().material.color = Color.red;
    }

    void Awake() {
        changeThisColor = transform.GetComponent<Renderer>().material.color;
    }




}
