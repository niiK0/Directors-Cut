using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Beber : MonoBehaviour, IInteractable
{
    public TaskList.TaskType taskType; // Assign the corresponding task type in the Inspector
    
    public void Interact(GameObject player)
    {
        TaskList taskList = TaskList.Instance;
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskType);

            if (taskList.availableTaskTypes.Contains(taskType) && !taskList.Beber.isComplete)
            {
                TaskData selectedTaskData = taskList.taskTypeToData[taskType];
                
                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + selectedTaskData.taskName);
                Debug.Log("Is Task Completed: " + selectedTaskData.isComplete);
                
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
        task.Beber.isComplete = true;
        // Wait for 5 seconds.
        yield return new WaitForSeconds(5.0f);

        // This code will be executed after waiting for 5 seconds.
        Debug.Log("5 seconds have passed!");
    }
}
    

