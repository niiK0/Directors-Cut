using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    //Canvas das tasks
    public GameObject taskCanvas;
    public TMP_Text taskTxt;
    public int taskNumber;

    //PlaceHolder task de modo a ter uma task inicializada para uso
    Task thisTask = new Task("Andar", false, false, false, 0f, 0f, new Steps[1] { new Steps(false, "Andar", 0, false, false, "Andar", true) });

    //Lista de tasks que vai conter as tasks deste jogador
    [SerializeField] List<Task> currTasks = new List<Task>() { };

    //Instanciar o taskmanager de maneira a atribuir tasks ao jogador
    TaskManager taskManager = new TaskManager();
    public int currTask = 0;


    public void Awake()
    {
        RandomTask();
    }

    

    public void RandomTask()
    {
        //Loop que vai buscar tasks e preencher a lista de tasks do jogador
        int i = 0;
        while(i < taskNumber)
        {
            int randomTaskID = Random.Range(0, 2);

            thisTask = taskManager.GetTaskById(currTask);
            
            // Check if the task is already in the list.
            if (!currTasks.Contains(thisTask))
            {
                // If the task is not in the list, add it.
                currTasks.Add(thisTask);
                i++;
            }
            
        }
        taskTxt.text = thisTask.taskName;
    }
}
