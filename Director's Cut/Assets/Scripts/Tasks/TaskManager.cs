using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager 
{

    //Criar uma lista de tasks
    List<Task> tasks = new List<Task>() {
        new Task(true, "Cozinhar", false, false, false, 10f, 0)
    };

    //Criar fun�ao que receba o id da task e devolva a informa��o dela
    public Task GetTaskById(int id)
    {
        return (tasks[id]);
    }
}
