using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Beber : MonoBehaviour, IInteractable
{
    //Task info
    static int taskIndex = 0;
    static string taskName = "Beber";
    public bool isComplete = false;

    //TasksUI 
    public GameObject taskUI;
    public GameObject waitingUI;
    public GameObject newWaitingUI;

    private bool isTaskCancelled = false;

    public void Interact(GameObject player)
    {
        //Giving the values to their respective holders
        TaskList taskList = TaskList.Instance;
        
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
            // Check for "x" key press to cancel the task
            if (Input.GetKeyDown(KeyCode.X))
            {
                isTaskCancelled = true;
                break;
            }

            // Update elapsed time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Verify if the task got Cancelled
        if (isTaskCancelled)
        {
            //UNFREEZE
            player.GetComponent<PlayerController>().freezePlayer = false;
            player.GetComponent<PlayerController>().freezeRotation = false;
            Destroy(newWaitingUI);
            Debug.Log("Task Cancelled!");
        }
        else
        {
            //Setting task as complete
            task.MarkTaskComplete(taskIndex);
            isComplete = true;
            Destroy(newWaitingUI);

            //UNFREEZE
            player.GetComponent<PlayerController>().freezePlayer = false;
            player.GetComponent<PlayerController>().freezeRotation = false;

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
}
    

