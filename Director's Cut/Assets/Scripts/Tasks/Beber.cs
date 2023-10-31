using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Beber : MonoBehaviour, IInteractable
{
    static string taskName = "Beber";
    static float taskRange = 3f;
    public bool isComplete = false;

    public void Interact(GameObject player)
    {
        TaskList taskList = TaskList.Instance;
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskName);

            if (taskList.tasks.Contains(taskName) && !isComplete)
            {
                
                
                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);
                
                // Fazer a corrotina que trata da task
                StartCoroutine(DoTask(taskList));
            }
            else
            {
                Debug.Log("Task type not found in TaskList. Or Its Done");
            }
        }
        else
        {
            Debug.LogError("TaskList singleton not found.");
        }
    }

    //NAO ESQUECER DE FREEZAR O JOGADOR
    IEnumerator DoTask(TaskList task)
    {
        isComplete = true;
        // Wait for 5 seconds.
        yield return new WaitForSeconds(5.0f);

        // This code will be executed after waiting for 5 seconds.
        Debug.Log("5 seconds have passed!");
    }
}
    

