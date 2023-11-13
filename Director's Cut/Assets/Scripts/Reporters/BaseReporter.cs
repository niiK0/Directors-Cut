using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseReporter : MonoBehaviourPunCallbacks, IInteractable
{
    public Transform meetingPlace;
    [SerializeField] WarningScript warning;

    private void Start()
    {
        meetingPlace = GameObject.FindGameObjectWithTag("MeetingPlace").transform;
    }

    public void Interact(GameObject playerObj)
    {
        Debug.Log("Interacted with btn");
        StartCoroutine(warning.CallWarning());
        ReporterFunction(playerObj);
    }

    protected abstract void ReporterFunction(GameObject playerObj);
}
