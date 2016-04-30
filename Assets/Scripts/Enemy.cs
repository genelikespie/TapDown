using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class Enemy : TapGameObject {

    // distance above from where the enemy spawns
    const float distanceToDrop = 10f;
    // time it takes for enemy to drop to 0
    const float timeToDrop = 1f;
    // the approximation of the size of the enemy
    public const float radius = 2.5f;

    Animator animator;
    Collider collider;
    Rigidbody rigidbody;

    const float initialSpeed = 5f;
    public float speed { get; private set; }
    // The initial target of our enemy, passed by the enemyspawner
    // This is only used as the initial velocity at spawn and 
    // should not be null when spawning is done.
    public Transform target;
    // If the enemy is in the game area (ready from spawning)
    // TODO check for this in TapArea
    public bool doneSpawning { get; private set; }


    bool rotateEnemy = false;
    const float angularSpeed = 1f;
    Vector3 newdir;
    Vector3 olddir;
    float step;                     // current angle we've rotated to

    public void SetDirection(Vector3 dir)
    {
        newdir = dir;
        olddir = transform.forward;
        rigidbody.velocity = Vector3.zero;
        step = 0;
        rotateEnemy = true;
        Debug.Log("new difference: " + dir);
    }

    void Awake () {
        doneSpawning = false;
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        transform.localScale = new Vector3(radius*2, radius*2, radius*2);
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
        //Debug.Log("done spawning");
        if (target == null) Debug.LogError("target was not initialzed when enemy spawned!");
        Vector3 diff = target.position - transform.position;
        diff = new Vector3(diff.x, 0, diff.z);                  // ignore the y axis
        SetDirection((diff).normalized );
        doneSpawning = true;
    }
	// Update is called once per frame
	void FixedUpdate () {
        // Drop the enemy from the sky!
        if (rotateEnemy)
        {
            step += (angularSpeed * Time.deltaTime);
            Vector3 currdir = Vector3.RotateTowards(olddir, newdir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(currdir);
            //Debug.Log(currdir + "  forward: " + transform.forward);
            if (transform.forward == newdir)
            {
                rigidbody.velocity = newdir * speed;
                rotateEnemy = false;
            }
        }

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
