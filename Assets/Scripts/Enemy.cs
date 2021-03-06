﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : TapGameObject {

    // distance above from where the enemy spawns
    const float distanceToDrop = 10f;
    // time it takes for enemy to drop to 0
    const float timeToDrop = 1f;
    // the approximation of the size of the enemy
    public const float radius = 2.5f;

    AudioSource popSound;
    Animator animator;
    AnimEnemy animEnemy;
    Collider collider;
    Rigidbody rigidbody;
    GameManager gameManager;
    TapGOPool catParticlePool;

    const float initialSpeed = 5f;
    protected const float maxSpeed = 15f;

    public float speed { get; private set; }
    // The initial target of our enemy, passed by the enemyspawner
    // This is only used as the initial velocity at spawn and 
    // should not be null when spawning is done.
    public void SetTarget (Transform targ) {
        animEnemy.target = targ;
    }

    // If the enemy is in the game area (ready from spawning)
    // TODO check for this in TapArea
    public bool doneSpawning { get; set; }

    bool rotateEnemy = false;
    float angularSpeed = 1f;
    float jumpSpeed = 1f;
    Vector3 newdir;
    Vector3 olddir;
    float step;                     // current angle we've rotated to

    public void SetDirection(Vector3 dir)
    {
        if (rotateEnemy == false)
        {
            speed = speed * 1.25f;
            if (speed >= maxSpeed)
                speed = maxSpeed;

            newdir = new Vector3(dir.x, 0, dir.z);
            olddir = transform.forward;
            rigidbody.velocity = Vector3.zero;
            step = 0;
            rotateEnemy = true;

            //KittyDo kitty = GetComponent<KittyDo>();
            angularSpeed = (Vector3.AngleBetween(newdir, olddir) / jumpSpeed);
            animEnemy.Jump();
        }
    }

    protected void Awake ()
    {
        AudioManager audioManager = AudioManager.Instance();
        popSound = audioManager.GetAudioSource("PopSound");
        gameManager = GameManager.Instance();
        doneSpawning = false;
        //animator = GetComponentIn<Animator>();
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        if (!gameManager)
        {
            Debug.LogError("no gameManager found!");
        }
        rigidbody.useGravity = false;
        transform.localScale = new Vector3(radius*2, radius*2, radius*2);
        animEnemy = GetComponentInChildren<AnimEnemy>();
        if (!animEnemy)
        {
            Debug.LogError("anim enemy not found!");
        }
    }

	// Update is called once per frame
	protected void FixedUpdate () {
        // Drop the enemy from the sky!
        if (rotateEnemy)
        {
            step += (angularSpeed * Time.deltaTime);
            Vector3 currdir = Vector3.RotateTowards(olddir, newdir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(currdir);
            //Debug.Log(currdir + "  forward: " + transform.forward);
            if ((transform.forward - newdir).magnitude < 0.01f)
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
        //animator.Play("drop");
    }
    protected void OnDisable()
    {
        if (doneSpawning)
        {
            rigidbody.velocity = Vector3.zero;
            if (catParticlePool == null)
            {
                catParticlePool = TapGOPoolSingleton<CatParticle>.PoolInstance();
            }
            TapGameObject particle = catParticlePool.GetObject();
            if (particle)
            {
                particle.gameObject.SetActive(true);
                particle.GetComponent<CatParticle>().PlayAtLocation(transform.position);
            }
            else
                Debug.LogError("Could not play particle system");
            gameManager.IncScore(1);
            popSound.Play();
        }
        doneSpawning = false;
        base.OnDisable();
    }

    // Called by the OnTriggerEnter in EnemyCollider to handle collision behavior.
    // Separate this from OnTriggerEnter so that we can put the 
    // collider anywhere in the hierarchy.
    public void CollideWithOther(Collider other)
    {
        if (other.gameObject.tag == "Base")
        {
            gameManager.DecHealth(1);
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Wall")
        {
            if (!rotateEnemy)
            {
                Vector3 vVelocity = -(transform.forward);
                Vector3 fVelocity = (other.transform.forward);
                Vector3 endDirection = vVelocity - 2 * (Vector3.Dot(vVelocity, fVelocity)) * fVelocity;
                endDirection = new Vector3(endDirection.x, 0, endDirection.z);
                endDirection = endDirection.normalized;
                this.SetDirection(endDirection);
            }
        }

        if (other.gameObject.tag == "Killzone")
        {
            this.gameObject.SetActive(false);
        }

        //swtich the forward vectors on the colliding enemies
        if (other.gameObject.tag == "Enemy")
        {
            if (!rotateEnemy)
            {
                //print("dome");
                Vector3 myVelocity = (this.transform.forward);
                Vector3 tempVelocity = myVelocity;
                Vector3 otherVelocity = (other.transform.forward);

                myVelocity = otherVelocity;
                otherVelocity = tempVelocity;

                myVelocity = new Vector3(myVelocity.x, 0, myVelocity.z);
                myVelocity = myVelocity.normalized;
                this.SetDirection(myVelocity);

                otherVelocity = new Vector3(otherVelocity.x, 0, otherVelocity.z);
                otherVelocity = otherVelocity.normalized;
                other.GetComponent<Enemy>().SetDirection(otherVelocity);
            }
        }
    }

}
