using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : MonoBehaviour
{
    //If spawned right is true he comes sideways.
    //If its falles he spawns bottom screen.
    private string name;
    private float speed;

    public void InstantiateBiker(string bikerName, float speed)
    {
        name = bikerName;
        this.speed = speed / 25;
    }

    private void Start()
    {
        this.gameObject.name = name;
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -0.236f);
    }

    // Update is called once per frame
    void Update()
    {
        moving();
    }

    void moving()
    {
        transform.Translate(Vector3.up *speed *Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LaneCollider")
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        GameManager.instance.SetCurrentActiveBikers();
        GameManager.instance.CurrentBikersListChecker();
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
}
