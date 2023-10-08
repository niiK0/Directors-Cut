using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        GameObject taskObject = GameObject.Find("PlayerTaskBar");
        taskObject.SendMessage("DoStep");   
    }
}
