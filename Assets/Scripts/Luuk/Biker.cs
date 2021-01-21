using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : roadUser
{
    private Sprite[] bikerImage = new Sprite[4];

    /// <summary>
    /// Constructs a Biker.
    /// </summary>
    /// <param name="bikerImage"></param>
    /// <param name="drive"></param>
    /// <param name="accident"></param>
    /// <param name="stoplightstop"></param>
    /// <param name="speed"></param>
    /// <param name="roadusername"></param>
    public void GenerateBiker(Sprite bikerImage001, Sprite bikerImage002, Sprite bikerImage003, Sprite bikerImage004, bool drive, bool accident, bool stoplightstop, float speed, int laneNumber,string roadusername)
    {
        bikerImage[0] = bikerImage001;
        bikerImage[1] = bikerImage002;
        bikerImage[2] = bikerImage003;
        bikerImage[3] = bikerImage004;

        this.drive = drive;
        this.accident = accident;
        this.stoplightstop = stoplightstop;
        this.speed = speed;
        this.laneNumber = laneNumber;
        this.roadusername = roadusername;
    }

    private void Start()
    {

        StartCoroutine(VisualMoving(currentSpeed: CalculateSpriteSpeed(speed)));
        Debug.Log("current speed is: " + speed + " animation speed is: " + CalculateSpriteSpeed(speed));
    }

    IEnumerator VisualMoving(float currentSpeed)
    {
        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = bikerImage[0];
        yield return new WaitForSeconds(currentSpeed);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = bikerImage[1];
        yield return new WaitForSeconds(currentSpeed);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = bikerImage[2];
        yield return new WaitForSeconds(currentSpeed);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = bikerImage[3];
        yield return new WaitForSeconds(currentSpeed);
        StartCoroutine(VisualMoving(currentSpeed: currentSpeed));
    }
}
