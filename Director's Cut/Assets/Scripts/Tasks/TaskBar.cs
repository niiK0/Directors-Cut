using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour, IInteractable
{
    //Canvas dos steps
    public GameObject stepsCanvas;
    public Slider stepsSlider;
    public TMP_Text stepTxt;

    //Canvas das tasks
    public GameObject taskCanvas;
    public Slider taskSlider;
    public TMP_Text taskTxt;


    //Player
    GameObject playerObj = null;
    
    //Placeholder Task que é só para andar
    Task thisTask = new Task("Andar", false, false, false,new int[1] { 0 }, 100f, 0f);
    TaskManager taskManager = new TaskManager();
    public int taskId;
    public int currStep = 0;

    //PlaceHolder Step
    Steps thisStep = new Steps(false, "Andar", 0, false, false);
    public int stepId = 0;

    //Verificaçoes de task
    Steps stepVer = new Steps(false, "Andar", 0, false, false);
    bool verifica = false;

    private float increment = 0.01f;

    public float fillSpeed = 0.5f;
    public int TaskPercent = 0;
    public float completePercentage = 0;

    public void Interact(GameObject player)
    {
        playerObj = player;
        Debug.Log("Interact");
    }

    public void Awake()
    {
        taskId = Random.Range(0, 2);

        //On Start Task
        thisTask = taskManager.GetTaskById(taskId);
        thisTask.isDoing = true;
        //TaskUI
        taskSlider.value = thisTask.completePercentage;
        taskTxt.text = thisTask.taskName;

        //On Start Steps
        thisStep = taskManager.GetStepsById(thisTask.taskSteps[currStep]);
        increment = thisStep.fillSpeed;
        //On interact StepUI
        stepsCanvas.SetActive(true);
        stepsSlider.value = completePercentage;
        stepTxt.text = thisStep.stepName;
    }


    // Update is called once per frame
    public void Update()
    {
        if (thisTask.isDoing)
        {
            
            //Verify if its frozen
            if (thisStep.freezePlayer)
                playerObj.GetComponent<Movement>().freezePlayer = true;

            //Verify if its working
            IncrementProgress(stepsSlider.value + increment);

            //slider grows gradually 
            if (stepsSlider.value < completePercentage)
                stepsSlider.value += fillSpeed * Time.deltaTime;

            //Verify if step is complete
            if (stepsSlider.value == 1)
            {
                thisStep.isDoing = false;

                thisStep.freezePlayer = false;

                thisStep.isComplete = true;

                stepsSlider.value = 0;

                playerObj.GetComponent<Movement>().freezePlayer = false;

                stepsCanvas.SetActive(false);

                //Output da task 
                taskSlider.value += 1/thisTask.taskSteps.Length;

                //Verifica se a task esta completa
                for (int i = 0; i < thisTask.taskSteps.Length; i++)
                {
                    stepVer = taskManager.GetStepsById(thisTask.taskSteps[i]);
                    if (stepVer.isComplete)
                    {
                        verifica = true;
                        Debug.Log("BRO");
                    }
                    else
                    {
                        verifica = false;
                    }
                }
            }

            //Se a task estiver completa
            if (verifica)
            {
                thisTask.isDoing = false;

                thisTask.isComplete = true;

                stepsSlider.value = 0;

                taskCanvas.SetActive(false);
            }

            //Parar de fazer a task
            if (Input.GetKeyDown(KeyCode.X))
            {
                //Resets
                thisStep.isDoing = false;
                thisStep.freezePlayer = false;
                stepsSlider.value = 0;
                playerObj.GetComponent<Movement>().freezePlayer = false;
                completePercentage = 0;

                stepsCanvas.SetActive(false);
            }

        }
    }

    public void IncrementProgress(float newProgress)
    {
        completePercentage = stepsSlider.value + newProgress;
    }


}
