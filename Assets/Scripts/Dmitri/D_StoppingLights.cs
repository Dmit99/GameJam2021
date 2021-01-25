using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_StoppingLights : MonoBehaviour
{
    public GameObject stoplightRed;
    public GameObject stoplightGreen;
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
        for (int i = 0; i < roadUsersInCollider.Count; i++)
        {
            roadUsersInCollider[i].GetComponent<roadUser>().SetDriverState(active);
        }
    }

    private void SpriteUpdate(bool isActive)
    {
        if (isActive)
        {
            stoplightRed.SetActive(false);
            stoplightGreen.SetActive(true);
        }
        
        if (!isActive)
        {
            stoplightRed.SetActive(true);
            stoplightGreen.SetActive(false);
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
            if(collision.GetComponent<Scooter>() != null)
            {
               if(collision.GetComponent<Scooter>().GetStopforstoplightState() == false)
               {
                    Debug.Log("Current state is: " + collision.GetComponent<Scooter>().GetStopforstoplightState());
                    return;
               }
            }
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
