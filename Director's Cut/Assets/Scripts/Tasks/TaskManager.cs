using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager 
{
    //Criar a lista de steps para as tasks
    List<Steps> steps = new List<Steps>()
    {
        new Steps(false, "Andar", 0, false, false,"Andar", true),
        new Steps(true, "Wait", 0.01f, false, false, "Esperar", false),
        new Steps(false, "Pick up bread", 1, false, false, "Pao", false),
        new Steps(false, "Pick up ham", 1, false, false,"Fiambre", false),
    };
 
    //Criar uma lista de tasks
    List<Task> tasks = new List<Task>() {
        new Task("Cozinhar", false, false, false,0f,0f, new Steps[1] {new Steps(true, "Wait", 0.01f, false, false,"Cubo", false)}),
        new Task("Beber", false, false, false, 0f, 0f, new Steps[2] {new Steps(true, "Wait", 0.01f, false, false, "Cubo", false), new Steps(true, "Wait", 0.01f, false, false, "Cubo" ,false)}),
    };

    //Criar funçao que receba o id da task e devolva a informação dela
    public Task GetTaskById(int id)
    {
        return (tasks[id]);
    }

    
}
