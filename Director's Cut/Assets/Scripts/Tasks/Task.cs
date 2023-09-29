using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task 
{
    public bool isFrozen;
    public string taskName;
    public bool isDoing;
    public bool isComplete;
    public bool isDirector;
    public int[] taskSteps;
    //public Player source;
    public float taskRange;
    public float completePercentage;

    public Task(bool Frozen, string Name, bool Doing, bool Complete, bool Director, int[] steps, float Range, float Percentage)
    {
        isFrozen= Frozen;
        taskName= Name;
        isDoing= Doing;
        isComplete= Complete;
        isDirector= Director;
        taskSteps = steps;
        taskRange= Range;
        completePercentage= Percentage;
    }
}
