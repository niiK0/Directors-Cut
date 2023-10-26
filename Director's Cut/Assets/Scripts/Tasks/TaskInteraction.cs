using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInteraction : MonoBehaviour, IInteractable
{
    public TaskList.TaskType taskType; // Assign the corresponding task type in the Inspector

    public void Interact(GameObject player)
    {
        TaskList taskList = TaskList.Instance;
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskType);

            if (taskList.taskTypeToData.ContainsKey(taskType))
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
