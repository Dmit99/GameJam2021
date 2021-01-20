using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : MonoBehaviour
{
    //If spawned right is true he comes sideways.
    //If its falles he spawns bottom screen.
    [SerializeField] private bool spawnedRight;
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
        moving(spawnedRight);
    }

    void moving(bool right)
    {
        if (right)
        {
            transform.Translate(-speed, 0, 0);
        }
        else transform.Translate(0, speed, 0);
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
