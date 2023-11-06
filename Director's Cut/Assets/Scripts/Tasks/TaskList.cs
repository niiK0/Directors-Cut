using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    //Numero de tasks RELACIONAR COM O SV
    public int tasksNumber = 2;

    //Array que guarda todas as tasks no inspector
    public string[] tasks;

    //Array que guarda as tasks que o player vai ter acesso
    public string[] currTasks;

    //Tornar este script um Singleton
    public static TaskList Instance;

    private void Awake()
    {
        //Tenho de instanciar o seu tamanho aqui?
        currTasks = new string[tasksNumber];
        
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

        if (tasksNumber > tasks.Length)
        {
            Debug.LogWarning("Not enough tasks to generate the requested number.");
            return;
        }

        // Lista que recolhe os indices ja escolhidos para as tasks
        List<int> availableTaskIndices = new List<int>();
        for (int i = 0; i < tasks.Length; i++)
        {
            availableTaskIndices.Add(i);
        }

        //Loop de randomizaçao
        for (int i = 0; i < tasksNumber; i++)
        {
            // Seleciona a task aleatoriamente
            int randomIndex = Random.Range(0, availableTaskIndices.Count);
            int selectedTaskIndex = availableTaskIndices[randomIndex];

            currTasks[i] = tasks[selectedTaskIndex];

            // Remove-se o indice da lista de modo a nao have repetiçoes
            availableTaskIndices.RemoveAt(randomIndex);
        }

        // Log the selected tasks.
        foreach (string task in currTasks)
        {
            Debug.Log("Selected Task: " + task);
        }
    }
}
