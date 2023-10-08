using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseReporter : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform meetingPlace;
    [SerializeField] private WarningScript warning;

    public void Interact(GameObject playerObj)
    {
        StartCoroutine(warning.CallWarning());
        Movement playerMovement = playerObj.GetComponent<Movement>();
        playerMovement.MovePlayer(meetingPlace);
        ReporterFunction();
    }

    protected abstract void ReporterFunction();
}
