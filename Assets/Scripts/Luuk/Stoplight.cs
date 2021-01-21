using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoplight : MonoBehaviour
{
    float startPosX;
    float startPosY;

    bool touched;
    bool active;


    void Update()
    {
        if (touched)
        {
           
            GetComponent<SpriteRenderer>().enabled = active;
        }
    }


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            active = !active;
            touched = true;
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);


        }
    }

    private void OnMouseUp()
    {
        touched = false;
    }
}
