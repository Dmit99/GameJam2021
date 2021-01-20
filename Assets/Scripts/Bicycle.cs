using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : MonoBehaviour
{
    //If spawned right is true he comes sideways.
    //If its falles he spawns bottom screen.
    private bool spawnedRight;
    private string name;
    private float speed;

    public void InstantiateBiker(string bikerName, bool spawnedRight, float speed)
    {
        this.spawnedRight = spawnedRight;
        name = bikerName;
        this.speed = speed / 10000;
    }

    private void Start()
    {
        this.gameObject.name = name;
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -0.236f);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedRight)
        {
            MoveLeft();
        }
        else if (!spawnedRight)
        {
            MoveUp();
        }
    }
    
    void MoveUp()
    {
        transform.Translate(0, speed, 0);
    }

    void MoveLeft()
    {
        transform.Translate(speed, 0, 0);
    }
}
