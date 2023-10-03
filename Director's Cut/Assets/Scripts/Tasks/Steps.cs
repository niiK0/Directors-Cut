using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps 
{
    public bool freezePlayer;
    public string stepName;
    public float fillSpeed;
    public bool isDoing;
    public bool isComplete;
    //public Player source;

    public Steps(bool Frozen, string Name, float CompleteP, bool Doing, bool Complete)
    {
        freezePlayer = Frozen;
        stepName = Name;
        fillSpeed = CompleteP;
        isDoing = Doing;
        isComplete = Complete;
        
    }
}
