using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Piano : MonoBehaviour, IInteractable
{
    //Singleton
    public static Piano Instance;

    //Task info
    static int taskIndex = 3;
    static string taskName = "Piano";
    public bool isComplete = false;

    //Instantiated object
    public GameObject pianoGamePrefab;
    private GameObject pianoGameInstance;

    //Player Holder
    private GameObject currPlayer = null;
    
    //TasksUI 
    GameObject taskUI;

    //TaskList Singleton Instance
    TaskList taskList;

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
    }

    public void Update()
    {
        //Call cancel task after you can do it
        if(currPlayer != null)
            CancelTask(currPlayer);
    }

    public void Interact(GameObject player)
    {
        //Giving the values to their respective holders
        currPlayer = player;
        taskList = TaskList.Instance;
        taskUI = taskList.gameObject;
        
        //Interaction system for the task
        if (taskList != null)
        {
            Debug.Log("Task Interaction - Task Type: " + taskName);

            if (taskList.currTasks.Contains(taskName) && !isComplete)
            {
                //Reset this value everytime the task gets interacted with
                isTaskCancelled = false;

                //Instantiating the UI prefab
                pianoGameInstance = Instantiate(pianoGamePrefab);
                pianoGameInstance.transform.position = new Vector3(0f, 0f, 0f);

                //Outputs so para ver o que se passa
                Debug.Log("Current Task Name: " + taskName);
                Debug.Log("Is Task Completed: " + isComplete);

                //FREEZE PLAYER
                currPlayer.GetComponent<PlayerController>().freezePlayer = true;
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
            //Removing the mini-game from the screen
            Destroy(pianoGameInstance);
            //UNFREEZE
            player.GetComponent<PlayerController>().freezePlayer = false;
            Debug.Log("Task Cancelled!");
            isTaskCancelled= false;
        }
    }


   
    public void FinishTask()
    {
        //Removing the mini-game from the screen
        Destroy(pianoGameInstance);

        //Setting task as complete
        taskList.MarkTaskComplete(taskIndex);
        isComplete = true;

        //UNFREEZE
        currPlayer.GetComponent<PlayerController>().freezePlayer = false;

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
