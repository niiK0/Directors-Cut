using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour, IInteractable
{
    public Slider slider;

    private bool cooking = false;

    private float increment = 0.01f;
    private float targetProgress = 0;

    public float fillSpeed = 0.5f;
    

    public void Interact()
    {
        cooking = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Verify if its working
        if (cooking)
            IncrementProgress(slider.value + increment);

        //slider grows gradually 
        if (slider.value < targetProgress)
            slider.value += fillSpeed * Time.deltaTime;

        //Verify if task is complete
        if (slider.value == 1){
            Debug.Log("Já acabou jéssicaaaaaaaaaa");
            increment = 0;
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

}
