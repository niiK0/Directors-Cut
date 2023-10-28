using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    public enum TaskType
    {
        Beber,
        Cozinhar,
        // Add more task types as needed
    }

    public TaskType[] availableTaskTypes; // Assign these in the Inspector

    private static TaskList _instance;

    public static TaskList Instance
    {
        get { return _instance; }
    }

    public Dictionary<TaskType, TaskData> taskTypeToData = new Dictionary<TaskType, TaskData>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Initialize the dictionary with the corresponding TaskData ScriptableObjects
        foreach (TaskType taskType in availableTaskTypes)
        {
            TaskData taskData = GetTaskDataFromEnum(taskType);
            if (taskData != null)
            {
                taskTypeToData[taskType] = taskData;
            }
        }

        RandomTask();
    }


    private TaskData GetTaskDataFromEnum(TaskType taskType)
    {
        switch (taskType)
        {
            case TaskType.Beber:
                return Beber; // Assign your TaskData ScriptableObjects here
            case TaskType.Cozinhar:
                return Cozinhar;
            // Add more cases for other task types
            default:
                return null;
        }
    }

    public void RandomTask()
    {
        TaskType randomTaskType = availableTaskTypes[Random.Range(0, availableTaskTypes.Length)];
        TaskData selectedTaskData = taskTypeToData[randomTaskType];

        // Now you have the randomly selected TaskData to work with
        Debug.Log("Selected Task: " + selectedTaskData.taskName);
    }

    // Add your existing fields and methods as needed

    // For example, if you have TaskData ScriptableObjects, you can assign them here:
    public TaskData Beber;
    public TaskData Cozinhar;
    // Add more TaskData fields as needed
}