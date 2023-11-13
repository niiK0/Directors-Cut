using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Luzes : MonoBehaviour, IInteractable
{
    //Task info
    static int taskIndex = 3;
    static string taskName = "Luz";
    public bool isComplete = false;

    //TasksUI 
    public GameObject taskUI;

    //Lantern
    public GameObject taskLight;
    public GameObject taskLightModified;

    public void Interact(GameObject player)
    {
        //Giving the values to their respective holders
        TaskList taskList = TaskList.Instance;

        //Interaction system for the task
        if (taskList != null)
        {
            if (taskList.currTasks.Contains(taskName) && !isComplete)
            {
                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);

                DoTask(taskList, player);

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

    public void DoTask(TaskList task, GameObject player)
    {
        //Complete the task
        taskLight.SetActive(false);
        taskLightModified.SetActive(true);

        //Setting task as complete
        task.MarkTaskComplete(taskIndex);
        isComplete = true;

        //Make the task name turn green
        if (taskUI != null)
        {
            Transform childTransform = taskUI.transform.Find(taskName);

            if (childTransform != null)
            {

                TextMeshProUGUI childText = childTransform.GetComponent<TextMeshProUGUI>();
                childText.color = Color.green;
            }
            else
            {
                Debug.LogWarning("Child not found with name: " + taskName);
            }
        }
        else
        {
            Debug.LogWarning("TaskUI not found with name: TaskUI");
        }
    }
}
