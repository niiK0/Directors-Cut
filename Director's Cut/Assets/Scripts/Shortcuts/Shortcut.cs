using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour, IInteractable
{
    [SerializeField] private Shortcut[] connectedShortcuts;

    public void Interact()
    {
        Debug.Log("Shortcut!");
    }
}
