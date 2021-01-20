using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerkeersDeelnemer : MonoBehaviour
{
    protected bool accident;
    protected bool moving;
    protected bool stoppingLight;

    public bool GetAccident()
    {
        return accident;
    }
}
