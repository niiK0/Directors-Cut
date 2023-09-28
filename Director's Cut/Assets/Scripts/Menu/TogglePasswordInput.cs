using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TogglePasswordInput : MonoBehaviour
{
    private bool isActive = false;

    public void Toggle()
    {
        isActive = !isActive;
        gameObject.GetComponent<TMP_InputField>().interactable = isActive;
        if (!isActive)
        {
            gameObject.GetComponent<TMP_InputField>().text = string.Empty;
        }
    }
}
