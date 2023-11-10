using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    //Singleton
    public static TaskList Instance;

    //Tasks Number RELACIONAR COM O SV
    public int tasksNumber = 2;

    //Array that saves every task (In inspector)
    public string[] tasks;

    //Array that saves the completness of tasks 
    public bool[] taskCompleteness;
    public bool tasksCompleted = false;

    //Array that saves the tasks selected for the player
    public string[] currTasks;

    

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

        //Instantiate currTasks with the desired number of tasks
        currTasks = new string[tasksNumber];

        //Instantiate taskCompleteness with the size of the currTask
        taskCompleteness = new bool[tasksNumber];

        //This function gets all tasks at random for the currTasks array
        RandomTasks();
    }

    public void RandomTasks()
    {
        //Checks if there are tasks to be randomized
        if (tasks.Length == 0)
        {
            Debug.LogWarning("No tasks assigned to the task list.");
            return;
        }

        //Since we cant repeat tasks checks if there are enough tasks
        if (tasksNumber > tasks.Length)
        {
            Debug.LogWarning("Not enough tasks to generate the requested number.");
            return;
        }

        //This list saves every task index so that we can remove them to check if they were already selected
        List<int> availableTaskIndices = new List<int>();
        for (int i = 0; i < tasks.Length; i++)
        {
            availableTaskIndices.Add(i);
        }

        //Randomization Loop
        for (int i = 0; i < tasksNumber; i++)
        {
            //Selects a random index from the list 
            int randomIndex = Random.Range(0, availableTaskIndices.Count);
            int selectedTaskIndex = availableTaskIndices[randomIndex];

            //Selects the new task 
            currTasks[i] = tasks[selectedTaskIndex];

            //Removes the said index from the list so we dont get any repetitions
            availableTaskIndices.RemoveAt(randomIndex);
        }

        //Log the selected tasks.
        foreach (string task in currTasks)
        {
            Debug.Log("Selected Task: " + task);
        }
    }

    
    public void MarkTaskComplete(int taskIndex)
    {
        //Verifies if the taskIndex exists in our array
        if (taskIndex >= 0 && taskIndex < currTasks.Length)
        {
            //If it does complete it
            taskCompleteness[taskIndex] = true;
        }

        //Calls the function to see if everything is finished
        tasksCompleted = AreAllTasksComplete();

        //Verifies if every task as been completed
        if (tasksCompleted)
        {
            Debug.Log("TODAS AS TASKS FEITAS");
        }
    }
    

    public bool AreAllTasksComplete()
    {
        //Goes through all the values in the array
        foreach (bool taskComplete in taskCompleteness)
        {
            //If one of the values is false the function returns false
            if (!taskComplete)
            {
                return false;            
            }
        }
        //VARIAVEL DO SERVER PARA SABER QUE ESTA TUDO FEITO
        return true;
    }
}
