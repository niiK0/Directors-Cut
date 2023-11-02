using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bathroom : MonoBehaviour, IInteractable
{
    static string taskName = "Cagar";
    static float taskRange = 2f;
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
        isComplete = true;
        //FREEZE PLAYER
        // Wait for 5 seconds.
        yield return new WaitForSeconds(15.0f);

        // This code will be executed after waiting for 15 seconds.
        Debug.Log("15 seconds have passed!");
    }
}
