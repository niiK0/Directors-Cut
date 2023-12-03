using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Lock : MonoBehaviour, IInteractable
{
    //Task info
    static int taskIndex = 4;
    static string taskName = "Tranca";
    public bool isComplete = false;

    //TasksUI 
    public GameObject taskUI;
    public GameObject waitingUI;
    public GameObject newWaitingUI;

    //Player Controller Reference
    public GameObject playerObject;

    private bool isTaskCancelled = false;

    public void Interact(GameObject player)
    {
        //Giving the values to their respective holders
        TaskList taskList = TaskList.Instance;
        playerObject = player;

        //Interaction system for the task
        if (taskList != null)
        {
            if (taskList.currTasks.Contains(taskName) && !isComplete)
            {
                //Reset this value everytime the task gets interacted with
                isTaskCancelled = false;

                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);

                //Starting the task
                newWaitingUI = Instantiate(waitingUI, taskUI.transform);
                StartCoroutine(DoTask(taskList, player));

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

    IEnumerator DoTask(TaskList task, GameObject player)
    {
        //FREEZE PLAYER
        player.GetComponent<PlayerController>().freezePlayer = true;
        player.GetComponent<PlayerController>().freezeRotation = true;

        float elapsedTime = 0f;
        float taskDuration = 5.0f;

        while (elapsedTime < taskDuration)
        {
            // Update elapsed time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (elapsedTime >= taskDuration && !isTaskCancelled)
        {
            FinishTask(task);
        }
    }

    public void FinishTask(TaskList task)
    {
        //Setting task as complete
        task.MarkTaskComplete(taskIndex);
        isComplete = true;

        //UNFREEZE
        playerObject.GetComponent<PlayerController>().freezePlayer = false;
        playerObject.GetComponent<PlayerController>().freezeRotation = false;

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
