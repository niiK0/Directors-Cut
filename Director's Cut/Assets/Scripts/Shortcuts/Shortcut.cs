using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour, IInteractable
{
    [SerializeField] private Shortcut[] connectedShortcuts;

    public void Interact(GameObject player)
    {
        Debug.Log("Shortcut!");
    }
}
