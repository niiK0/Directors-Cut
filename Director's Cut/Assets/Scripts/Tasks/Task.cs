using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public string taskName;
    public bool isDoing;
    public bool isComplete;
    public bool isDirector;
    public Steps[] steps;
    //public Player source;
    public float taskRange;
    public float completePercentage;
}
