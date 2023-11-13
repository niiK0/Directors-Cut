using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TasksUI : MonoBehaviour
{
    public Transform taskListPanel; 
    public GameObject taskPrefab; 

    private void Start()
    {
        TaskList taskList = TaskList.Instance;

        for (int i = 0; i < taskList.tasksNumber; i++)
        {
            GameObject taskUI = Instantiate(taskPrefab, taskListPanel);
            TextMeshProUGUI taskText = taskUI.GetComponent<TextMeshProUGUI>();
            taskText.text = taskList.currTasksUI[i];
            taskUI.name = taskList.currTasks[i];

            // Ajusta a posiçao de forma a nao dar overlap
            float yOffset = i * taskText.preferredHeight + 10; 
            taskUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -yOffset);
        }
    }
}
