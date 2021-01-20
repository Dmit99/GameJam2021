using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fietser : VerkeersDeelnemer
{
    private Sprite fietserImage;

    //Constructor
    public Fietser(Sprite fietserImage)
    {
        this.fietserImage = fietserImage;
    }

    public Sprite GetFietserImage()
    {
        return fietserImage;
    }
}
