using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Man I'm dead");
        Destroy(gameObject);
    }
}
