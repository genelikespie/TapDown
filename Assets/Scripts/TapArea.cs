using UnityEngine;
using System.Collections;

/// <summary>
/// This object shows the area that a player's tap point has affected
/// The player cannot tap in this area while it remains active
/// This is stored in GameObjectPool<TapArea> which is managed by GameManager
/// </summary>
public class TapArea : TapGameObject {

    public const float radius = 16f;

    //Material change functions
    public Material material1;
    public Material material2;

    public Color red;
    public Color grey;
    public Renderer rend;

    float lerp;

    //time functions to remove the TapArea
    public float timeLengthTapArea = 3f;
    public float timeLeftTapArea = 0f;
    bool alreadyActive = false;
    bool MoveUp = false;
    
    private ActiveTapGOPool activeEnemyPool;

    /// <summary>
    /// When TapArea is set to active, we find all the active enemies within our radius
    /// We make the enemies turn around
    /// </summary>
    void OnEnable()
    {
        // call our base class's method

        base.OnEnable();


        // Activate timer to kill Tap Area
        alreadyActive = true;
        MoveUp = true;
        foreach (GameObject obj in activeEnemyPool.activeObjectList)
        {
            Vector3 difference = (obj.transform.position - transform.position);
            difference = new Vector3(difference.x, 0, difference.z);
            Debug.Log(difference + " mag: " + difference.magnitude);
            if ((difference).magnitude - Enemy.radius < radius)
            {
                // since the enemy is in our radius, make the enemy move in another direction
                difference = difference.normalized;
                obj.GetComponent<Enemy>().SetDirection(difference);
            }
        }
    }

    //Use to remove the tap area and make it disappear.
    void FixedUpdate()
    {
        if (alreadyActive == true)
        {
            timeLeftTapArea += Time.deltaTime;

            // this.rend.material.color = Color.Lerp(red, grey, timeLeftTapArea/timeLengthTapArea);
            if (timeLengthTapArea / 2 <= timeLeftTapArea)
            {
                //if changing color from red -> gray
                if (MoveUp == true)
                {
                    float moveY = (transform.position.y - .02f);
                    transform.position = new Vector3(this.transform.position.x, moveY, this.transform.position.z);
                    this.gameObject.GetComponent<MeshRenderer>().material = material2;
                    MoveUp = false;
                }
            }
            //reset and remove the TapArea
            if (timeLengthTapArea <= timeLeftTapArea)
            {
                alreadyActive = false;
                timeLeftTapArea = 0f;
                rend.material = material1;
                this.gameObject.SetActive(false);
                MoveUp = false;
            }
        }
    }



	// Use this for initialization
	void Awake () {


        activeEnemyPool = TapGOPoolSingleton<Enemy>.ActivePoolInstance();
        if (!activeEnemyPool)
            Debug.LogError("Could not find active enemy pool!");
                rend = this.GetComponent<Renderer>();
        red = material1.color;
        grey = material2.color;
    transform.localScale = new Vector3(radius*2, radius*2, radius*2);
	}
}
