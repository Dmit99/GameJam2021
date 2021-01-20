using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadUser : MonoBehaviour
{
    protected bool drive;
    protected bool accident;
    protected bool stoplightstop;

    protected int speed;

    protected string roadusername;

    public roadUser(bool drive, bool accident, bool stoplightstop, int speed, string roadusername)
    {
        this.drive = drive;
        this.accident = accident;
        this.stoplightstop = stoplightstop;
        this.speed = speed;
        this.roadusername = roadusername;
            
    }

    public bool GetAccident()
    {
        return accident;
    }

    public bool GetDriversState()
    {
        return drive;
    }

    public bool GetStopforstoplightState()
    {
        return stoplightstop;
    }

    public int Getspeed()
    {
        return speed;
    }

    public string Getname()
    {
        return roadusername;
    }
}
