using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInteraction : MonoBehaviour, IInteractable
{
    public int taskID;

    public void Interact(GameObject player)
    {
        switch (taskID)
        {
            case 1:
                Debug.Log("Cozinhar");
                //Vamos usar o metodo sendMessage para spawnar os steps
                break;
        }

        //GameObject taskObject = GameObject.Find("PlayerTaskBar");
        //taskObject.SendMessage("DoStep");   
    }
}
