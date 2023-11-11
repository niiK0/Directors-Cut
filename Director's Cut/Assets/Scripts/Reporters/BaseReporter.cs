using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseReporter : MonoBehaviourPunCallbacks, IInteractable
{
    public Transform meetingHolder;
    [SerializeField] WarningScript warning;

    public void Interact(GameObject playerObj)
    {
        Debug.Log("Interacted with emergency button");
        StartCoroutine(warning.CallWarning());
        ReporterFunction(playerObj);
    }

    protected abstract void ReporterFunction(GameObject playerObj);
}
