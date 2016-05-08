using UnityEngine;
using System.Collections;

/// <summary>
/// Holds methods called by an Enemy's animator in an
/// animation event
/// </summary>
public class AnimEnemy : MonoBehaviour {

    public Transform target;
    public Enemy enemy;
    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    /// <summary>
    /// Called as an AnimationEvent by the AnimationClip at
    /// the end of the Drop animation.
    /// This sets the Enemy as initialized and starts its movement
    /// </summary>
    public void DoneSpawning()
    {
        //Debug.Log("done spawning");
        if (target == null) Debug.LogError("target was not initialzed when enemy spawned!");
        Vector3 diff = target.position - transform.position;
        diff = new Vector3(diff.x, 0, diff.z);                  // ignore the y axis
        enemy.SetDirection((diff).normalized);
        enemy.doneSpawning = true;
    }
}
