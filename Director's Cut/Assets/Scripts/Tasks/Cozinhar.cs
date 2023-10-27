using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cozinhar : MonoBehaviour, IInteractable
{
    public TaskList.TaskType taskType; // Assign the corresponding task type in the Inspector
    
    public void Interact(GameObject player)
    {
        TaskList taskList = TaskList.Instance;
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskType);

            if (taskList.availableTaskTypes.Contains(taskType))
            {
                TaskData selectedTaskData = taskList.taskTypeToData[taskType];
                Debug.Log("Current Task Name: " + selectedTaskData.taskName);
                Debug.Log("Is Task Completed: " + selectedTaskData.isComplete);
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
