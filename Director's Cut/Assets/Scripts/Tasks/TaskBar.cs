using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject currItem;

    public static TaskBar Instance;

    //Canvas dos steps
    public GameObject stepsCanvas;
    public Slider stepsSlider;
    public TMP_Text stepTxt;

    //Canvas das tasks
    public GameObject taskCanvas;
    public Slider taskSlider;
    public TMP_Text taskTxt;
    public float taskPercent = 0;

    //Lista de tasks que ira receber as tasks aleatoriamente dependendo do numero de tasks permitidas
    public int tasksNumber = 2; 
    [SerializeField] List<Task> currTasks = new List<Task>() { };


    //Player
    public GameObject playerObj = null;
    
    //Placeholder Task que � s� para andar
    Task thisTask = new Task("Andar", false, false, false, 0f, 0f, new Steps[1] { new Steps(false, "Andar", 0, false, false,"Andar", true) });
    
    //Instanciar o taskmanager de maneira a atribuir tasks ao jogador
    TaskManager taskManager = new TaskManager();
    public int currTask = 0;
    public int currStep = 0;
    public int stepsNumber = 0;

    //Verifica�oes de task
    bool verificaStep = false;
    bool verificaTask = false;

    private float increment = 0.01f;

    public float fillSpeed = 0.5f;
    public int TaskPercent = 0;
    public float completePercentage = 0;

    public void Awake()
    {
        Instance = this;
        RandomTask();
    }

    // Update is called once per frame
    public void Update()
    {
        //Verifica se ja tem um item instanciado
        if (!thisTask.taskSteps[currStep].instatiatedItem)
        {
            //Instancia o item
            InstantiateItem();
            thisTask.taskSteps[currStep].instatiatedItem = true;
        }


        if (thisTask.taskSteps[currStep].isDoing)
        {
            

            //Verify if its frozen
            if (thisTask.taskSteps[currStep].freezePlayer)
                playerObj.GetComponent<PlayerController>().freezePlayer = true;

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

                playerObj.GetComponent<PlayerController>().freezePlayer = false;

                stepsCanvas.SetActive(false);

                Destroy(currItem);

                //Output da task 
                taskPercent += thisTask.taskSteps.Length;
                taskSlider.value = taskPercent;

                //Verifica se todos os Steps foram completos
                if (currStep == stepsNumber - 1)
                {
                    currStep= 0;
                    verificaStep = true;
                }
                else
                {
                    currStep++;
                    stepsCanvas.SetActive(true);
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
                    //Come�ar a nova task
                    currTask++;
                    thisTask = currTasks[currTask];
                    thisTask.taskSteps[currStep].isDoing = false;

                    //Reiniciar todas as variaveis do canvas task
                    taskSlider.value = thisTask.completePercentage;
                    taskTxt.text = thisTask.taskName;
                    taskCanvas.SetActive(true);


                    //Reiniciar todas as variaveis do canvas Step
                    stepsNumber = thisTask.taskSteps.Length;
                    stepsSlider.value = 0;
                    stepTxt.text = thisTask.taskSteps[currStep].stepName;
                    stepsCanvas.SetActive(true);
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
                playerObj.GetComponent<PlayerController>().freezePlayer = false;
                completePercentage = 0;

                stepsCanvas.SetActive(false);
            }

        }
    }


    public void InstantiateItem()
    {
        Debug.Log(thisTask.taskSteps[currStep].prefabName);
        //string prefabFolderPath = "E:\\UnityProjects\\Directors-Cut\\Director's Cut\\Assets\\Prefabs";
        //GameObject itemPrefab = Resources.Load<GameObject>(prefabFolderPath + thisTask.taskSteps[currStep].prefabName);

        currItem = Instantiate(itemPrefab, new Vector3(0, 3, 11), Quaternion.identity);
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
        //Reinicio as variaveis de forma a come�ar na primeira task da lista
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

    public void DoStep()
    {
        thisTask.taskSteps[currStep].isDoing = true;
        Debug.Log("Chegou");
    }

}
