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
    public static TaskBar Instance;

    // Canvas das tasks
    public GameObject taskCanvas;
    public TMP_Text taskTxt;

    // Player
    public GameObject playerObj = null;

    // Current Task
    private TaskData currentTask;

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    public void Start()
    {
        
        
    }

   
}
