using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : roadUser
{
    private Sprite bikerImage;

    /// <summary>
    /// Constructs a Biker.
    /// </summary>
    /// <param name="bikerImage"></param>
    /// <param name="drive"></param>
    /// <param name="accident"></param>
    /// <param name="stoplightstop"></param>
    /// <param name="speed"></param>
    /// <param name="roadusername"></param>
    public void GenerateBiker(Sprite bikerImage, bool drive, bool accident, bool stoplightstop, int speed, string roadusername)
    {
        this.bikerImage = bikerImage;
        this.drive = drive;
        this.accident = accident;
        this.stoplightstop = stoplightstop;
        this.speed = speed;
        this.roadusername = roadusername;

        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = this.bikerImage;
    }


    public Sprite GetBikerImage()
    {
        return bikerImage;
    }
}
