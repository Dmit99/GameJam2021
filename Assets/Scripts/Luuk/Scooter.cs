using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scooter : roadUser
{
    private Sprite scooterImage;
    private bool stoplightGo;


    public Scooter(Sprite scooterImage, bool stoplightGo, bool drive, bool accident, bool stoplightstop, int speed, string roadusername) : base(drive, accident, stoplightstop, speed, roadusername)
    {
        this.scooterImage = scooterImage;
        this.stoplightGo = stoplightGo;
    }

    public Sprite GetScooterImage()
    {
        return scooterImage;
    }

    public bool GetStoplightGo()
    {
        return stoplightstop;
    }


}
