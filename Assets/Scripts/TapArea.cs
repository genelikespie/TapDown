using UnityEngine;
using System.Collections;

/// <summary>
/// This object shows the area that a player's tap point has affected
/// The player cannot tap in this area while it remains active
/// This is stored in GameObjectPool<TapArea> which is managed by GameManager
/// </summary>
public class TapArea : TapGameObject {

    public const float Radius = 16f;
    
    private ActiveTapGOPool activeEnemyPool;

    /// <summary>
    /// When TapArea is set to active, we find all the active enemies within our radius
    /// We make the enemies turn around
    /// </summary>
    void OnEnable()
    {
        // call our base class's method
        base.OnEnable();

        foreach (GameObject obj in activeEnemyPool.activeObjectList)
        {
            if ((obj.transform.position - transform.position).magnitude < Radius)
            {
                // since the enemy is in our radius, make the enemy move in another direction
                Vector3 direction = (obj.transform.position - transform.position).normalized;
                obj.GetComponent<Enemy>().SetDirection(direction);
            }
        }
    }
	// Use this for initialization
	void Awake () {
        activeEnemyPool = TapGOPoolSingleton<Enemy>.ActivePoolInstance();
        if (!activeEnemyPool)
            Debug.LogError("Could not find active enemy pool!");

        transform.localScale = new Vector3(Radius*2, Radius*2, Radius*2);
	}
}
