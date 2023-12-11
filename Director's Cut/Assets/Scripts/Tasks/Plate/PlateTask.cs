using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlateTask : MonoBehaviour
{
    //Singleton
    public static PlateTask Instance;

    //Task info
    static int taskIndex = 6;
    static string taskName = "Plate";
    public bool isDoing;
    public bool isComplete = false;

    //Instantiated object
    public GameObject plateGamePrefab;
    private GameObject plateGameInstance;

    //TasksUI 
    public GameObject taskUI;

    //TaskList Singleton Instance
    TaskList taskList;

    //Player Controller Reference
    public GameObject playerObject;

    private bool isTaskCancelled = false;

    private void Awake()
    {
        //Singleton Verification
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        isDoing = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            CancelTask(playerObject);
        }
    }

    public void Interact(GameObject player)
    {
        //Giving the values to their respective holders
        taskList = TaskList.Instance;
        playerObject = player;

        //Interaction system for the task
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskName);

            if (taskList.currTasks.Contains(taskName) && !isComplete)
            {
                //Reset this value everytime the task gets interacted with
                isTaskCancelled = false;


                if (!isDoing)
                {
                    //Instantiating the UI prefab
                    plateGameInstance = Instantiate(plateGamePrefab);
                    plateGameInstance.transform.position = new Vector3(0f, 0f, 0f);
                    isDoing = true;
                }
                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);

                //FREEZE PLAYER
                player.GetComponent<PlayerController>().freezePlayer = true;
                player.GetComponent<PlayerController>().freezeRotation = true;
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
        //Cancel the task
        isTaskCancelled = true;

        if (isTaskCancelled)
        {
            //Removing the mini-game from the screen
            Destroy(plateGameInstance);
            //UNFREEZE
            player.GetComponent<PlayerController>().freezePlayer = false;
            player.GetComponent<PlayerController>().freezeRotation = false;
            Debug.Log("Task Cancelled!");
            isTaskCancelled = false;
            isDoing = false;
        }
    }



    public void FinishTask()
    {
        //Removing the mini-game from the screen
        Destroy(plateGameInstance);

        //Setting task as complete
        taskList.MarkTaskComplete(taskIndex);
        isComplete = true;
        isDoing = false;

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
