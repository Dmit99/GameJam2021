using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : MonoBehaviour
{
    //If spawned right is true he comes sideways.
    //If its falles he spawns bottom screen.
    private bool spawnedRight;

    public void InstantiateBiker(bool spawnedRight)
    {
        this.spawnedRight = spawnedRight;
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
        transform.Translate(0, 0.001f, 0);
    }

    void MoveLeft()
    {
        transform.Translate(0.001f , 0, 0);
    }
}
