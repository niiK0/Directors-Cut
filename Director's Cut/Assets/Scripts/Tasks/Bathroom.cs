using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Bathroom : MonoBehaviour, IInteractable
{
    static int taskIndex = 1;
    static string taskName = "Cagar";
    public bool isComplete = false;

    public GameObject taskUI;
    public GameObject waitingUI;
    public GameObject newWaitingUI;

    private bool isTaskCancelled = false;

    //Player Controller Reference
    public GameObject playerObject;


    public void Update()
    {
        // Check for "x" key press to cancel the task
        if (Input.GetKeyDown(KeyCode.X))
        {
            CancelTask(playerObject);
        }
    }


    public void Interact(GameObject player)
    {
        TaskList taskList = TaskList.Instance;
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskName);

            if (taskList.currTasks.Contains(taskName) && !isComplete)
            {
                //Reiniciar esta variavel todos os momentos de interašao pq pode ser cancelado
                isTaskCancelled = false;

                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);

                // Fazer a corrotina que trata da task
                newWaitingUI = Instantiate(waitingUI, taskUI.transform);
                StartCoroutine(DoTask(taskList, player));

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
    IEnumerator DoTask(TaskList task, GameObject player)
    {
        //FREEZE PLAYER
        player.GetComponent<PlayerController>().freezePlayer = true;
        player.GetComponent<PlayerController>().freezeRotation = true;

        float elapsedTime = 0f;
        float taskDuration = 10.0f; // Change this to your desired task duration

        while (elapsedTime < taskDuration)
        {
            // Update elapsed time
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }


        if (elapsedTime >= taskDuration && !isTaskCancelled)
        {
            FinishTask(task);
        }
    }


    public void CancelTask(GameObject player)
    {
        isTaskCancelled = true;

        //Verify if the task got Cancelled
        if (isTaskCancelled)
        {

            StopAllCoroutines();

            //UNFREEZE
            player.GetComponent<PlayerController>().freezePlayer = false;
            player.GetComponent<PlayerController>().freezeRotation = false;
            Destroy(newWaitingUI);
            Debug.Log("Task Cancelled!");
        }
    }

    public void FinishTask(TaskList task)
    {
        StopAllCoroutines();

        //Setting task as complete
        task.MarkTaskComplete(taskIndex);
        isComplete = true;
        Destroy(newWaitingUI);

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
