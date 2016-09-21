using UnityEngine;
using System.Collections;

public class KittyEnemy : Enemy {

    Transform ColorChange;
    Color BaseColor;
    bool SpeedCheck1 = false;
    bool SpeedCheck2 = false;

    void Awake()
    {
        base.Awake();
        ColorChange = transform.Find("animWrapper/default");
        BaseColor = ColorChange.GetComponent<Renderer>().material.color;
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
        

        if (maxSpeed * .65 < speed && SpeedCheck1 == false)
        {
            SpeedCheck1 = true;
            ColorChange.GetComponent<Renderer>().material.color = Color.yellow;
        }

        if (maxSpeed == speed && SpeedCheck2 == false)
        {
            SpeedCheck2 = true;
            ColorChange.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    //Reset the SpeedChecks so that it can go back to normal color
    //Set the cat back to its base color.
    protected void OnDisable()
    {
        SpeedCheck1 = false;
        SpeedCheck2 = false;
        ColorChange.GetComponent<Renderer>().material.color = BaseColor;
        base.OnDisable();
    }
}
