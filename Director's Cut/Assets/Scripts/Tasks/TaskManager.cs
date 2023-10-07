using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager 
{
    //Criar a lista de steps para as tasks
    List<Steps> steps = new List<Steps>()
    {
        new Steps(false, "Andar", 0, false, false),
        new Steps(true, "Wait", 0.01f, false, false),
        new Steps(false, "Pick up bread", 1, false, false),
        new Steps(false, "Pick up ham", 1, false, false),
    };
 
    //Criar uma lista de tasks
    List<Task> tasks = new List<Task>() {
        new Task("Cozinhar", false, false, false,0f,0f, new Steps[1] {new Steps(true, "Wait", 0.01f, true, false)}),
        new Task("Beber", false, false, false, 0f, 0f, new Steps[2] {new Steps(true, "Wait", 0.01f, false, false), new Steps(true, "Wait", 0.01f, false, false)}),
    };

    //Criar funçao que receba o id da task e devolva a informação dela
    public Task GetTaskById(int id)
    {
        return (tasks[id]);
    }

    
}
