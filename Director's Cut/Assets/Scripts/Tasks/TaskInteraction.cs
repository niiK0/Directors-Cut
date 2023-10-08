using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInteraction : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        GameObject taskObject = GameObject.Find("PlayerTaskBar");
        taskObject.SendMessage("DoStep");   
    }
}
