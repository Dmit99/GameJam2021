using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : roadUser
{
    private Sprite bikerImage;

    public Biker(Sprite bikerImage, bool drive, bool accident, bool stoplightstop, int speed, string roadusername) : base( drive,  accident,  stoplightstop,  speed,  roadusername)
    {
        this.bikerImage = bikerImage;
        
    }


    public Sprite GetBikerImage()
    {
        return bikerImage;
    }

}
