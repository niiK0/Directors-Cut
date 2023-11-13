using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cozinhar : MonoBehaviour, IInteractable
{
    static int taskIndex = 10;
    static string taskName = "Cozinhar";
    static float taskRange = 1f;
    public bool isComplete = false;


    public void Interact(GameObject player)
    {
        TaskList taskList = TaskList.Instance;
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskName);

            if (taskList.tasks.Contains(taskName))
            {
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);
                // Debug other properties of the 'selectedTaskData' here.
            }
            else
            {
                Debug.Log("Task type not found in TaskList.");
            }
        }
        else
        {
            Debug.LogError("TaskList singleton not found.");
        }
    }
}
