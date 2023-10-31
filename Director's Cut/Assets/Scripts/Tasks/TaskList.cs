using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    //Array que guarda todas as tasks no inspector
    public string[] tasks;

    //Tornar este script um Singleton
    public static TaskList Instance;

    private void Awake()
    {
        //Verificaçao de singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        RandomTask();
    }

    public void RandomTask()
    {
        if (tasks.Length == 0)
        {
            Debug.LogWarning("No tasks assigned to the task list.");
            return;
        }

        int randomIndex = Random.Range(0, tasks.Length);
        
        Debug.Log("Selected Task: " + tasks[randomIndex]);
    }
}
