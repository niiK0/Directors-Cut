using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    //Canvas dos steps
    public GameObject stepsCanvas;
    public Slider stepsSlider;
    public TMP_Text stepTxt;

    //Canvas das tasks
    public GameObject taskCanvas;
    public Slider taskSlider;
    public TMP_Text taskTxt;

    //Lista de tasks que ira receber as tasks aleatoriamente dependendo do numero de tasks permitidas
    public int tasksNumber = 2; 
    [SerializeField] List<Task> currTasks = new List<Task>() { };


    //Player
    public GameObject playerObj = null;
    
    //Placeholder Task que é só para andar
    Task thisTask = new Task("Andar", false, false, false, 0f, 0f, new Steps[1] { new Steps(false, "Andar", 0, false, false) });
    
    //Instanciar o taskmanager de maneira a atribuir tasks ao jogador
    TaskManager taskManager = new TaskManager();
    public int currTask = 0;
    public int currStep = 0;
    public int stepsNumber = 0;

    //Verificaçoes de task
    bool verificaStep = false;
    bool verificaTask = false;

    private float increment = 0.01f;

    public float fillSpeed = 0.5f;
    public int TaskPercent = 0;
    public float completePercentage = 0;

    public void Awake()
    {
        RandomTask();
    }


    // Update is called once per frame
    public void Update()
    {
        if (thisTask.taskSteps[currStep].isDoing)
        {
            
            //Verify if its frozen
            if (thisTask.taskSteps[currStep].freezePlayer)
                playerObj.GetComponent<Movement>().freezePlayer = true;

            //Incremento seguido
            IncrementProgress(stepsSlider.value + increment);

            //slider grows gradually 
            if (stepsSlider.value < completePercentage)
                stepsSlider.value += fillSpeed * Time.deltaTime;

            //Verify if step is complete
            if (stepsSlider.value == 1)
            {
                thisTask.taskSteps[currStep].isDoing = false;

                thisTask.taskSteps[currStep].freezePlayer = false;

                thisTask.taskSteps[currStep].isComplete = true;

                stepsSlider.value = 0;

                playerObj.GetComponent<Movement>().freezePlayer = false;

                stepsCanvas.SetActive(false);

                //Output da task 
                taskSlider.value += 1 / thisTask.taskSteps.Length;

                //Verifica se a task foi completa
                if (currStep == stepsNumber - 1 && thisTask.taskSteps[currStep].isComplete == true)
                {
                    verificaStep = true;
                }
                else
                {
                    currStep++;
                }
            }

            //Se a task estiver completa
            if (verificaStep)
            {
                thisTask.isDoing = false;

                thisTask.isComplete = true;

                stepsSlider.value = 0;

                taskCanvas.SetActive(false);

                verificaStep = false;

                //Verifica se a task foi completa
                if ( currTask == tasksNumber - 1 && thisTask.isComplete == true)
                {
                    verificaTask = true;
                }
                else
                {
                    currTask++;
                }
            }

            if (verificaTask)
            {
                Debug.Log("Todas as tasks completas");
            }

            //Parar de fazer a task
            if (Input.GetKeyDown(KeyCode.X))
            {
                //Resets
                thisTask.taskSteps[currStep].isDoing = false;
                thisTask.taskSteps[currStep].freezePlayer = false;
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

    public void RandomTask()
    {
        //Loop que vai buscar tasks e preencher a lista de tasks do jogador
        for(int i = 0; i < tasksNumber; i++)
        {
            currTask = Random.Range(0, 2);
            thisTask = taskManager.GetTaskById(currTask);
            currTasks.Add(thisTask);
        }
        //Reinicio as variaveis de forma a começar na primeira task da lista
        currTask = 0;
        thisTask = currTasks[currTask];

        //On Start Task
        //THIS TASK VAI SER OUTRA CENA
        
        thisTask.isDoing = true;
        stepsNumber = thisTask.taskSteps.Length;
        //TaskUI
        taskSlider.value = thisTask.completePercentage;
        taskTxt.text = thisTask.taskName;

        
        //On interact StepUI
        stepsCanvas.SetActive(true);
        stepsSlider.value = completePercentage;
        stepTxt.text = thisTask.taskSteps[0].stepName;
    }



}
