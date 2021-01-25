using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_StoppingLights : MonoBehaviour
{
    public SpriteRenderer stoplight;
    public Sprite stoplightRed;
    public Sprite stoplightGreen;
    [SerializeField] private List<GameObject> roadUsersInCollider;

    //If Active = true, stoplight is red. 
    //If Active = false, stoplight is green.
    bool active;

    private void Awake()
    {
        active = false;
        SpriteUpdate(active);
        roadUsersInCollider = new List<GameObject>();
    }

    private void Update()
    {
        if (active)
        {
            foreach (GameObject i in roadUsersInCollider)
            {
                i.gameObject.GetComponent<roadUser>().SetDriverState(false);
            }
        }
        else if (!active)
        {
            foreach (GameObject i in roadUsersInCollider)
            {
                i.gameObject.GetComponent<roadUser>().SetDriverState(true);
            }
        }
    }

    private void SpriteUpdate(bool isActive)
    {
        if (isActive)
        {
            stoplight.sprite = stoplightRed;
        }
        
        if (!isActive)
        {
            stoplight.sprite = stoplightGreen;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            active = !active;
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            SpriteUpdate(active);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RoadUser")
        {
            roadUsersInCollider.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject i in roadUsersInCollider)
        {
            i.gameObject.GetComponent<roadUser>().SetDriverState(true);
        }

        if (collision.gameObject.tag == "RoadUser")
        {
            roadUsersInCollider.Remove(collision.gameObject);
        }
    }
}
