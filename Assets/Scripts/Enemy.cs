using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : TapGameObject {

    // distance above from where the enemy spawns
    const float distanceToDrop = 10f;
    // time it takes for enemy to drop to 0
    const float timeToDrop = 1f;

    Animator animator;
    Collider2D collider;
    Rigidbody2D rigidbody;

    const float initialSpeed = 5f;
    public float speed { get; private set; }
    // The initial target of our enemy, passed by the enemyspawner
    // This is only used as the initial velocity at spawn and 
    // should not be null when spawning is done.
    public Transform target;
    // If the enemy is in the game area (ready from spawning)
    // TODO check for this in TapArea
    public bool doneSpawning { get; private set; }

    public void SetDirection(Vector3 dir)
    {
        // TODO implement
        Debug.Log("new direction: " + dir);
    }

    void Awake () {
        doneSpawning = false;
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
    }
	// Use this for initialization
	void Start () {
	
	}

    /// <summary>
    /// Called as an AnimationEvent by the AnimationClip at
    /// the end of the Drop animation.
    /// This sets the Enemy as initialized and starts its movement
    /// </summary>
    public void DoneSpawning ()
    {
        if (target == null) Debug.LogError("target was not initialzed when enemy spawned!");
        rigidbody.velocity = (target.position - transform.position).normalized * speed;
        doneSpawning = true;
    }
	// Update is called once per frame
	void FixedUpdate () {
        // Drop the enemy from the sky!

	}
    protected void OnEnable()
    {
        base.OnEnable();
        speed = initialSpeed;
        animator.Play("Drop");
    }
    protected void OnDisable()
    {
        doneSpawning = false;
        base.OnDisable();
    }
}
