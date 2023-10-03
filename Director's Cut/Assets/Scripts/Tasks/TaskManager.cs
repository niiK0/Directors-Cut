using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager 
{
    //Criar a lista de steps para as tasks
    List<Steps> steps = new List<Steps>()
    {
        new Steps(false, "Andar", 0, false, false),
        new Steps(true, "Wait", 0.01f, true, false),
        new Steps(false, "Pick up bread", 1, true, false),
        new Steps(false, "Pick up ham", 1, true, false),
    };
 
    //Criar uma lista de tasks
    List<Task> tasks = new List<Task>() {
        new Task("Cozinhar", false, false, false, new int[1] {1}, 10f, 0),
        new Task("Beber", false, false, false, new int[1] {1}, 0, 0),
        new Task("FAZER UMA ASSANDES", false, false, false, new int[2] {2,3}, 0, 0)
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
