using UnityEngine;
using System.Collections;

public class BushParticleTrigger : MonoBehaviour {

    Collider trigger;
    ParticleSystem particleSystem;
	// Use this for initialization
	void Awake () {
        trigger = GetComponent<BoxCollider>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        if (!trigger || !particleSystem)
            Debug.LogError("trigger or particles not found!");
	}
	
	void OnTriggerEnter(Collider other) {
        if (other.tag == "Killzone")
        {
            particleSystem.Play();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Killzone")
        {
            particleSystem.Stop();
        }
    }

}
