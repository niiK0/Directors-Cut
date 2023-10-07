using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task 
{
    public string taskName;
    public bool isDoing;
    public bool isComplete;
    public bool isDirector;
    //public Player source;
    public float taskRange;
    public float completePercentage;
    public Steps[] taskSteps;

    public Task(string Name, bool Doing, bool Complete, bool Director, float Range, float Percentage, Steps[] steps)
    {

        taskName= Name;
        isDoing= Doing;
        isComplete= Complete;
        isDirector= Director;
        
        taskRange= Range;
        completePercentage= Percentage;
        taskSteps = steps;
    }
}
