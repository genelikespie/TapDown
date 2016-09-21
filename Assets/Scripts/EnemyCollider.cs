using UnityEngine;
using System.Collections;

/// <summary>
/// Attach this script to an object in an Enemy.
/// This should handle collisions for the Enemy Script.
/// </summary>
[RequireComponent(typeof(Collider))]
public class EnemyCollider : TapGameObject {

    public Enemy parentEnemyObject;

    void OnTriggerEnter(Collider other)
    {
        parentEnemyObject.CollideWithOther(other);
    }
}
