using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Piano : MonoBehaviour, IInteractable
{
    public GameObject pianoGamePrefab;
    private GameObject pianoGameInstance; 

    static int taskIndex = 3;
    static string taskName = "Piano";
    public bool isComplete = false;

    GameObject taskUI;

    private bool isTaskCancelled = false;
    
    TaskList taskList;

    public void Interact(GameObject player)
    {
        taskList = TaskList.Instance;
        taskUI = taskList.gameObject;
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskName);

            if (taskList.currTasks.Contains(taskName) && !isComplete)
            {
                //Reiniciar esta variavel todos os momentos de interaçao pq pode ser cancelado
                isTaskCancelled = false;

                //Instanciar o UI pretendido
                pianoGameInstance = Instantiate(pianoGamePrefab);
                // Set the position of the UI (adjust as needed)
                pianoGameInstance.transform.position = new Vector3(0f, 0f, 0f);


                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);

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

    public void CancelTask(GameObject player)
    {
        // Check for "x" key press to cancel the task
        if (Input.GetKeyDown(KeyCode.X))
        {
            isTaskCancelled = true;
        }

        if (isTaskCancelled)
        {
            //UNFREEZE
            player.GetComponent<PlayerController>().freezePlayer = false;
            Debug.Log("Task Cancelled!");
        }
    }

    public void FinishTask(GameObject player)
    {
        taskList.MarkTaskComplete(taskIndex);
        isComplete = true;
        //UNFREEZE
        player.GetComponent<PlayerController>().freezePlayer = false;

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
