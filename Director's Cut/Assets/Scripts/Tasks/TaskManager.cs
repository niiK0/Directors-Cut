using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager 
{
    //Criar a lista de steps para as tasks
    List<Steps> steps = new List<Steps>()
    {
        new Steps(true, "Wait", 5, true, false),
    };
 
    //Criar uma lista de tasks
    List<Task> tasks = new List<Task>() {
        new Task(true, "Cozinhar", false, false, false, new int[] {1}, 10f, 0),
        new Task(true, "Beber", false, false, false, new int[] {1}, 0, 0)
    };

    //Criar funçao que receba o id da task e devolva a informação dela
    public Task GetTaskById(int id)
    {
        return (tasks[id]);
    }

    //Função que vai buscar o step em questao
    public Steps GetStepsById(int id)
    {
        return steps[id];
    }
}
