using UnityEngine;

public class Scooter : roadUser
{
    private Sprite scooterImage;
    private bool stoplightGo;

    /// <summary>
    /// Constructs a scooter.
    /// </summary>
    /// <param name="scooterImage"></param>
    /// <param name="stoplightGo"></param>
    /// <param name="drive"></param>
    /// <param name="accident"></param>
    /// <param name="stoplightstop"></param>
    /// <param name="speed"></param>
    /// <param name="roadusername"></param>
    public void GenerateScooter(Sprite scooterImage, bool stoplightGo, bool drive, bool accident, bool stoplightstop, int speed, string roadusername)
    {
        this.scooterImage = scooterImage;
        this.stoplightGo = stoplightGo;
        this.drive = drive;
        this.accident = accident;
        this.stoplightstop = stoplightstop;
        this.speed = speed;
        this.roadusername = roadusername;

        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = this.scooterImage;
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
