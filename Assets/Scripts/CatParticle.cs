using UnityEngine;
using System.Collections;

public class CatParticle : TapGameObject {

    ParticleSystem particleSystem;
	// Use this for initialization
	void Awake () {
        particleSystem = GetComponent<ParticleSystem>();
        if (!particleSystem)
            Debug.LogError("no particle system");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (particleSystem && !particleSystem.IsAlive())
        {
            gameObject.SetActive(false);
        }
	}

    public void PlayAtLocation(Vector3 location)
    {
        transform.position = location;
        particleSystem.Play();
    }
    
}
