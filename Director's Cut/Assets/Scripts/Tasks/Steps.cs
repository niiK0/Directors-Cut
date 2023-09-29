using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps 
{
    public bool freezePlayer;
    public string stepName;
    public float stepTime;
    public bool isDoing;
    public bool isComplete;
    //public Player source;

    public Steps(bool Frozen, string Name, float time, bool Doing, bool Complete)
    {
        freezePlayer = Frozen;
        stepName = Name;
        stepTime = time;
        isDoing = Doing;
        isComplete = Complete;
        
    }
}
