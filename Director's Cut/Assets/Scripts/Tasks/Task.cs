using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task 
{
    public string taskName;
    public bool isFrozen;
    public bool isDoing;
    public bool isComplete;
    public bool isDirector;
    public Steps[] taskSteps;
    //public Player source;
    public float taskRange;
    public float completePercentage;

    public Task(bool Frozen, string Name, bool Doing, bool Complete, bool Director,  float Range, float Percentage)
    {
        isFrozen= Frozen;
        taskName= Name;
        isDoing= Doing;
        isComplete= Complete;
        isDirector= Director;
        //Steps
        taskRange= Range;
        completePercentage= Percentage;
    }
}
