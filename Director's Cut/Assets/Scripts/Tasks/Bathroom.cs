using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Bathroom : MonoBehaviour, IInteractable
{
    static int taskIndex = 2;
    static string taskName = "Cagar";
    static float taskRange = 2f;
    public bool isComplete = false;

    GameObject taskUI;

    private bool isTaskCancelled = false;

    public void Interact(GameObject player)
    {
        TaskList taskList = TaskList.Instance;
        taskUI = taskList.gameObject;
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskName);

            if (taskList.currTasks.Contains(taskName) && !isComplete)
            {
                //Reiniciar esta variavel todos os momentos de interaçao pq pode ser cancelado
                isTaskCancelled = false;

                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);

                // Fazer a corrotina que trata da task
                StartCoroutine(DoTask(taskList));

                //UNFREZE PLAYER
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
        //FREEZE PLAYER
        float elapsedTime = 0f;
        float taskDuration = 15.0f; // Change this to your desired task duration

        while (elapsedTime < taskDuration)
        {
            // Check for "x" key press to cancel the task
            if (Input.GetKeyDown(KeyCode.X))
            {
                isTaskCancelled = true;
                break; // Exit the loop and cancel the task
            }

            // Update elapsed time
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        if (isTaskCancelled)
        {
            Debug.Log("Task Cancelled!");
        }
        else
        {
            Debug.Log("5 seconds have passed!");
            task.MarkTaskComplete(taskIndex);
            isComplete = true;

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
