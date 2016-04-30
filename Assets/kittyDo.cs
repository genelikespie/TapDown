using UnityEngine;
using System.Collections;

public class kittyDo : MonoBehaviour {

    Animator kittyAnimator;

	// Use this for initialization
	void Start () {
       kittyAnimator = this.GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetKey("up"))
        {
            DoWalk();
        }

        if (Input.GetKeyDown("down"))
        {
            DoRun();
        }
    }

    public void DoWalk()
    {
        kittyAnimator.SetInteger("Status", 1);
    }

    public void DoRun()
    {
        kittyAnimator.SetInteger("Status", 2);
    }

    public void DoIdle()
    {
        kittyAnimator.SetInteger("Status", 0);
    }
}
