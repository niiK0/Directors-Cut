using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour, IInteractable
{
    GameObject playerObj = null;
    //Placeholder Task que é só para andar
    Task thisTask = new Task(false, "Andar", false, false, false, 100f, 0f);
    TaskManager taskManager = new TaskManager();

    public int taskId;

    //Variaveis do UI
    public TMP_Text taskTxt;
    public Slider slider;
    private float increment = 0.01f;
    
    public float fillSpeed = 0.5f;

    public void Start()
    {
        
    }

    public void Interact(GameObject player)
    {
        //playerObj = player;
        thisTask = taskManager.GetTaskById(taskId);
        thisTask.isDoing = true;

        taskTxt.text =  thisTask.taskName;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisTask.isDoing)
        {
            //Making the bar appear with the values 
            
            //Verify if its frozen
            //if (thisTask.isFrozen)
            //playerObj.GetComponent<Movement>().freezePlayer = true;

            //Verify if its working
            IncrementProgress(slider.value + increment);

            //slider grows gradually 
            if (slider.value < thisTask.completePercentage)
                slider.value += fillSpeed * Time.deltaTime;

            //Verify if task is complete
            if (slider.value == 1)
            {
                Debug.Log("Já acabou jéssicaaaaaaaaaa");
                thisTask.isDoing = false;

                thisTask.isFrozen = false;

                thisTask.isComplete = true;

                slider.interactable = false;
            }

            //Parar de fazer a task
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                thisTask.isDoing = false;
                thisTask.isFrozen = false;
                slider.interactable = false;
            }
        }
    }

    public void IncrementProgress(float newProgress)
    {
        thisTask.completePercentage = slider.value + newProgress;
    }

}
